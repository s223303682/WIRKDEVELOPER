using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class Active
    {
        [Key]
        public int ActiveID { get; set; }
        [Required]
        public string ActiveName { get; set; }
    }
}
