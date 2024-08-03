using Core.Entities.UserEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Seller : Person
    {
        public string Phone { get; set; }
        public string Pin { get; set; }
        public int SeriaNumber { get; set; }
        public ICollection<Order> Order { get; set; }
        public ICollection<Product> Product { get; set; }
    }
}
