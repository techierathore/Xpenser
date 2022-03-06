using System.ComponentModel.DataAnnotations;

namespace Xpenser.UI.ViewModels
{
    public class SignUpVm
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This Field Is Required"), EmailAddress]
        public string UserEmail { get; set; }
        [StringLength(10, ErrorMessage = "Mobile No. Can't be more than 10 digits")]
        // [RegularExpression("([0-9]+)", ErrorMessage = "Please enter numeric values only")]
        [RegularExpression(@"(?<!\d)\d{10}(?!\d)", ErrorMessage = "Please enter 10 digits numeric values only")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        public string LoginPass { get; set; }
        [Required(ErrorMessage = "This Field Is Required")]
        [Compare("LoginPass")]
        public string ConfirmPassword { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFirstLogin { get; set; }
    }
}
