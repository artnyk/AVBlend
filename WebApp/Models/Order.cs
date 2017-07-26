using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
        public int ModelID { get; set; }
        public int UserID { get; set; }

        public Model Model { get; set; }
        public User User { get; set; }
    }
}
