namespace BiteRight.Application.Dtos.Products;

public class SimpleProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime AddedDateTime { get; set; }
    public double UsagePercentage { get; set; }
}