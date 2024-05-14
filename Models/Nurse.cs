using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class Nurse
    {
        public int NurseID { get; set; }
        public int StaffId { get; set; }
        [ForeignKey("StaffId")]
      
    }
}
