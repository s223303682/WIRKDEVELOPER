using System.ComponentModel.DataAnnotations;
using WIRKDEVELOPER.Areas.Identity.Data;

namespace WIRKDEVELOPER.Models.Account
{
    public class Surgeon
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*Required")]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Invalid HPCSA Number Number")]
        public string SurgeonLicenseNumber { get; set; }
        [Required]
        public string Specialization {  get; set; }

        //[Required(ErrorMessage = "*Required")]
        //[FutureDate(ErrorMessage = "The License Expiry Date must be a date in the future.")]
        //public DateTime LicenseExpiryDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
