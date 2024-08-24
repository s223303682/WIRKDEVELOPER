using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class Ward
    {
        [Key]
        public int WardID { get; set; }
        [Required]
        public string? WardName { get; set; }
        [Required]
        public int NoOfBeds { get; set; } 
        public int NurseResponsible { get; set; }
        

    }
}
