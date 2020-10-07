using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models.Company
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int Eid { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        [ForeignKey("Department")]
        public int Did { get; set; }
        public Department Department { get; set; }
        public string Id { get; set; }
    }
}
