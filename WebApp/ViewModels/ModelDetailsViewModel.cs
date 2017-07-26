using System;

namespace WebApp.ViewModels
{
    public class ModelDetailsViewModel
    {
        public int ModelID { get; set; }
        public string Name { get; set; }
        public DateTime DateCreation { get; set; }
        public int SoldNum { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string SellerName { get; set; }
    }
}