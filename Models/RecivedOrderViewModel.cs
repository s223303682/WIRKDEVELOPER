using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WIRKDEVELOPER.Models
{
    public class RecivedOrderViewModel
    {
        [Required(ErrorMessage = "Please select a medication.")]
        public int SelectedMedicationId { get; set; }
        public List<SelectListItem> Medications { get; set; }

        [Required(ErrorMessage = "Please enter the quantity received.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int QuantityReceived { get; set; }

        // public int StockReceivedId { get; set; }

        public int StockOrderId { get; set; }
        public string MedicationName { get; set; }

        public DateTime DateReceived { get; set; }
        public string Status { get; set; }
    }
}
