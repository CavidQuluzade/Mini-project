using Application.Services.Concrete;
using Core.Constats.UserOperaions;
using Core.Entities;
using Core.Messages;
using Data;
using Data.Intializer;
using Data.UnitOfWork.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Presentation;

public class Program
{
    private static readonly Unitofwork _unitofwork = new Unitofwork();
    private static readonly AdminService _adminService = new AdminService(_unitofwork);
    private static readonly CustomerService _customerService = new CustomerService(_unitofwork);
    private static readonly SellerService _sellerService = new SellerService(_unitofwork);
    static void Main(string[] args)
    {
        Account: bool exit = true;
        bool exitAccount = true;
        ConsoleCommerceAppDbContext db = new ConsoleCommerceAppDbContext();
        Initializer.SeedData();
        InputEmail: BasicMessages.InputMessage("email");
        string email = Console.ReadLine();
        if (!email.Contains("@")) 
        {
            ErrorMessages.InvalidInputMessage(email);
            goto InputEmail;
        }
        
        BasicMessages.InputMessage("password");
        string password = Console.ReadLine();
        var existAdmin = db.Admins.FirstOrDefault(x => x.Email == email);
        var existSeller = db.Sellers.FirstOrDefault(x => x.Email == email);
        var existCustommer  = db.Customers.FirstOrDefault(x => x.Email == email);
        if (existAdmin == null && existSeller == null && existCustommer == null) 
        {
            ErrorMessages.NotFoundMessage("account");
            goto InputEmail;
        }

        if (existAdmin != null)
        {
            PasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
            passwordHasher.VerifyHashedPassword(existAdmin, existAdmin.Password, password);
            if (passwordHasher == null)
            {
                ErrorMessages.InvalidInputMessage("account");
                goto Account;
            }
            while (exit && exitAccount)
            {
                ShowAdminMenu();
                BasicMessages.InputMessage("choice");
                string input = Console.ReadLine();
                bool isSucceded = int.TryParse(input, out int choice);
                if (!isSucceded)
                {
                    ErrorMessages.InvalidInputMessage(input);
                }
                switch ((AdminOperations)choice)
                {
                    case AdminOperations.Exit:
                        exit = false;
                        break;
                    case AdminOperations.Signout:
                        exitAccount = false;
                        goto Account;
                    case AdminOperations.GetAllCustomers:
                        _adminService.GetAllCustomers();
                        break;
                    case AdminOperations.GetAllSellers:
                        _adminService.GetAllSellers();
                        break;
                    case AdminOperations.GetOrders:
                        _adminService.GetOrders();
                        break;
                    case AdminOperations.GetOrdersByCustomer:
                        _adminService.GetOrdersByCustomer();
                        break;
                    case AdminOperations.GetOrdersByDate:
                        _adminService.GetOrdersByDate();
                        break;
                    case AdminOperations.GetOrdersBySeller:
                        _adminService.GetOrdersBySeller();
                        break;
                    case AdminOperations.AddCategory:
                        _adminService.AddCategory();
                        break;
                    case AdminOperations.AddCustomer:
                        _adminService.AddCustomer();
                        break;
                    case AdminOperations.AddSeller:
                        _adminService.AddSeller();
                        break;
                    case AdminOperations.RemoveCustomer:
                        _adminService.RemoveCustomer();
                        break;
                    case AdminOperations.RemoveSeller:
                        _adminService.RemoveSeller();
                        break;
                    default:ErrorMessages.InvalidInputMessage(input);
                        break;
                }
            }
        }
        if (existSeller != null)
        {
            PasswordHasher<Seller> passwordHasher = new PasswordHasher<Seller>();
            passwordHasher.VerifyHashedPassword(existSeller, existSeller.Password, password);
            if (passwordHasher == null)
            {
                ErrorMessages.InvalidInputMessage("account");
                goto Account;
            }
            while (exit && exitAccount)
            {
                ShowSellerMenu();
                BasicMessages.InputMessage("choice");
                string input = Console.ReadLine();
                bool isSucceded = int.TryParse(input, out int choice);
                if (!isSucceded)
                {
                    ErrorMessages.InvalidInputMessage(input);
                }
                switch ((SellerOperations)choice)
                {
                    case SellerOperations.Exit:
                        exit = false;
                        break;
                    case SellerOperations.SignOut:
                        exitAccount = false;
                        goto Account;
                    case SellerOperations.AddProduct:
                        _sellerService.AddProduct(existSeller.Id);
                        break;
                    case SellerOperations.ChangeProductCount:
                        _sellerService.ChangeProductCount();
                        break;
                    case SellerOperations.GetIncome:
                        _sellerService.GetIncome(existSeller.Id);
                        break;
                    case SellerOperations.GetSelledProduct:
                        _sellerService.GetSelledProduct(existSeller.Id);
                        break;
                    case SellerOperations.GetSelledProductByDate:
                        _sellerService.GetSelledProductByDate(existSeller.Id);
                        break;
                    case SellerOperations.RemoveProduct:
                        _sellerService.RemoveProduct(existSeller.Id);
                        break;
                    case SellerOperations.SearchProducts:
                        _sellerService.SearchProducts();
                        break;
                    case SellerOperations.GetAllProducts:
                        _sellerService.GetAllProducts(existSeller.Id);
                        break;
                    default:
                        ErrorMessages.InvalidInputMessage(input);
                        break;
                }
            }
        }
        if (existCustommer != null)
        {
            PasswordHasher<Customer> passwordHasher = new PasswordHasher<Customer>();
            passwordHasher.VerifyHashedPassword(existCustommer, existCustommer.Password, password);
            if (passwordHasher == null)
            {
                ErrorMessages.InvalidInputMessage("account");
                goto Account;
            }
            while (exit && exitAccount)
            {
                ShowCustommerMenu();
                BasicMessages.InputMessage("choice");
                string input = Console.ReadLine();
                bool isSucceded = int.TryParse(input, out int choice);
                if (!isSucceded)
                {
                    ErrorMessages.InvalidInputMessage(input);
                }
                switch ((CustomerOperations)choice)
                {
                    case CustomerOperations.Exit:
                        exit = false;
                        break;
                    case CustomerOperations.Signout:
                        exitAccount = false;
                        goto Account;

                    case CustomerOperations.BuyProduct:
                        _customerService.BuyProduct(existCustommer.Id);
                        break;
                    case CustomerOperations.FilterProducts:
                        _customerService.FilterProducts();
                        break;
                    case CustomerOperations.GetBoughtProducts:
                        _customerService.GetBoughtProducts(existCustommer.Id);
                        break;
                    case CustomerOperations.GetProductsByDate:
                        _customerService.GetProductsByDate(existCustommer.Id);
                        break;
                    case CustomerOperations.GetAllProducts:
                        _customerService.GetAllProducts();
                        break;
                    default:
                        ErrorMessages.InvalidInputMessage(input);
                        break;
                }
            }
        }



    }
    static void ShowAdminMenu()
    {
        Console.WriteLine("0   Exit");
        Console.WriteLine("1   Sign out");
        Console.WriteLine("2   Get all customers");
        Console.WriteLine("3   Get all sellers");
        Console.WriteLine("4   Get orders");
        Console.WriteLine("5   Get orders by customer");
        Console.WriteLine("6   Get orders by date");
        Console.WriteLine("7   Get orders by seller");
        Console.WriteLine("8   Add category");
        Console.WriteLine("9   Add customer");
        Console.WriteLine("10  Add seller");
        Console.WriteLine("11  Remove customer");
        Console.WriteLine("12  Remove seller");
    }
    static void ShowSellerMenu()
    {
        Console.WriteLine("0   Exit");
        Console.WriteLine("1   Sign out");
        Console.WriteLine("2   Add product");
        Console.WriteLine("3   Change product count");
        Console.WriteLine("4   Get income");
        Console.WriteLine("5   Get sold products");
        Console.WriteLine("6   Get sold products by date");
        Console.WriteLine("7   Remove product");
        Console.WriteLine("8   Search product");
        Console.WriteLine("9   Get all products");
    }
    static void ShowCustommerMenu()
    {
        Console.WriteLine("0   Exit");
        Console.WriteLine("1   Sign out");
        Console.WriteLine("2   BuyProduct");
        Console.WriteLine("3   FilterProducts");
        Console.WriteLine("4   GetBoughtProducts");
        Console.WriteLine("5   GetProductsByDate");
        Console.WriteLine("6   Get all products");
    }
}
