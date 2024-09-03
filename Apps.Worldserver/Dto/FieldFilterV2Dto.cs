using Newtonsoft.Json.Linq;

namespace Apps.Worldserver.Dto;
public class FieldFilterV2Dto
{
    public FieldFilterV2Dto(string fieldName, string type, string value)
    {
        Field = fieldName;
        Type = type;
        Criterion = new() { Value = value };
    }

    public string Field { get; set; }
    public string Type { get; set; }
    public Criterion Criterion { get; set; }
}

public class Criterion
{
    public string Value { get; set; }
}

