namespace Apps.Worldserver.Dto;

public class CollectionResponseDto<T>
{
    public int Total { get; set; }
    public IEnumerable<T> Items { get; set; }
}
