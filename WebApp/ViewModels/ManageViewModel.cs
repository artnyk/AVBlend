using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class ManageViewModel
    {
        public int UserID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Value {0} must contain more than {2} simbols.", MinimumLength = 2)]
        public string Login { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value {0} must contain more than {2} simbols.", MinimumLength = 2)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Value {0} must contain more than {2} simbols.", MinimumLength = 2)]
        public string Webmoney_Wallet { get; set; }
    }
}