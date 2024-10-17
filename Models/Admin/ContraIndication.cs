using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models.Admin
{
    public class ContraIndication
    {
        [Key]
        public int ContraIndicationID { get; set; }

        [Required]
        [DisplayName("ActiveIngridients")]
        public int? ActiveID { get; set; }
        [ForeignKey("ActiveID")]
        public virtual Active Active { get; set; }

        [Required]
        [DisplayName("Condition")]
        public int? AnConditionsID { get; set; }
        [ForeignKey("AnConditionsID")]
        public virtual AnConditions AnConditions { get; set; }
    }
}
