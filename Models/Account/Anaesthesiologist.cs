using System.ComponentModel.DataAnnotations;
using WIRKDEVELOPER.Areas.Identity.Data;

namespace WIRKDEVELOPER.Models.Account
{
    public class Anaesthesiologist
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid HPCSA Number Number")]
        public string AnaesthesiologistLicenseNumber { get; set; }

        //[Required(ErrorMessage = "*Required")]
        //[FutureDate(ErrorMessage = "The License Expiry Date must be a date in the future.")]
        //public DateTime LicenseExpiryDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
