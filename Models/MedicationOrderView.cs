namespace WIRKDEVELOPER.Models
{
    public class MedicationOrderView
    {
        public List<PharmacyMedicationViewModel> Medications { get; set; }
        public List<StockOrderView> StockOrders { get; set; }
        public MedicationOrderView()
        {
            StockOrders = new List<StockOrderView>();
        }
    }
}
