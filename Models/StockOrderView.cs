namespace WIRKDEVELOPER.Models
{
    public class StockOrderView
    {
        public int StockOrderId { get; set; }
        public string MedicationName { get; set; }
        public int OrderQuantity { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
    }
}
