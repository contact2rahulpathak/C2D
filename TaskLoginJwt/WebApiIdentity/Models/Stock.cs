using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class Stock
    {
        public int Id { get; set; }
        //[Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }
        public string Qty { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }
       

    }
}
