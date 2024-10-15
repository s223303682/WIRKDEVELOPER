namespace WIRKDEVELOPER.Models
{
    public class PharmacistReportViewModel
    {

        public string PharmacistName { get; set; } = "Dorothy Daniels";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ReportGeneratedDate { get; set; } = DateTime.Now;
        public List<PrescriptionReportItem> PrescriptionItems { get; set; }
        public int TotalScriptsDispensed { get; set; }
        public int TotalScriptsRejected { get; set; }
        public List<MedicationSummary> MedicationSummaries { get; set; }
    }
    public class MedicationSummary
    {
        public string MedicationName { get; set; }
        public int TotalQuantityDispensed { get; set; }
    }
    public class PrescriptionReportItem
    {
        public DateTime Date { get; set; }
        public string PatientName { get; set; }
        public string Prescriber { get; set; }
        public string Medication { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }

}
