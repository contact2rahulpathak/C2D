﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public ICollection<OrderProduct> OrderProduct { get; set; }

    }
}