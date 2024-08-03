using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Constats.UserOperaions
{
    public enum AdminOperations
    {
        Exit,
        Signout,
        GetAllCustomers,
        GetAllSellers,
        GetOrders,
        GetOrdersByCustomer,
        GetOrdersByDate,
        GetOrdersBySeller,
        AddCategory,
        AddCustomer,
        AddSeller,
        RemoveCustomer,
        RemoveSeller
    }
}
