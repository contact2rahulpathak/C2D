using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models.MasterDetalModel
{
    public class Customer
    { 
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int CustomerID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
    }
}
