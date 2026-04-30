using System.ComponentModel.DataAnnotations;

namespace MainStreet.Domain.Enums
{
    public enum PaymentMethod
    {
        [Display(Name = "Cash Payment")]
        Cash = 1,

        [Display(Name = "Credit / Debit Card")]
        Card = 2,
    }
}
