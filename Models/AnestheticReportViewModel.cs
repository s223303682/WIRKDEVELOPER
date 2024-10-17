namespace WIRKDEVELOPER.Models
{
    public class AnestheticReportViewModel
    {
        public string Date { get; set; }
        public string PatientName { get; set; }
        public string MedicationName { get; set; }
        public int Quantity { get; set; }
    }
    public class MedicineSummaryViewModel
    {
        public string MedicationName { get; set; }
        public int QuantityOrdered { get; set; }
    }
}
