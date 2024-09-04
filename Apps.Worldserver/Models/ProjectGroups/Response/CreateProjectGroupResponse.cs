namespace Apps.Worldserver.Models.ProjectGroups.Response;
public class CreateProjectGroupResponse
{
    public string Status { get; set; }
    public List<CreateProjectGroupInnerResponse> Response { get; set; }
}
public class CreateProjectGroupInnerResponse
{
    public string Status { get; set; }
    public int? Response { get; set; }
    public List<Warning>? Warnings { get; set; }
}

public class Warning
{
    public string Message { get; set; }
}

