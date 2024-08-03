using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product : Base
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }
        public Seller Seller { get; set; }
        public int SellerId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
