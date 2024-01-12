using BiteRight.Domain.Common;

namespace BiteRight.Domain.Product;

public class Product : AggregateRoot<ProductId>
{
    public Name Name { get; }
    public Description Description { get; }
    public Price? Price { get; }
    public ExpirationDate ExpirationDate { get; }
    public AddedDateTime AddedDateTime { get; }
    public Usage Usage { get; }
}