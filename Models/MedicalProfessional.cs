using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
    public class MedicalProfessional
    {
        [Key]
        public int MedicalProfessionId { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Surname { get; set; }
		[Required]
		public int ContactNumber { get; set; }
		[Required]
		public string? Email { get; set; }
		[Required]
		public string? Specialization { get; set; }
		[Required]
		public int RegistrationNumber { get; set; }
		[Required]
		public string? Address { get; set; }
       
       
    }
}
