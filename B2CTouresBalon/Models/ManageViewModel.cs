using System.ComponentModel.DataAnnotations;

namespace B2CTouresBalon.Models
{
    public class ManageViewModel
    {

        public decimal CustomerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "First Name")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Credit Card Type")]
        public string CreditCardType { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        public bool Status { get; set; }
    }
}