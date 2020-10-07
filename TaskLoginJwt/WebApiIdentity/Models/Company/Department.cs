using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models.Company
{
    public class Department
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Required]
        public int Did { get; set; }

        [Required]
        public string DName { get; set; }
        public string Description { get; set; }

        public IEnumerable<Employee> Employees { get; set; }

        public string Id { get; set; }
    }
}

