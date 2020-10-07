using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Column(TypeName = "nvarchar(150)")]
        public string FullName { get; set; }
        [Column(TypeName = "nvarchar(500)")]
        public string RefreshToken { get; set; }
        [Column(TypeName = "DateTime2")]
         public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
