using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommMVC.Entities
{
    public class Checkout
    {
        public required PaymentType PaymentType { get; set; }
        
        /*
        public string CreditCardNumber { get; set; }
        public string CreditCardName { get; set; }
        public string CreditCardExpiry { get; set; }
        public string CreditCardCvc { get; set; }
        */
        /*
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PaymentType == PaymentType.CreditCard)
            {
                if (string.IsNullOrEmpty(CreditCardNumber))
                    yield return new ValidationResult("Credit card number is required.", new[] { nameof(CreditCardNumber) });

                if (string.IsNullOrEmpty(CreditCardName))
                    yield return new ValidationResult("Cardholder name is required.", new[] { nameof(CreditCardName) });

                if (string.IsNullOrEmpty(CreditCardExpiry))
                    yield return new ValidationResult("Expiry date is required.", new[] { nameof(CreditCardExpiry) });

                if (string.IsNullOrEmpty(CreditCardCvc))
                    yield return new ValidationResult("CVC is required.", new[] { nameof(CreditCardCvc) });
            }
        }
        */
    }
}
