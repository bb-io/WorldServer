using Blackbird.Applications.Sdk.Common;

namespace Apps.Worldserver.Dto;
public class UploadedFileDto
{
    public string Name { get; set; }

    [Display("Internal name")]
    public string InternalName { get; set; }

    [Display("Full name")]
    public string FullName { get; set; }
    public string Url { get; set; }
    public int Size { get; set; }

    [Display("Creation time")]
    public long CreationTime { get; set; }
    public bool Exists { get; set; }
}

