using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WIRKDEVELOPER.Models
{
	public class ConditionDiagnosis
	{
		[Key]
		public int ConditionDiagnosisID { get; set; }
		[Required]
		public string? ConditionDiagnosisName { get; set;}
	}
}
