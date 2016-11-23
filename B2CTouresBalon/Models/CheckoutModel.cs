using System.ComponentModel.DataAnnotations;

namespace B2CTouresBalon.Models
{
    public class CheckoutModel
    {
        public Cart Cart { get; set; }

        public int CustomerId { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "First Name")]
        public string Firstname{ get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Total a Pagar")]
        public double Total{ get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Credit Card Type")]
        public string CreditCardType { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Credit Card")]
        public string Creditcard { get; set; }

    }
}