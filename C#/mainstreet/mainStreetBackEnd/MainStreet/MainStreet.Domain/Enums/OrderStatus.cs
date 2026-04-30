using System.ComponentModel.DataAnnotations;

namespace MainStreet.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Awaiting Confirmation")]
        Pending = 0,

        [Display(Name = "Order Accepted")]
        Confirmed = 1,

        [Display(Name = "In the Kitchen")]
        Cooking = 2,

        [Display(Name = "Ready for Pickup")]
        Ready = 3,

        [Display(Name = "Out for Delivery")]
        Dispatched = 4,

        [Display(Name = "Enjoy your Meal!")]
        Delivered = 5,

        [Display(Name = "Order Cancelled")]
        Cancelled = 6
    }
}
