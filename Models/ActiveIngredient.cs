﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace WIRKDEVELOPER.Models
{
	public class ActiveIngredient
	{
		[Key]
		public int ActiveIngredientID { get; set; }
		[Required]
		public string? ActiveIngredientName { get; set; }
	}
}
	