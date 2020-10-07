using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models.MasterDetalModel
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public int OrderNo { get; set; }
        public int CustomerId { get; set; }
        public int PMethod { get; set; }
        public int GTotal { get; set; }


      //  [NotMapped]
      //  public string DeletedOrderItemIDs { get; set; }

        public virtual Customer Customer { get; set; }
       
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
