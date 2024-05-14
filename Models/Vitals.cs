using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Drawing2D;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WIRKDEVELOPER.Models
{
    public class Vitals
    {
        [Key]
        public int VitalID { get; set; }
        [Required]
        [DisplayName("Vital Name")]
        public string? VitalName { get; set; }
        [Required]
        [DisplayName("Low Limit")]
        public int? LowLimit { get; set; }
        [Required]
        [DisplayName("High Limit")]
        public int? HighLimit { get; set; }
        public string PatientName { get; set; }
        public DateTime VisitDate { get; set; }
        public TimeSpan? VisitTime { get; set; }
        public int HeartRate { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public int Temperature { get; set; }
        public int RespiratoryRate { get; set; }
        public  int BloodPressure { get; set; }
        public string BpPosition { get; set; }

    }
}
