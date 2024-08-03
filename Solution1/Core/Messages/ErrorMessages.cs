﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Messages
{
    public static class ErrorMessages
    {
        public static void ErrorMessage() => Console.WriteLine("Error happen");
        public static void InvalidInputMessage(string name) => Console.WriteLine($"{name} is invalid. Check {name} for mistake");
        public static void NotFoundMessage(string name) => Console.WriteLine($"{name} not found");
        public static void CountIsZeroMessage(string name) => Console.WriteLine($"There no {name} in base.");
        public static void ExistMessage(string name) => Console.WriteLine($"This {name} already exists");
    }
}
