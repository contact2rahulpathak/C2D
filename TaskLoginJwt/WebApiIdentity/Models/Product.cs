using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class Product
    {
        //    [Key]
        //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<Stock> Stock { get; set; }
        public ICollection<OrderProduct> OrderProduct { get; set; }
    }
}
