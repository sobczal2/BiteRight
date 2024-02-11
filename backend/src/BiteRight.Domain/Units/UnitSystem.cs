namespace BiteRight.Domain.Units;

[Flags]
public enum UnitSystem
{
    Metric = 1,
    Imperial = 2,
    Both = Metric | Imperial
}