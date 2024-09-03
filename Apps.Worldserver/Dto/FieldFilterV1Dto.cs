namespace Apps.Worldserver.Dto;

public class FieldFilterV1Dto
{
    public FieldFilterV1Dto(string fieldName, string operatorName, string value)
    {
        Field = fieldName;
        Operator = operatorName;
        Value = value;
    }

    public string Field { get; set; }
    public string Operator { get; set; }
    public string Value { get; set; }
}
