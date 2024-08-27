using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class Allergies
    {
        [Key]
        public string AllergiesID { get; set; }
        public string AllergiesName { get; set; }
    }
}
