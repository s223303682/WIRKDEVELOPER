namespace WIRKDEVELOPER.Models
{
    public class PatientMedicationNotesViewModel
    {
        public string PatientName { get; set; }
        public List<MedicationViewModel> Medications { get; set; }
    }
    public class MedicationViewModel
    {
        public int OrderMedicationID { get; set; }
        public string PharmacyMedicationName { get; set; }
        public int Quantity { get; set; }
        public string Instructions { get; set; }
        public List<NoteViewModel> Notes { get; set; }
    }

    public class NoteViewModel
    {
        public string NoteText { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
