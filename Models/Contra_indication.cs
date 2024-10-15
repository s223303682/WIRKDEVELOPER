using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace WIRKDEVELOPER.Models
{
	public class Contra_indication
	{
		[Key]
		public int ContraIndication { get; set; }
		[Required]
		public int ActiveIngredientID { get; set; }
		[ForeignKey("ActiveIngredientID")]
		public virtual ActiveIngredient? ActiveIngredient {  get; set; }
		[Required]
		public int ConditionDiagnosisID { get; set; }
		[ForeignKey("ConditionDiagnosisID")]
		public virtual ConditionDiagnosis? ConditionDiagnosis { get; set; }
	}
}
