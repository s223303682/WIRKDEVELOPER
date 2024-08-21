using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class Administrator
    {
        [Key]
        public int AdminID { get; set; }
        [Required]
        [DisplayName("Admin Name")]
        public string? AdminName { get; set; }
        [Required]
        [DisplayName("Admin Surname")]
        public int AdminSurname { get; set; }
        [Required]
        [DisplayName("Contact Number")]
        public string? contactNumber { get; set; }
        [Required]
        [DisplayName("Email address")]
        public string? EmailAddress { get; set; }
    }
}
