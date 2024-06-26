﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WIRKDEVELOPER.Models
{
    public class DischargePatient
    {
        public int DischargePatientId { get; set; }
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
       
        public int MedicationID { get; set; }
        [ForeignKey("MedicationID")]
        public int staffId { get; set; }
        [ForeignKey("StaffId")]
        public int AdmissionId { get; set; }
        [ForeignKey("AdmissionId")]
        public DateTime DischargeDate { get; set; }
    }
}
