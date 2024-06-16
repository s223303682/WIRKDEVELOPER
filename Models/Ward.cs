using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class Ward
    {
        [Key]
        public int WardID { get; set; }
       
        public int NurseID { get; set; }
        [ForeignKey("NurseID")]
        public string WardName { get; set; }

    }
}
