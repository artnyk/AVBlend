using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApp.ViewModels
{
    public class ModelEditViewModel
    {
        public int ModelID { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value {0} must contain more than {2} simbols.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public int CategoryID { get; set; }
        public SelectList Catigories { get; set; }
    }
}