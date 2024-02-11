namespace BiteRight.Application.Dtos.Units;

public class UnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Abbreviation { get; set; } = default!;
    public UnitSystemDto UnitSystem { get; set; }
}