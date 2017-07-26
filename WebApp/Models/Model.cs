using System;

namespace WebApp.Models
{
    public class Model
    {
        public int ModelID { get; set; }
        public string Name { get; set; }
        public string Directory { get; set; }
        public DateTime DateCreation { get; set; }
        public int SoldNum { get; set; }
        public decimal Price { get; set; }
        public bool Enabled { get; set; }
        public int CategoryID { get; set; }
        public int UserID { get; set; }

        public Category Category { get; set; }
        public User User { get; set; }
    }
}
