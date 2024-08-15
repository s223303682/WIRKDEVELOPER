using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class PharmActive
    {
        [Key]
        public int ActiveID { get; set; }
        [Required]
        public string ActiveName { get; set; }
    }
}
