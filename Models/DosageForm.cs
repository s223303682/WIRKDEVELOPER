using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class DosageForm
	{
		[Key]
		public int DosageFormID { get; set; }
		[Required]
		public string? DosageFormName { get; set; }
	}
}
