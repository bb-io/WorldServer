namespace Apps.Worldserver.Dto;

public class WorldserverError
{
    public IEnumerable<ErrorDto> Errors { get; set; }
}

public class WorldserverErrorWrapper
{
    public string Status { get; set; }

    public IEnumerable<WorldserverError> Response { get; set; }
}
