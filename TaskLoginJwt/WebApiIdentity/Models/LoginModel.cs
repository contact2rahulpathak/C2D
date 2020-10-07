using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiIdentity.Models
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
       // public int RefreshToken { get; set; }

       // public int RefreshTokenExpiryTime { get; set; }
    }
    //public class LoginModel
    //{
    //    [Key]
    //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public long Id { get; set; }
    //    public string UserName { get; set; }
    //    public string Password { get; set; }
    //    public string RefreshToken { get; set; }
    //    public DateTime RefreshTokenExpiryTime { get; set; }
    //}
}
