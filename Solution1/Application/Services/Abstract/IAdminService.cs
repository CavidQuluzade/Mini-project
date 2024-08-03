namespace Application.Services.Abstract;

public interface IAdminService
{
    public void AddCustomer();
    public void RemoveCustomer();
    public void AddSeller();
    public void RemoveSeller();
    public void AddCategory();
    public void GetAllSellers();
    public void GetAllCustomers();
    public void GetOrders();
    public void GetOrdersBySeller();
    public void GetOrdersByCustomer();
    public void GetOrdersByDate();
}
