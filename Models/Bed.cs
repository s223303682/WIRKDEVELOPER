using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace WIRKDEVELOPER.Models
{
    public class Bed
    {
        [Key]
        public int BedID { get; set; }
        [Required]
        [DisplayName(" Bed Number")]
        public int BedNumber { get; set; }
          
    }
}
