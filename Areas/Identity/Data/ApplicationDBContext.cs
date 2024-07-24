using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WIRKDEVELOPER.Areas.Identity.Data;
using WIRKDEVELOPER.Models;

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
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
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
    public DbSet<Admin> admins { get; set; }
    public DbSet<Bed> beds { get; set; }
    public DbSet<Condition> conditions { get; set; }
    public DbSet<Patient> patients { get; set; }
    public DbSet<PatientCondition> patientConditions { get; set; }
    public DbSet<PatientVisit> patientVisits { get; set;}
    public DbSet<PatientVitals> patientVitals { get; set; }
    public DbSet<Vitals> vitals { get; set; }
    public DbSet<Prescription> prescriptions { get; set; }
    public DbSet<Medication> medications { get; set; }
    public DbSet<BookSurgery> bookSurgeries { get; set; }
    public DbSet<Stock> stocks { get; set; }
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
}
