using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class OrderProduct
    {
        public string ProductId { get; set; }
        public Product Product { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }
    }
}
