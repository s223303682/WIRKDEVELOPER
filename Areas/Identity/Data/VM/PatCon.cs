using WIRKDEVELOPER.Models;
using WIRKDEVELOPER.Models.Admin;

namespace WIRKDEVELOPER.Areas.Identity.Data.VM
{
	public class PatCon
	{
		public IEnumerable<PatientCondition> PatConditions { get; set; }
		public IEnumerable<AnAllergies> Allergies { get; set; }
		public IEnumerable<CurrentMedication> Medications { get; set; }

	}
	public class ReportList
	{
		public IEnumerable<Report> Reports { get; set; }
		public string Nurse { get; set; }
		public string from { get; set; }
		public string to { get; set; }

	}

	public class SubReport
	{
		public Patient Patient { get; set; }
		public IEnumerable<PatientVitals> PatientVitals { get; set; }
		public IEnumerable<AnAllergies> Allergies { get; set; }
		public IEnumerable<CurrentMedication> CurrentMedications { get; set; }
		public IEnumerable<PatientCondition> PatientConditions { get; set; }

	}
	public class Report
	{
		public int PatientID { get; set; }
		public string PatientName { get; set; }
		public string Date { get; set; }
		public string Medication { get; set; }
		public int MedicationId { get; set; }
		public int QTY { get; set; }
		public string Time { get; set; }

	}
	public class PresAd
	{
		public string PatientName { get; set; }
		public string PatientID { get; set; }
		public string Bed { get; set; }
		public string Ward { get; set; }
		public int PresId { get; set; }

	}
	public class MedsID
	{
		public int MedId { get; set; }
		public int Quantity { get; set; }
	}

}
