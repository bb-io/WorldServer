using Apps.Worldserver.Api;
using Apps.Worldserver.Invocables;
using Apps.Worldserver.Models.Files.Request;
using Apps.Worldserver.Models.Tasks.Request;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System.Net.Http.Headers;
using System.Net.Mime;

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
        var request = new WorldserverRequest($"/files/asset", Method.Get);
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
    public async Task UploadFile([ActionParameter] UploadFileRequest uploadFileRequest)
    {
        var request = new WorldserverRequest($"../v1/files", Method.Post);

        var fileBytes = await _fileManagementClient.DownloadAsync(uploadFileRequest.File).Result.GetByteData();
        request.AddFile("file", fileBytes, uploadFileRequest.File.Name);

        await Client.ExecuteWithErrorHandling(request);
    }
}
