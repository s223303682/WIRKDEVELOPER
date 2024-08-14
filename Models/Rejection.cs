using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;
namespace WIRKDEVELOPER.Models
{
    public class Rejection
    {
        [Key]
        public int RejectID {  get; set; }

        [Required]
        public string Description { get; set; }

    }
}
