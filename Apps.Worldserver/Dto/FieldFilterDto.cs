namespace Apps.Worldserver.Dto;

public class FieldFilterDto
{
    public FieldFilterDto(string fieldName, string operatorName, string value)
    {
        Field = fieldName;
        Operator = operatorName;
        Value = value;
    }

    public string Field { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
}
