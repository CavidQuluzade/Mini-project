using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public static class BasicMessages
    {
        public static void InputMessage(string name) => Console.WriteLine($"Input {name}");
        public static void SuccessMessage(string name, string type) => Console.WriteLine($"{name} succesfully {type}");
        public static void InoutDateMessage() => Console.WriteLine($"Input date (format: dd/MM/yyyy)");
        public static void WantToUseMessage(string name) => Console.WriteLine($"Do you want to search {name} (y/n)");
        public static void WantToBuyMessage(string name) => Console.WriteLine($"Do you want to buy {name} (y/n)");
    }
}
