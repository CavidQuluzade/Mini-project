using Core.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.Intializer
{
    public static class Initializer
    {
        private static readonly ConsoleCommerceAppDbContext _context;
        static Initializer()
        {
            _context = new ConsoleCommerceAppDbContext();
        }
        public static void SeedData()
        {
            SeedAdmin();
        }
        private static void SeedAdmin()
        {
            if (!_context.Admins.Any())
            {
                Admin admin = new Admin();
                admin.Surname = "Admin";
                admin.Name = "Admin";
                admin.Email = "admin@admin.com";

                PasswordHasher<Admin> passwordHasher = new PasswordHasher<Admin>();
                admin.Password = passwordHasher.HashPassword(admin, "Admin123");
                _context.Admins.Add(admin);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception)
                {

                    throw new Exception("error");
                }
            }
        }

    }
}
