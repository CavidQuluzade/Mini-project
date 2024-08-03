using Application.Services.Abstract;
using Core.Entities;
using Core.Messages;
using Data.UnitOfWork.Concrete;
using System.Globalization;

namespace Application.Services.Concrete;

public class SellerService : ISellerService
{
    private readonly Unitofwork _unitofwork;
    public SellerService(Unitofwork unitofwork)
    {
        _unitofwork = unitofwork;
    }
    public void AddProduct(int id)
    {
        Product product = new Product();
    InputName: BasicMessages.InputMessage("name of product");
        string productName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(productName))
        {
            ErrorMessages.InvalidInputMessage(productName);
            goto InputName;
        }
        product.Name = productName;
    InputCount: BasicMessages.InputMessage("count");
        string CountInput = Console.ReadLine();
        bool isSucceded = int.TryParse(CountInput, out int productCount);
        if (!isSucceded || string.IsNullOrWhiteSpace(CountInput))
        {
            ErrorMessages.InvalidInputMessage(CountInput);
            goto InputCount;
        }
        product.Count = productCount;
        int count = _unitofwork.Categories.GetCategoryCount();
        if (count <= 0)
        {
            ErrorMessages.CountIsZeroMessage("category");
            return;
        }
    InputPrice: BasicMessages.InputMessage("price");
        string PriceInput = Console.ReadLine();
        isSucceded = decimal.TryParse(PriceInput, out decimal productPrice);
        if (!isSucceded || string.IsNullOrWhiteSpace(PriceInput))
        {
            ErrorMessages.InvalidInputMessage(PriceInput);
            goto InputPrice;
        }
        product.Price = productPrice;
    CategoryInput: _unitofwork.Categories.GetAllCategories();
        BasicMessages.InputMessage("category id");
        string categoryInput = Console.ReadLine();
        isSucceded = int.TryParse(categoryInput, out int categoryId);
        if (!isSucceded || string.IsNullOrWhiteSpace(categoryInput))
        {
            ErrorMessages.InvalidInputMessage(categoryInput);
            goto CategoryInput;
        }
        product.CategoryId = categoryId;
        product.SellerId = id;
        _unitofwork.Products.Add(product);
        _unitofwork.Commit(productName, "added");
    }
    public void ChangeProductCount()
    {
        ProductInput: _unitofwork.Products.GetAllProducts();
        BasicMessages.InputMessage("product id");
        string productIdInput = Console.ReadLine();
        bool isSucceded = int.TryParse(productIdInput, out int productId);
        if (!isSucceded || string.IsNullOrWhiteSpace(productIdInput))
        {
            ErrorMessages.InvalidInputMessage(productIdInput);
            goto ProductInput;
        }
        var existProduct = _unitofwork.Products.GetProduct(productId);
        if (existProduct == null)
        {
            ErrorMessages.NotFoundMessage(productIdInput);
            goto ProductInput;
        }
        CountINput: BasicMessages.InputMessage("new count");
        string inputCount = Console.ReadLine();
        isSucceded = int.TryParse(inputCount, out int Count);
        if(!isSucceded || Count <= existProduct.Count || string.IsNullOrWhiteSpace(inputCount))
        {
            ErrorMessages.InvalidInputMessage(inputCount);
            goto CountINput;
        }
        existProduct.Count = Count;
        _unitofwork.Products.Update(existProduct);
        _unitofwork.Commit(existProduct.Name, "updated");
    }

    public void GetIncome(int id)
    {
      decimal total =  _unitofwork.Sellers.GetIncome(id);
        Console.WriteLine($"Your income is: {total}");
    }

    public void GetSelledProduct(int id)
    {
        if (_unitofwork.Orders.GetOrdersCountBySeller(id) <= 0)
        {
            ErrorMessages.CountIsZeroMessage("order");
        }
        else
        {
            _unitofwork.Orders.GetOrdersBySeller(id);
        }
    }
    public void GetAllProducts(int id)
    {
        _unitofwork.Products.GetAllProductsBySellerId(id);
    }
    public void GetSelledProductByDate(int id)
    {
    InputDate: BasicMessages.InoutDateMessage();
        string input = Console.ReadLine();
        bool isSucceded = DateTime.TryParseExact(input, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputDate;
        }
        _unitofwork.Orders.GetOrdersByDateWithSellerId(date, id);
    }

    public void RemoveProduct()
    {
    ProductInput: _unitofwork.Products.GetAllProducts();
        BasicMessages.InputMessage("product id");
        string productIdInput = Console.ReadLine();
        bool isSucceded = int.TryParse(productIdInput, out int productId);
        if (!isSucceded || string.IsNullOrWhiteSpace(productIdInput))
        {
            ErrorMessages.InvalidInputMessage(productIdInput);
            goto ProductInput;
        }
        var existProduct = _unitofwork.Products.GetProduct(productId);
        if (existProduct == null)
        {
            ErrorMessages.NotFoundMessage(productIdInput);
            goto ProductInput;
        }
        _unitofwork.Products.Delete(existProduct);
        _unitofwork.Commit(existProduct.Name, "deleted");

    }

    public void SearchProducts()
    {
        BasicMessages.InputMessage("something to search");
        string inputSearch = Console.ReadLine();
        _unitofwork.Products.SearchProduct(inputSearch);
    }
}
