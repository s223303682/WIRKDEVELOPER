using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;
using WIRKDEVELOPER.Models.Account;
using Microsoft.EntityFrameworkCore;
using WIRKDEVELOPER.Models.Admin;
using WIRKDEVELOPER.Models.PatientHistory;

namespace WIRKDEVELOPER.Areas.Identity.Data;

public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ChronicMedication>()
            .HasOne(a => a.Active) // Assuming you have a navigation property for Active
            .WithMany() // Adjust accordingly if Active has multiple Allergies
            .HasForeignKey(a => a.ActiveID);

        builder.Entity<Addm>()
      .HasOne(a => a.AnAllergies)
      .WithMany() // Assuming AnAllergies can have multiple related Addms
      .HasForeignKey(a => a.AnAllergiesID)
      .OnDelete(DeleteBehavior.Cascade); // Set delete behavior according to your needs

        builder.Entity<AnAllergies>()
            .HasOne(a => a.Active) // Assuming you have a navigation property for Active
            .WithMany() // Adjust accordingly if Active has multiple Allergies
            .HasForeignKey(a => a.ActiveID);
        builder.Entity<Addm>()
         .HasOne(a => a.Patient)
         .WithMany() // Specify if the reverse navigation exists
         .HasForeignKey(a => a.PatientID);

        builder.Entity<Addm>()
            .HasOne(a => a.AnCurrentMedication)
            .WithMany() // Specify if the reverse navigation exists
            .HasForeignKey(a => a.AnCurrentMedicationID);

        builder.Entity<Addm>()
            .HasOne(a => a.TreatmentCode)
            .WithMany() // Specify if the reverse navigation exists
            .HasForeignKey(a => a.TreatmentCodeID);
        builder.Entity<Booking>()
            .HasOne(b => b.OperationTheatre)
            .WithMany()  // Assuming OperationTheatre does not have a collection of Bookings
            .HasForeignKey(b => b.OperationTheatreID)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

        // Configuring PrescriptionMedication-Prescription relationship
        builder.Entity<PrescriptionMedication>()
            .HasOne(pm => pm.Prescription)
            .WithMany(p => p.PrescriptionMedications)
            .HasForeignKey(pm => pm.PrescriptionID)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

        // Configuring PrescriptionMedication-PharmacyMedication relationship
        builder.Entity<PrescriptionMedication>()
            .HasOne(pm => pm.PharmacyMedication)
            .WithMany()  // Assuming PharmacyMedication does not have a collection of PrescriptionMedications
            .HasForeignKey(pm => pm.PharmacyMedicationID)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes
       builder.Entity<Addm>()
       .HasOne(a => a.Patient)
       .WithMany()
       .HasForeignKey("PatientID"); //


		builder.Entity<OrderCreate>()
		.HasOne(o => o.Patient)
		.WithMany() // Specify the relationship if it's one-to-many, or adjust accordingly
		.HasForeignKey(o => o.AddmID)
		.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<OrderCreate>()
			.HasOne(o => o.PharmacyMedication)
			.WithMany() // Adjust relationship if it's different
			.HasForeignKey(o => o.PharmacyMedicationID)
			.OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OrderMedication>()
            .HasMany(om => om.notes)
            .WithOne(n => n.OrderMedication)
            .HasForeignKey(n => n.OrderMedicationID);

      



        //base.OnModelCreating(builder);

        // Apply additional configurations if needed
        builder.ApplyConfiguration(new applicationUserEntityConfiguration());
    }

    public class applicationUserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName).HasMaxLength(255);
            builder.Property(u => u.LastName).HasMaxLength(255);

        }
    }

    public DbSet<Models.Administrator> admins { get; set; }
    public DbSet<BookingNewPatient> bookingNewPatients { get; set; }
    public DbSet<MedicationPrescription> medicationPrescriptions { get; set; }
    public DbSet<PrescriptionMedication> prescriptionMedications { get; set; }
  
    public DbSet<PrescriptionViewModel> prescriptionViewModels { get; set; }
   // public DbSet<Alert> Alerts { get; set; }
    public DbSet<AlertMedication> AlertMedications { get; set; }
   
    public DbSet<Bed> beds { get; set; }
    public DbSet<Ward> ward { get; set; }
    public DbSet<Condition> conditions { get; set; }
    public DbSet<Patient> patients { get; set; }
    public DbSet<PatientCondition> patientConditions { get; set; }
    public DbSet<PatientVisit> patientVisits { get; set;}
    public DbSet<PatientVitals> patientVitals { get; set; }
    public DbSet<Vitals> vitals { get; set; }
    public DbSet<Prescription> prescriptions { get; set; }
    public DbSet<Medication> medications { get; set; }
    public DbSet<TreatmentCode> treatmentCodes { get; set; }
    public DbSet<BookingTreatmentCode> BookingTreatmentCode { get; set; }
    
    public DbSet<Admission> admission { get; set; } 
    public DbSet<Bed> bed { get; set; }
    public DbSet<MedicationAdministration> medAdmin { get; set; }
    public DbSet<ViewPatient> viewrecords { get; set; }
    public DbSet<AnAllergies> anallergies { get; set; }
    public DbSet<AnVitals> anvitals { get; set; }
    public DbSet<AnConditions> anconditions { get; set; }
    public DbSet<AnCurrentMedication> ancurrentmedication { get; set; }
    public DbSet<Order> order { get; set; }
    public DbSet<VitalRanges> vitalranges { get; set; }
    public DbSet<Notes> notes { get; set; }
   
    public DbSet<Booking> bookings { get; set; }
    public DbSet<DischargePatient> discharge { get; set; }
    public DbSet<MedicalProfessional> medicalProfessionals { get; set; }
    public DbSet<DosageForm>dosageForms { get; set; }
    public DbSet<ConditionDiagnosis>conditionDiagnoses { get; set; }
    public DbSet<ActiveIngredient>activeIngredients { get; set; }
    public DbSet<Contra_indication>contra_Indications { get; set; }
    public DbSet<OperationTheatre>operationTheatres { get; set; }
    public DbSet<PharmacyMedication>pharmacyMedications { get; set; }
    public DbSet<DayHospital>dayHospitals { get; set; }
    public DbSet<Rejection> rejections { get; set; }
    public DbSet<Active> active { get; set; }
    public DbSet<Schedule> schedules { get; set; }
    public DbSet<PharmStock> pharmStock { get; set; }

    public DbSet<Models.Account.Admin> Administrators { get; set; }

    public DbSet<Surgeon> Surgeons { get; set; }

    public DbSet<Anaesthesiologist> Anaesthesiologists { get; set; }

    public DbSet<Nurse> Nurses { get; set; }

    public DbSet<Pharmacist> Pharmacists { get; set; }
    public DbSet<PharmacyMedicationIngredient> PharmacyMedicationIngredients {  get; set; }
    public DbSet<Ranges> ranges {  get; set; }
    public DbSet<Addm> addm {  get; set; }
    public DbSet<StockReceived> StockReceiveds { get; set; }
    public DbSet<OrderItems> orderItems { get; set; }
    public DbSet<OrderMedication> ordermedication { get; set; }
    public DbSet<OrderCreate> ordercreate { get; set; }
    public DbSet<NotesOfOrders> notesoforders { get; set; }
    public DbSet<ChronicMedication> chronicmedication { get; set; }
    public DbSet<ContraIndication> contraindication{ get; set; }
    public DbSet<PatientAllergies> PatientAllergies{ get; set; }
    public DbSet<PatientChronicCondition> PatientChronicConditions{ get; set; }
    public DbSet<PatientMedication> PatientMedications{ get; set; }
    public DbSet<MedicationActive> MedicationActive { get; set; }

}
