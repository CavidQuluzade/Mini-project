﻿using Application.Services.Abstract;
using Core.Entities;
using Core.Messages;
using Data.UnitOfWork.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Application.Services.Concrete;

public class CustomerService : ICustomerService
{
    private readonly Unitofwork _unitofwork;
    public CustomerService(Unitofwork unitofwork)
    {
        _unitofwork = unitofwork;
    }
    public void BuyProduct(int id)
    {
        Order order = new Order();
    InputProduct: GetAllProducts();
        BasicMessages.WantToUseMessage("product");
        string input = Console.ReadLine();
        bool isSucceded = char.TryParse(input, out char answer);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputProduct;
        }
        if (answer != 'y' && answer != 'n')
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputProduct;
        }
        if (answer == 'y')
        {
            FilterProducts();
        }
        else if (answer == 'n')
        {
            GetAllProducts();

        }
        string InputId = Console.ReadLine();
        isSucceded = int.TryParse(InputId, out int productId);
        if (!isSucceded || string.IsNullOrWhiteSpace(InputId))
        {
            ErrorMessages.InvalidInputMessage(InputId);
            goto InputProduct;
        }
        var existProduct = _unitofwork.Products.GetProduct(productId);
        if (existProduct == null)
        {
            ErrorMessages.NotFoundMessage("product");
            goto InputProduct;
        }
    InputAnswer: BasicMessages.WantToBuyMessage("product");
        input = Console.ReadLine();
        isSucceded = char.TryParse(input, out answer);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputAnswer;
        }
        if (answer != 'y' && answer != 'n')
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputProduct;
        }
        if (answer == 'n')
        {
            return;
        }
        order.ProductId = productId;
        order.CustomerId = id;
        order.SellerId = existProduct.SellerId;
    InputCount: BasicMessages.InputMessage("count of product");
        string InputCount = Console.ReadLine();
        isSucceded = int.TryParse(InputCount, out int productCount);
        if (!isSucceded || string.IsNullOrWhiteSpace(InputId))
        {
            ErrorMessages.InvalidInputMessage(InputId);
            goto InputProduct;
        }
        if (existProduct.Count < productCount)
        {
            ErrorMessages.CountIsZeroMessage(existProduct.Name);
            goto InputCount;
        }
        order.TotalAmount = productCount * existProduct.Price;
        existProduct.Count -= productCount;
        _unitofwork.Products.Update(existProduct);
        _unitofwork.Orders.Add(order);
        _unitofwork.Commit(existProduct.Name, "bought");
    }

    public void FilterProducts()
    {
        BasicMessages.InputMessage("something to search");
        string inputSearch = Console.ReadLine();
        var products = _unitofwork.Products.SearchProduct(inputSearch);
        if (products == null)
        {
            ErrorMessages.NotFoundMessage("product");
            return;
        }

        foreach (var product in products)
        {
            Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Count: {product.Count} | Category: {product.Category.Name}");
        }
    }



    public void GetBoughtProducts(int id)
    {
        if (_unitofwork.Orders.GetOrdersCountByCustomer(id) <= 0)
        {
            ErrorMessages.CountIsZeroMessage("orders");
        }
        else
        {
            foreach (var order in _unitofwork.Orders.GetOrdersByCustomer(id))
            {
                Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
            }
        }
    }

    public void GetProductsByDate(int id)
    {
    InputDate: BasicMessages.InoutDateMessage();
        string input = Console.ReadLine();
        bool isSucceded = DateTime.TryParseExact(input, format: "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date);
        if (!isSucceded || string.IsNullOrWhiteSpace(input))
        {
            ErrorMessages.InvalidInputMessage(input);
            goto InputDate;
        }
        var orders = _unitofwork.Orders.GetOrdersByDateWithCustomerId(date, id);
        foreach (var order in orders)
        {
            Console.WriteLine($"Id: {order.Id} | Total: {order.TotalAmount} | Seller: {order.Sellers.Surname} {order.Sellers.Name} | Customer: {order.Customers.Surname} {order.Customers.Name} | Product: {order.Products.Name} - {order.Products.Price} | Date: {order.CreatedDate}");
        }
    }

    public void GetAllProducts()
    {
        foreach(var product in _unitofwork.Products.GetAllProducts())
        {
            Console.WriteLine($"Id: {product.Id} | Name: {product.Name} | Price: {product.Price} | Count: {product.Count} | Category: {product.Category.Name}");
        }
    }
}
    
