namespace BiteRight.Application.Dtos.Products;

public enum ProductSortingStrategy
{
    NameAsc = 0,
    NameDesc = 1,
    ExpirationDateAsc = 2,
    ExpirationDateDesc = 3,
    AddedDateTimeAsc = 4,
    AddedDateTimeDesc = 5,
    ConsumptionAsc = 6,
    ConsumptionDesc = 7,
}