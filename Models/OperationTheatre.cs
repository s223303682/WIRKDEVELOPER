using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class OperationTheatre
	{
		[Key]
		public int OperationTheatreID { get; set; }
		[Required]
		public string? OperationTheatreName { get; set; } 
	}
}
