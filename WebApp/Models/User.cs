using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Webmoney_Wallet { get; set; }
        public string IdentityUserId { get; set; }

        public List<Order> Orders { get; set; }
    }
}
