namespace Core.Entities;

public class Order : Base
{
    public Customer Customers { get; set; }
    public int CustomerId { get; set; }
    public Seller Sellers { get; set; }
    public int SellerId { get; set; }
    public Product Products { get; set; }
    public int ProductId { get; set; }
    public decimal TotalAmount { get; set; }
}
