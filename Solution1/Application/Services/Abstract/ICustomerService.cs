namespace Application.Services.Abstract;

public interface ICustomerService
{
    public void BuyProduct(int id);
    public void GetBoughtProducts(int id);
    public void GetProductsByDate(int id);
    public void FilterProducts();
}
