﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using WIRKDEVELOPER.Areas.Identity.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
	public class Notes
	{
		[Required]
		[DisplayName(" Date")]
		public DateTime? Date { get; set; } = DateTime.Now;

		[Required]
		[DisplayName("Patient")]
		public int? AnOrderID { get; set; }
		[ForeignKey("AnOrderID")]
        public virtual Order Order { get; set; }

		[Required]
		[DisplayName("Notes")]
		public string? notes { get; set; }

		[Key]
		public int NotesID { get; set; }
	}
}
