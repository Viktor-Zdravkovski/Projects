using System.ComponentModel.DataAnnotations;

namespace MainStreet.Domain.Enums
{
    public enum Roles
    {
        [Display(Name = "Admin")]
        Admin = 1,
        [Display(Name = "Staff")]
        Staff = 2,
        [Display(Name = "User")]
        User = 3
    }
}
