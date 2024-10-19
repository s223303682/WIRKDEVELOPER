namespace WIRKDEVELOPER.Models
{
    public class StockOrderView
    {
        public int StockOrderId { get; set; }
        public string PharmacyMedicationName { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }
}
