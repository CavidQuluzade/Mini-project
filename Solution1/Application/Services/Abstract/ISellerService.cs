namespace Application.Services.Abstract;

public interface ISellerService
{
    public void AddProduct(int id);
    public void ChangeProductCount();
    public void RemoveProduct();
    public void GetSelledProduct(int id);
    public void GetSelledProductByDate(int id);
    public void SearchProducts();
    public void GetIncome(int id);
}
