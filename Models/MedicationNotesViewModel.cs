namespace WIRKDEVELOPER.Models
{
    public class MedicationNotesViewModel
    {
        public string PatientName { get; set; }
        public string MedicationName { get; set; }
        public string Notes { get; set; }
        public DateTime? Date { get; set; } = DateTime.Now;
        public int? AnOrderID { get; set; }
    }
}
