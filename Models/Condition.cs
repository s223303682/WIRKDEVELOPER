using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Condition
    {
        [Key]
        public int ConditionID { get; set; }
        [Required]
        [DisplayName("Condition Name")]
        public string? ConditionName { get; set; }
        public int? AdminID { get; set; }
        [ForeignKey("AdminID")]
        public virtual Administrator? Admin { get; set; }

    }
}
