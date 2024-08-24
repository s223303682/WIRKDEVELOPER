using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class DayHospital
	{
		[Key]
		public int DayHospitalID { get; set; }
        [Required]
        public string? Name { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Suburb { get; set; }
        public string Zip { get; set; }
        [Required]
		[DisplayName("Contact Number")]
		public string? ContactNumber { get; set; }
		[Required]
		[DisplayName("Email Address")]
		public string? emailAddress { get; set; }
		[Required]
		[DisplayName("Practice Manager")]
		public string? PracticeManager { get; set; }
		[Required]
		[DisplayName("Phurshase Manager Email")]
		public string? PhurshaseManagerEmail { get; set; }
	}
}
