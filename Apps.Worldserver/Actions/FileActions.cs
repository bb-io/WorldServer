using Apps.Worldserver.Api;
using Apps.Worldserver.Dto;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Files.Request;
using Apps.Worldserver.Models.Tasks.Request;
using Apps.Worldserver.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace Apps.Worldserver.Actions;

[ActionList]
public class FileActions : WorldserverInvocable
{
    private readonly IFileManagementClient _fileManagementClient;

    public FileActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) : base(invocationContext)
    {
        _fileManagementClient = fileManagementClient;
    }

    [Action("Download file", Description = "Download task file")]
    public async Task<FileReference> DownloadFile([ActionParameter] GetTaskRequest taskRequest,
        [ActionParameter] DownloadFileRequest downloadFileRequest)
    {
        var request = new WorldserverRequest($"/v2/files/asset", Method.Get);
        request.AddQueryParameter("resourceId", taskRequest.TaskId);

        if(!string.IsNullOrEmpty(downloadFileRequest.AssetLocationType))
            request.AddQueryParameter("assetLocationType", downloadFileRequest.AssetLocationType);

        var response = await Client.ExecuteWithErrorHandling(request);

        using var stream = new MemoryStream(response.RawBytes);
        var contentDisposition = ContentDispositionHeaderValue.Parse(response.ContentHeaders.First(x => x.Name == "Content-Disposition").Value.ToString());
        var file = await _fileManagementClient.UploadAsync(stream, MediaTypeNames.Text.Html, contentDisposition.FileNameStar);
        return file;
    }

    [Action("Upload file", Description = "Upload file")]
    public async Task<UploadedFileDto> UploadFile([ActionParameter] UploadFileRequest uploadFileRequest)
    {
        return await UploadFileWithCustomForm(uploadFileRequest.File);
    }

    public async Task<UploadedFileDto> UploadFileWithCustomForm(FileReference fileReference)
    {
        var fileStream = await _fileManagementClient.DownloadAsync(fileReference);

        using var client = new WebClient();
        client.Headers.Add("token", Client.ObtainSessionToken(InvocationContext.AuthenticationCredentialsProviders));

        var multipart = new MultipartFormBuilder();
        multipart.AddFile("file", fileReference.Name, fileStream);

        using var formBodyStream = multipart.GetStream();
        client.Headers.Add("content-type", multipart.ContentType);
        var response = client.UploadData(
            $"{WorldserverClient.GetUri(InvocationContext.AuthenticationCredentialsProviders).ToString().TrimEnd('/')}/v1/files",
            formBodyStream.ToArray());
        return JsonConvert.DeserializeObject<UploadedFileDto>(Encoding.UTF8.GetString(response));
    }
}
