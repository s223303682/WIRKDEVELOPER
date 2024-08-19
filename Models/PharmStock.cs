using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
    public class PharmStock
    {
        [Key]
        public int PharmStockId { get; set; }
        [Required]
        
        public int QuantityOrdered { get; set; }

        [Required]
        public int QuantityRecieved { get; set; }
		[Required]
		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;


	}
}
