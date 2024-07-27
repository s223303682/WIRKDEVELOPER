using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class DayHospitalRecords
	{
		[Key]
		public int HospitalID {  get; set; }
		[Required]
		public string? HospitalName{ get; set;}
		[Required]
		public string? HospitalAddress { get; set; }
		[Required]
		public string? EmailAddress { get; set; }
		[Required]
		public string? contactNumber { get; set; }
		[Required]
		public int HospitalNumber { get; set; }
		[Required]
		public string? PurchaseManagerAddress { get; set; }
	}
}
