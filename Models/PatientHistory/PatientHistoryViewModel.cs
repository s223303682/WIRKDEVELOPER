using WIRKDEVELOPER.Models.Admin;

namespace WIRKDEVELOPER.Models.PatientHistory
{
    public class PatientHistoryViewModel
    {
        public int PatientID { get; set; }

        public Patient Patient { get; set; } // This property holds the patient information

        public string PatientName { get; set; }

        public List<Active> Allergies { get; set; } // Assuming ActiveIngredient is the model for allergies

        public List<int> SelectedAllergies { get; set; } = new List<int>();// Stores the IDs of selected allergies

        public List<AnConditions> AnConditions { get; set; }

        public List<int> SelectedConditions { get; set; } = new List<int>();

        public List<ChronicMedication> ChronicMedication { get; set; }

        public List<int> SelectedChronicMedications { get; set; } = new List<int>();
    }
}
