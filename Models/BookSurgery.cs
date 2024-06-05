using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class BookSurgery
    {
        [Key]
        public int BookingSurgeryID { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Surname { get; set; }
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? EmailAddress { get; set; }
        [Required]
        public DateOnly? Date { get; set; }
        [Required]
        public TimeOnly? Time { get; set; }
        //[Required]
        //public string? Theatre { get; set; }
        //[ForeignKey("Name")]
        //public virtual Anasthesiologist? Anasthesiologist { get; set; }
        //[Required]
        //public string? Anasthesiologist { get; set; }
        //[ForeignKey("Name")]
        //public virtual Anasthesiologist? Anasthesiologist { get; set; }
        //[Required]
        //public string? TreatmentCode { get; set; }
        //[ForeignKey("Name")]
        //public virtual Anasthesiologist? Anasthesiologist { get; set; }

    }
}
