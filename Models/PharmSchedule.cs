using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class PharmSchedule
    {
        [Key]
        public int ScheduleId { get; set; }
        [Required]
        public string ScheduleName { get; set; }

    }
}
