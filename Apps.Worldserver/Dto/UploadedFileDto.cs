namespace Apps.Worldserver.Dto;
public class UploadedFileDto
{
    public string Name { get; set; }
    public string InternalName { get; set; }
    public string FullName { get; set; }
    public string Url { get; set; }
    public int Size { get; set; }
    public long CreationTime { get; set; }
    public bool Exists { get; set; }
}

