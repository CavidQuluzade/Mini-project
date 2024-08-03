using Application.Services.Abstract;
using Core.Entities;
using Core.Messages;
using Data.Repositories.Concrete;
using Data.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Globalization;

namespace Application.Services.Concrete;

public class AdminService : IAdminService
{
    private readonly Unitofwork _unitofwork;

    public AdminService(Unitofwork unitofwork)
    {
        _unitofwork = unitofwork;
    }

    public void GetAllCustomers()
    {
        _unitofwork.Customers.GetAllCustomers();
    }

    public void GetAllSellers()
    {
        _unitofwork.Sellers.GetAllSellers();
    }

    public void GetOrders()
    {
        _unitofwork.Orders.GetOrdersDesc();
    }

    public void GetOrdersByCustomer()
    {
        int count = _unitofwork.Customers.GetCustomersCount();
        if (count <= 0)
        {
            ErrorMessages.CountIsZeroMessage("customer");
            return;
        }
    Input: _unitofwork.Customers.GetAllCustomers();
        BasicMessages.InputMessage("customer id");
        string input = Console.ReadLine();
        bool isSucceded = int.TryParse(input, out int id);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto Input;
        }
        var existCustomer = _unitofwork.Customers.GetCustomerById(id);
        if (existCustomer is null)
        {
            ErrorMessages.NotFoundMessage("customer");
            goto Input;
        }
        _unitofwork.Orders.GetOrdersByCustomer(id);
    }

    public void GetOrdersByDate()
    {
    InputDate: BasicMessages.InoutDateMessage();
        string input = Console.ReadLine();
        bool isSucceded = DateTime.TryParseExact(input, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputDate;
        }
        int count = _unitofwork.Orders.GetOrdersByDateCount(date);
        if (count <= 0)
        {
            ErrorMessages.CountIsZeroMessage("order");
            return;
        }
        _unitofwork.Orders.GetOrdersByDate(date);
    }

    public void GetOrdersBySeller()
    {
        int count = _unitofwork.Sellers.GetSellersCount();
        if (count <= 0)
        {
            ErrorMessages.CountIsZeroMessage("seller");
            return;
        }
    Input: _unitofwork.Sellers.GetAllSellers();
        BasicMessages.InputMessage("seller id");
        string input = Console.ReadLine();
        bool isSucceded = int.TryParse(input, out int id);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto Input;
        }
        var existSeller= _unitofwork.Sellers.GetSellerById(id);
        if (existSeller is null)
        {
            ErrorMessages.NotFoundMessage("seller");
            goto Input;
        }
        _unitofwork.Orders.GetOrdersBySeller(id);

    }

    public void AddCategory()
    {
    RepeatName: BasicMessages.InputMessage("category name");
        string categoryName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(categoryName))
        {
            ErrorMessages.InvalidInputMessage(categoryName);
            goto RepeatName;
        }
        Category category = new Category();
        category.Name = categoryName;
        var existCategory = _unitofwork.Categories.GetCategoryByName(categoryName);
        if (existCategory is not null)
        {
            ErrorMessages.ExistMessage(categoryName);
            return;
        }
        _unitofwork.Categories.Add(category);
        _unitofwork.Commit(category.Name, "added");
    }

    public void AddCustomer()
    {
        Customer customer = new Customer();
    InputName: BasicMessages.InputMessage("name");
        string customerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerName))
        {
            ErrorMessages.InvalidInputMessage(customerName);
            goto InputName;
        }
        customer.Name = customerName;
    InputSurname: BasicMessages.InputMessage("surname");
        string customerSurname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerSurname))
        {
            ErrorMessages.InvalidInputMessage(customerSurname);
            goto InputSurname;
        }
        customer.Surname = customerSurname;
    InputEmail: BasicMessages.InputMessage("email");
        string customerEmail = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerEmail) || !customerEmail.Contains("@"))
        {
            ErrorMessages.InvalidInputMessage(customerEmail);
            goto InputEmail;
        }
        var existadminEmail = _unitofwork.Admins.ExistAdminEmail(customerEmail);
        var existsellerEmail = _unitofwork.Sellers.ExistSellerEmail(customerEmail);
        var existcustomerEmail = _unitofwork.Customers.ExistCustomerEmail(customerEmail);
        if (existadminEmail || existsellerEmail || existcustomerEmail)
        {
            ErrorMessages.ExistMessage("email");
            goto InputEmail;
        }
        customer.Email = customerEmail;
    InputPassword: BasicMessages.InputMessage("password");
        string customerPassword = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerPassword))
        {
            ErrorMessages.InvalidInputMessage(customerPassword);
            goto InputPassword;
        }
        if(customerPassword.Length < 8 || !customerPassword.Any(char.IsUpper) || !customerPassword.Any(char.IsDigit))
        {
            ErrorMessages.InvalidInputMessage(customerPassword);
            goto InputPassword;
        }
        PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
        customer.Password = passwordHasher.HashPassword(customer, customerPassword);
    InputPhone: BasicMessages.InputMessage("phone number (example: +994000000000)");
        string customerPhone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerPhone) || customerPhone.Length != 13 || !customerPhone.Any(char.IsDigit) || customerPhone.Contains("+994"))
        {
            ErrorMessages.InvalidInputMessage(customerPhone);
            goto InputPhone;
        }
        customer.Phone = customerPhone;
    InputPIN: BasicMessages.InputMessage("PIN");
        string customerPIN = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(customerPIN) || (customerPIN.Length != 7 || !customerPIN.Any(char.IsDigit)))
        {
            ErrorMessages.InvalidInputMessage(customerPIN);
            goto InputPIN;
        }
        bool existcustomerPIN = _unitofwork.Customers.ExistCustomerPIN(customerPIN);
        bool existsellerPIN = _unitofwork.Sellers.ExistSellerPIN(customerPIN);
        if (existsellerPIN || existcustomerPIN)
        {
            ErrorMessages.ExistMessage("PIN");
            goto InputPIN;
        }
        customer.Pin = customerPIN;
    InputSeria: BasicMessages.InputMessage("Seria number (example: 0000000)");
        string SeriaInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(SeriaInput) || (SeriaInput.Length != 7 || !int.TryParse(SeriaInput, out int customerSeria)))
        {
            ErrorMessages.InvalidInputMessage(SeriaInput);
            goto InputSeria;
        }
        bool existcustomerSeria = _unitofwork.Customers.ExistCustomerSeria(customerSeria);
        bool existsellerSeria = _unitofwork.Sellers.ExistSellerSeria(customerSeria);
        if (existsellerSeria || existcustomerSeria)
        {
            ErrorMessages.ExistMessage("Seria");
            goto InputSeria;
        }
        customer.SeriaNumber = customerSeria;
        _unitofwork.Customers.Add(customer);
        _unitofwork.Commit(customerName, "added");

    }
    public void AddSeller()
    {
        Seller seller = new Seller();
    InputName: BasicMessages.InputMessage("name");
        string sellerName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerName))
        {
            ErrorMessages.InvalidInputMessage(sellerName);
            goto InputName;
        }
        seller.Name = sellerName;
    InputSurname: BasicMessages.InputMessage("surname");
        string sellerSurname = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerSurname))
        {
            ErrorMessages.InvalidInputMessage(sellerSurname);
            goto InputSurname;
        }
        seller.Surname = sellerSurname;
    InputEmail: BasicMessages.InputMessage("email");
        string sellerEmail = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerEmail) || !sellerEmail.Contains("@"))
        {
            ErrorMessages.InvalidInputMessage(sellerEmail);
            goto InputEmail;
        }
        bool existadminEmail = _unitofwork.Admins.ExistAdminEmail(sellerEmail);
        bool existcustomerEmail = _unitofwork.Customers.ExistCustomerEmail(sellerEmail);
        bool existsellerEmail = _unitofwork.Sellers.ExistSellerEmail(sellerEmail);
        if (existadminEmail || existcustomerEmail || existsellerEmail)
        {
            ErrorMessages.ExistMessage("email");
            goto InputEmail;
        }
        seller.Email = sellerEmail;
    InputPassword: BasicMessages.InputMessage("password");
        string sellerPassword = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerPassword))
        {
            ErrorMessages.InvalidInputMessage(sellerPassword);
            goto InputPassword;
        }
        if (sellerPassword.Length < 8 || !sellerPassword.Any(char.IsUpper) || !sellerPassword.Any(char.IsDigit))
        {
            ErrorMessages.InvalidInputMessage(sellerPassword);
            goto InputPassword;
        }
        PasswordHasher<Seller> passwordHasher = new PasswordHasher<Seller>();
        seller.Password = passwordHasher.HashPassword(seller, sellerPassword);
    InputPhone: BasicMessages.InputMessage("phone number (example: +994000000000");
        string sellerPhone = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerPhone) || sellerPhone.Length != 13 || !sellerPhone.Any(char.IsDigit) || !sellerPhone.Contains("+994"))
        {
            ErrorMessages.InvalidInputMessage(sellerPhone);
            goto InputPhone;
        }
        seller.Phone = sellerPhone;
    InputPIN: BasicMessages.InputMessage("PIN");
        string sellerPIN = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(sellerPIN) || (sellerPIN.Length != 7 || !sellerPIN.Any(char.IsDigit)))
        {
            ErrorMessages.InvalidInputMessage(sellerPIN);
            goto InputPIN;
        }
        bool existcustomerPIN = _unitofwork.Customers.ExistCustomerPIN(sellerPIN);
        bool existsellerPIN = _unitofwork.Sellers.ExistSellerPIN(sellerPIN);
        if (existsellerPIN || existcustomerPIN)
        {
            ErrorMessages.ExistMessage("PIN");
            goto InputPIN;
        }
        seller.Pin = sellerPIN;
    InputSeria: BasicMessages.InputMessage("Seria number (example: 0000000");
        string SeriaInput = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(SeriaInput) || (SeriaInput.Length != 7 || !int.TryParse(SeriaInput, out int sellerSeria)))
        {
            ErrorMessages.InvalidInputMessage(SeriaInput);
            goto InputSeria;
        }
        bool existcustomerSeria = _unitofwork.Customers.ExistCustomerSeria(sellerSeria);
        bool existsellerSeria = _unitofwork.Sellers.ExistSellerSeria(sellerSeria);
        if (existcustomerSeria || existsellerSeria)
        {
            ErrorMessages.ExistMessage("Seria");
            goto InputSeria;
        }
        seller.SeriaNumber = sellerSeria;
        _unitofwork.Sellers.Add(seller);
        _unitofwork.Commit(sellerName, "added");
    }

    public void RemoveCustomer()
    {
        Input: GetAllCustomers();
        BasicMessages.InputMessage("id");
        string input = Console.ReadLine();
        bool isSucceded = int.TryParse(input, out int id);
        if (!isSucceded)
        {
            ErrorMessages.InvalidInputMessage(input);
            goto Input;
        }
        var existCustomer = _unitofwork.Customers.GetCustomerById(id);
        if (existCustomer is null)
        {
            ErrorMessages.NotFoundMessage("customer");
            goto Input;
        }
        _unitofwork.Customers.Delete(existCustomer);
        _unitofwork.Commit(existCustomer.Name, "deleted");
    }

    public void RemoveSeller()
    {
    Input: GetAllSellers();
        BasicMessages.InputMessage("id");
        string input = Console.ReadLine();
        bool isSucceded = int.TryParse(input, out int id);
        if (!isSucceded)
        {
            ErrorMessages.InvalidInputMessage(input);
            goto Input;
        }
        var existSeller = _unitofwork.Sellers.GetSellerById(id);
        if (existSeller is null)
        {
            ErrorMessages.NotFoundMessage("seller");
            goto Input;
        }
        _unitofwork.Sellers.Delete(existSeller);
        _unitofwork.Commit(existSeller.Name, "deleted");

    }
}
