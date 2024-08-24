using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class StockReceived
    {
        public int StockReceivedId { get; set; }

        [Required]
        public int QuantityRecived { get; set; }
        [Required]
        public string Status = "Recieved";

        [DisplayName("Stock Ordered")]
        [Required(ErrorMessage = "Required")]
        [ForeignKey("PharmStockId")]
        public int PharmStockId { get; set; }
        public virtual PharmStock PharmStock { get; set; }

    }
}
