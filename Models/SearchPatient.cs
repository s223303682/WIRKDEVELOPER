using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class SearchPatient
    {
        [Key]
        public int SearchPatientID { get; set; }
        public string SearchTerm { get; set; }
        public IEnumerable<Addm> addms { get; set; }
    }
}
