namespace WIRKDEVELOPER.Models
{
    public class PharmacyMedicationViewModel
    {
        public int PharmacyMedicationId { get; set; }
        public string PharmacyMedicationName { get; set; }
        public int QuantityOnHand { get; set; }
        public int ReorderLevel { get; set; }
        public int OrderQuantity { get; set; }
        public bool IsSelected { get; set; }
        public string Email { get; set; }
    }
}
