using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class OperationTheatre
	{
		[Key]
		public int TheatreID { get; set; }
		[Required]
		public string? TheatreName { get; set; }
	}
}
