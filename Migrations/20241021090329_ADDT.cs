using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WIRKDEVELOPER.Migrations
{
    public partial class ADDT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "active",
                columns: table => new
                {
                    ActiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_active", x => x.ActiveID);
                });

            migrationBuilder.CreateTable(
                name: "activeIngredients",
                columns: table => new
                {
                    ActiveIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_activeIngredients", x => x.ActiveIngredientID);
                });

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminSurname = table.Column<int>(type: "int", nullable: false),
                    contactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "AlertMedications",
                columns: table => new
                {
                    AlertMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertMedications", x => x.AlertMedicationID);
                });

            migrationBuilder.CreateTable(
                name: "anconditions",
                columns: table => new
                {
                    AnConditionsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Diagnose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    chroniccode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anconditions", x => x.AnConditionsID);
                });

            migrationBuilder.CreateTable(
                name: "anvitals",
                columns: table => new
                {
                    AnVitalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Vital = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anvitals", x => x.AnVitalID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bed",
                columns: table => new
                {
                    BedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BedNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bed", x => x.BedID);
                });

            migrationBuilder.CreateTable(
                name: "conditionDiagnoses",
                columns: table => new
                {
                    ConditionDiagnosisID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionDiagnosisName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditionDiagnoses", x => x.ConditionDiagnosisID);
                });

            migrationBuilder.CreateTable(
                name: "dayHospitals",
                columns: table => new
                {
                    DayHospitalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PracticeManager = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhurshaseManagerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dayHospitals", x => x.DayHospitalID);
                });

            migrationBuilder.CreateTable(
                name: "discharge",
                columns: table => new
                {
                    DischargePatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientGender = table.Column<int>(type: "int", nullable: false),
                    staffId = table.Column<int>(type: "int", nullable: false),
                    AdmissionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discharge", x => x.DischargePatientId);
                });

            migrationBuilder.CreateTable(
                name: "dosageForms",
                columns: table => new
                {
                    DosageFormID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DosageFormName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dosageForms", x => x.DosageFormID);
                });

            migrationBuilder.CreateTable(
                name: "medAdmin",
                columns: table => new
                {
                    MedicationAdministrationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescriberName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescriberPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationName = table.Column<int>(type: "int", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medAdmin", x => x.MedicationAdministrationID);
                });

            migrationBuilder.CreateTable(
                name: "medicalProfessionals",
                columns: table => new
                {
                    MedicalProfessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicalProfessionals", x => x.MedicalProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "notesoforders",
                columns: table => new
                {
                    NotesOfOrdersID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderMedicationID = table.Column<int>(type: "int", nullable: false),
                    PharmacyMedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notesoforders", x => x.NotesOfOrdersID);
                });

            migrationBuilder.CreateTable(
                name: "operationTheatres",
                columns: table => new
                {
                    OperationTheatreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationTheatreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operationTheatres", x => x.OperationTheatreID);
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientIDNO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    surbub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    zip = table.Column<int>(type: "int", nullable: false),
                    contactNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    wardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergiesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prescriber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgnoreReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.PrescriptionID);
                });

            migrationBuilder.CreateTable(
                name: "prescriptionViewModels",
                columns: table => new
                {
                    PrescriptionViewModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Prescriber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IgnoreReason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasAlerts = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptionViewModels", x => x.PrescriptionViewModelID);
                });

            migrationBuilder.CreateTable(
                name: "rejections",
                columns: table => new
                {
                    RejectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rejections", x => x.RejectID);
                });

            migrationBuilder.CreateTable(
                name: "schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_schedules", x => x.ScheduleId);
                });

            migrationBuilder.CreateTable(
                name: "treatmentCodes",
                columns: table => new
                {
                    TreatmentCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ICDCODE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treatmentCodes", x => x.TreatmentCodeID);
                });

            migrationBuilder.CreateTable(
                name: "viewrecords",
                columns: table => new
                {
                    ViewPatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_viewrecords", x => x.ViewPatientID);
                });

            migrationBuilder.CreateTable(
                name: "vitalranges",
                columns: table => new
                {
                    VitalRangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Vital = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minimumrange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maximumrange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vitalranges", x => x.VitalRangeID);
                });

            migrationBuilder.CreateTable(
                name: "vitals",
                columns: table => new
                {
                    VitalID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitalType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minimumrange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Maximumrange = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vitals", x => x.VitalID);
                });

            migrationBuilder.CreateTable(
                name: "anallergies",
                columns: table => new
                {
                    AnAllergiesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anallergies", x => x.AnAllergiesID);
                    table.ForeignKey(
                        name: "FK_anallergies_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "conditions",
                columns: table => new
                {
                    ConditionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConditionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditions", x => x.ConditionID);
                    table.ForeignKey(
                        name: "FK_conditions_admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "admins",
                        principalColumn: "AdminID");
                });

            migrationBuilder.CreateTable(
                name: "contraindication",
                columns: table => new
                {
                    ContraIndicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveID = table.Column<int>(type: "int", nullable: false),
                    AnConditionsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contraindication", x => x.ContraIndicationID);
                    table.ForeignKey(
                        name: "FK_contraindication_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contraindication_anconditions_AnConditionsID",
                        column: x => x.AnConditionsID,
                        principalTable: "anconditions",
                        principalColumn: "AnConditionsID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Administrators_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Anaesthesiologists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnaesthesiologistLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anaesthesiologists", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Anaesthesiologists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nurses",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NurseLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nurses", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Nurses_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pharmacists",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pharmacists", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Pharmacists_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Surgeons",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurgeonLicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Surgeons", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Surgeons_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ward",
                columns: table => new
                {
                    WardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ward", x => x.WardID);
                    table.ForeignKey(
                        name: "FK_ward_Bed_BedID",
                        column: x => x.BedID,
                        principalTable: "Bed",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contra_Indications",
                columns: table => new
                {
                    ContraIndication = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientID = table.Column<int>(type: "int", nullable: false),
                    ConditionDiagnosisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contra_Indications", x => x.ContraIndication);
                    table.ForeignKey(
                        name: "FK_contra_Indications_activeIngredients_ActiveIngredientID",
                        column: x => x.ActiveIngredientID,
                        principalTable: "activeIngredients",
                        principalColumn: "ActiveIngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contra_Indications_conditionDiagnoses_ConditionDiagnosisID",
                        column: x => x.ConditionDiagnosisID,
                        principalTable: "conditionDiagnoses",
                        principalColumn: "ConditionDiagnosisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medications",
                columns: table => new
                {
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosageFormID = table.Column<int>(type: "int", nullable: false),
                    DosageFormName = table.Column<int>(type: "int", nullable: true),
                    ActiveIngridientID = table.Column<int>(type: "int", nullable: false),
                    ActiveIngredientStrength = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityOnHand = table.Column<int>(type: "int", nullable: false),
                    RestockLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medications", x => x.MedicationID);
                    table.ForeignKey(
                        name: "FK_medications_activeIngredients_ActiveIngridientID",
                        column: x => x.ActiveIngridientID,
                        principalTable: "activeIngredients",
                        principalColumn: "ActiveIngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_medications_dosageForms_DosageFormName",
                        column: x => x.DosageFormName,
                        principalTable: "dosageForms",
                        principalColumn: "DosageFormID");
                });

            migrationBuilder.CreateTable(
                name: "admission",
                columns: table => new
                {
                    AdmissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    PatientGender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPhone = table.Column<int>(type: "int", nullable: false),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surbub = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedNumber = table.Column<int>(type: "int", nullable: false),
                    ConditionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WardName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllergiesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TreatmentCode = table.Column<int>(type: "int", nullable: false),
                    zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admission", x => x.AdmissionID);
                    table.ForeignKey(
                        name: "FK_admission_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    AnOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddmID = table.Column<int>(type: "int", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.AnOrderID);
                    table.ForeignKey(
                        name: "FK_order_patients_AddmID",
                        column: x => x.AddmID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAllergies",
                columns: table => new
                {
                    PatientAllergiesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ActiveID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAllergies", x => x.PatientAllergiesId);
                    table.ForeignKey(
                        name: "FK_PatientAllergies_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID");
                    table.ForeignKey(
                        name: "FK_PatientAllergies_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientChronicConditions",
                columns: table => new
                {
                    PatientChronicConditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    AnConditionsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientChronicConditions", x => x.PatientChronicConditionId);
                    table.ForeignKey(
                        name: "FK_PatientChronicConditions_anconditions_AnConditionsID",
                        column: x => x.AnConditionsID,
                        principalTable: "anconditions",
                        principalColumn: "AnConditionsID");
                    table.ForeignKey(
                        name: "FK_PatientChronicConditions_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientVisits",
                columns: table => new
                {
                    PatientVisitID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdmissionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DischargeTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientVisits", x => x.PatientVisitID);
                    table.ForeignKey(
                        name: "FK_patientVisits_Bed_BedID",
                        column: x => x.BedID,
                        principalTable: "Bed",
                        principalColumn: "BedID");
                    table.ForeignKey(
                        name: "FK_patientVisits_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientVitals",
                columns: table => new
                {
                    PatientVitalsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Reading = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientVitals", x => x.PatientVitalsID);
                    table.ForeignKey(
                        name: "FK_patientVitals_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "PrescriptionMedicationViewModel",
                columns: table => new
                {
                    PrescriptionMedicationViewModelID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrescriptionViewModelID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrescriptionMedicationViewModel", x => x.PrescriptionMedicationViewModelID);
                    table.ForeignKey(
                        name: "FK_PrescriptionMedicationViewModel_prescriptionViewModels_PrescriptionViewModelID",
                        column: x => x.PrescriptionViewModelID,
                        principalTable: "prescriptionViewModels",
                        principalColumn: "PrescriptionViewModelID");
                });

            migrationBuilder.CreateTable(
                name: "chronicmedication",
                columns: table => new
                {
                    ChronicMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChronicName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    DosageFormID = table.Column<int>(type: "int", nullable: false),
                    ActiveID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chronicmedication", x => x.ChronicMedicationID);
                    table.ForeignKey(
                        name: "FK_chronicmedication_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chronicmedication_dosageForms_DosageFormID",
                        column: x => x.DosageFormID,
                        principalTable: "dosageForms",
                        principalColumn: "DosageFormID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_chronicmedication_schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pharmacyMedications",
                columns: table => new
                {
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    stocklevel = table.Column<int>(type: "int", nullable: false),
                    stockhand = table.Column<int>(type: "int", nullable: false),
                    DosageFormID = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmacyMedications", x => x.PharmacyMedicationID);
                    table.ForeignKey(
                        name: "FK_pharmacyMedications_dosageForms_DosageFormID",
                        column: x => x.DosageFormID,
                        principalTable: "dosageForms",
                        principalColumn: "DosageFormID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pharmacyMedications_schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "schedules",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patientConditions",
                columns: table => new
                {
                    PatientConditionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: true),
                    ConditionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patientConditions", x => x.PatientConditionID);
                    table.ForeignKey(
                        name: "FK_patientConditions_conditions_ConditionID",
                        column: x => x.ConditionID,
                        principalTable: "conditions",
                        principalColumn: "ConditionID");
                    table.ForeignKey(
                        name: "FK_patientConditions_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "bookingNewPatients",
                columns: table => new
                {
                    BookingNewPatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNewPatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingNewPatientSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookingNewPatientIDNUmber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OperationTheatreID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingNewPatients", x => x.BookingNewPatientID);
                    table.ForeignKey(
                        name: "FK_bookingNewPatients_Anaesthesiologists_UserId",
                        column: x => x.UserId,
                        principalTable: "Anaesthesiologists",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_bookingNewPatients_operationTheatres_OperationTheatreID",
                        column: x => x.OperationTheatreID,
                        principalTable: "operationTheatres",
                        principalColumn: "OperationTheatreID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    OperationTheatreID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingID);
                    table.ForeignKey(
                        name: "FK_bookings_Anaesthesiologists_UserId",
                        column: x => x.UserId,
                        principalTable: "Anaesthesiologists",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_bookings_operationTheatres_OperationTheatreID",
                        column: x => x.OperationTheatreID,
                        principalTable: "operationTheatres",
                        principalColumn: "OperationTheatreID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CurrentMedication",
                columns: table => new
                {
                    CurrentMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentMedication", x => x.CurrentMedicationID);
                    table.ForeignKey(
                        name: "FK_CurrentMedication_medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "medications",
                        principalColumn: "MedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ancurrentmedication",
                columns: table => new
                {
                    AnCurrentMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChronicMedicationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ancurrentmedication", x => x.AnCurrentMedicationID);
                    table.ForeignKey(
                        name: "FK_ancurrentmedication_chronicmedication_ChronicMedicationID",
                        column: x => x.ChronicMedicationID,
                        principalTable: "chronicmedication",
                        principalColumn: "ChronicMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicationActive",
                columns: table => new
                {
                    MedicationActiveID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChronicMedicationID = table.Column<int>(type: "int", nullable: true),
                    ActiveID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicationActive", x => x.MedicationActiveID);
                    table.ForeignKey(
                        name: "FK_MedicationActive_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID");
                    table.ForeignKey(
                        name: "FK_MedicationActive_chronicmedication_ChronicMedicationID",
                        column: x => x.ChronicMedicationID,
                        principalTable: "chronicmedication",
                        principalColumn: "ChronicMedicationID");
                });

            migrationBuilder.CreateTable(
                name: "PatientMedications",
                columns: table => new
                {
                    PatientMedicationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    ChronicMedicationID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientMedications", x => x.PatientMedicationId);
                    table.ForeignKey(
                        name: "FK_PatientMedications_chronicmedication_ChronicMedicationID",
                        column: x => x.ChronicMedicationID,
                        principalTable: "chronicmedication",
                        principalColumn: "ChronicMedicationID");
                    table.ForeignKey(
                        name: "FK_PatientMedications_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "medicationPrescriptions",
                columns: table => new
                {
                    MedicationPrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instruction = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_medicationPrescriptions", x => x.MedicationPrescriptionID);
                    table.ForeignKey(
                        name: "FK_medicationPrescriptions_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ordercreate",
                columns: table => new
                {
                    OrderCreateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnOrderID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddmID = table.Column<int>(type: "int", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordercreate", x => x.OrderCreateID);
                    table.ForeignKey(
                        name: "FK_ordercreate_order_AnOrderID",
                        column: x => x.AnOrderID,
                        principalTable: "order",
                        principalColumn: "AnOrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ordercreate_patients_AddmID",
                        column: x => x.AddmID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ordercreate_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ordermedication",
                columns: table => new
                {
                    OrderMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnOrderID = table.Column<int>(type: "int", nullable: true),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ordermedication", x => x.OrderMedicationID);
                    table.ForeignKey(
                        name: "FK_ordermedication_order_AnOrderID",
                        column: x => x.AnOrderID,
                        principalTable: "order",
                        principalColumn: "AnOrderID");
                    table.ForeignKey(
                        name: "FK_ordermedication_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PharmacyMedicationIngredients",
                columns: table => new
                {
                    PharmacyMedicationIngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    ActiveID = table.Column<int>(type: "int", nullable: false),
                    Strength = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyMedicationIngredients", x => x.PharmacyMedicationIngredientId);
                    table.ForeignKey(
                        name: "FK_PharmacyMedicationIngredients_active_ActiveID",
                        column: x => x.ActiveID,
                        principalTable: "active",
                        principalColumn: "ActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PharmacyMedicationIngredients_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pharmStock",
                columns: table => new
                {
                    PharmStockId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityOrdered = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pharmStock", x => x.PharmStockId);
                    table.ForeignKey(
                        name: "FK_pharmStock_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "prescriptionMedications",
                columns: table => new
                {
                    PrescriptionMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrescriptionID = table.Column<int>(type: "int", nullable: false),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptionMedications", x => x.PrescriptionMedicationID);
                    table.ForeignKey(
                        name: "FK_prescriptionMedications_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_prescriptionMedications_prescriptions_PrescriptionID",
                        column: x => x.PrescriptionID,
                        principalTable: "prescriptions",
                        principalColumn: "PrescriptionID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookingPatientTreatmentCode",
                columns: table => new
                {
                    BookingPatientTreatmentCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingNewPatientID = table.Column<int>(type: "int", nullable: false),
                    TreatmentCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingPatientTreatmentCode", x => x.BookingPatientTreatmentCodeID);
                    table.ForeignKey(
                        name: "FK_BookingPatientTreatmentCode_bookingNewPatients_BookingNewPatientID",
                        column: x => x.BookingNewPatientID,
                        principalTable: "bookingNewPatients",
                        principalColumn: "BookingNewPatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingPatientTreatmentCode_treatmentCodes_TreatmentCodeID",
                        column: x => x.TreatmentCodeID,
                        principalTable: "treatmentCodes",
                        principalColumn: "TreatmentCodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingTreatmentCode",
                columns: table => new
                {
                    BookingTreatmentCodeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    TreatmentCodeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTreatmentCode", x => x.BookingTreatmentCodeID);
                    table.ForeignKey(
                        name: "FK_BookingTreatmentCode_bookings_BookingID",
                        column: x => x.BookingID,
                        principalTable: "bookings",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTreatmentCode_treatmentCodes_TreatmentCodeID",
                        column: x => x.TreatmentCodeID,
                        principalTable: "treatmentCodes",
                        principalColumn: "TreatmentCodeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ranges",
                columns: table => new
                {
                    RangeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    PatientGender = table.Column<int>(type: "int", nullable: false),
                    CurrentMedicationID = table.Column<int>(type: "int", nullable: false),
                    TreatmentCodeID = table.Column<int>(type: "int", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false),
                    ConditionID = table.Column<int>(type: "int", nullable: false),
                    WardID = table.Column<int>(type: "int", nullable: false),
                    AnAllergies = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    TreatmentID = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ranges", x => x.RangeID);
                    table.ForeignKey(
                        name: "FK_ranges_anallergies_AnAllergies",
                        column: x => x.AnAllergies,
                        principalTable: "anallergies",
                        principalColumn: "AnAllergiesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_Bed_BedID",
                        column: x => x.BedID,
                        principalTable: "Bed",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_conditions_ConditionID",
                        column: x => x.ConditionID,
                        principalTable: "conditions",
                        principalColumn: "ConditionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_CurrentMedication_CurrentMedicationID",
                        column: x => x.CurrentMedicationID,
                        principalTable: "CurrentMedication",
                        principalColumn: "CurrentMedicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_treatmentCodes_TreatmentID",
                        column: x => x.TreatmentID,
                        principalTable: "treatmentCodes",
                        principalColumn: "TreatmentCodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ranges_ward_WardID",
                        column: x => x.WardID,
                        principalTable: "ward",
                        principalColumn: "WardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "addm",
                columns: table => new
                {
                    AddmID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    AnCurrentMedicationID = table.Column<int>(type: "int", nullable: false),
                    TreatmentCodeID = table.Column<int>(type: "int", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false),
                    AnConditionsID = table.Column<int>(type: "int", nullable: false),
                    WardID = table.Column<int>(type: "int", nullable: false),
                    AnAllergiesID = table.Column<int>(type: "int", nullable: false),
                    ChronicMedicationID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicationActiveID = table.Column<int>(type: "int", nullable: false),
                    TreatmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_addm", x => x.AddmID);
                    table.ForeignKey(
                        name: "FK_addm_anallergies_AnAllergiesID",
                        column: x => x.AnAllergiesID,
                        principalTable: "anallergies",
                        principalColumn: "AnAllergiesID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_anconditions_AnConditionsID",
                        column: x => x.AnConditionsID,
                        principalTable: "anconditions",
                        principalColumn: "AnConditionsID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_ancurrentmedication_AnCurrentMedicationID",
                        column: x => x.AnCurrentMedicationID,
                        principalTable: "ancurrentmedication",
                        principalColumn: "AnCurrentMedicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_Bed_BedID",
                        column: x => x.BedID,
                        principalTable: "Bed",
                        principalColumn: "BedID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_chronicmedication_ChronicMedicationID",
                        column: x => x.ChronicMedicationID,
                        principalTable: "chronicmedication",
                        principalColumn: "ChronicMedicationID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_MedicationActive_MedicationActiveID",
                        column: x => x.MedicationActiveID,
                        principalTable: "MedicationActive",
                        principalColumn: "MedicationActiveID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_treatmentCodes_TreatmentCodeID",
                        column: x => x.TreatmentCodeID,
                        principalTable: "treatmentCodes",
                        principalColumn: "TreatmentCodeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_addm_ward_WardID",
                        column: x => x.WardID,
                        principalTable: "ward",
                        principalColumn: "WardID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orderItems",
                columns: table => new
                {
                    OrderItemsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PharmacyMedicationID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OrderCreateID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orderItems", x => x.OrderItemsID);
                    table.ForeignKey(
                        name: "FK_orderItems_ordercreate_OrderCreateID",
                        column: x => x.OrderCreateID,
                        principalTable: "ordercreate",
                        principalColumn: "OrderCreateID");
                    table.ForeignKey(
                        name: "FK_orderItems_pharmacyMedications_PharmacyMedicationID",
                        column: x => x.PharmacyMedicationID,
                        principalTable: "pharmacyMedications",
                        principalColumn: "PharmacyMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notes",
                columns: table => new
                {
                    NotesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationNoteID = table.Column<int>(type: "int", nullable: false),
                    OrderMedicationID = table.Column<int>(type: "int", nullable: false),
                    NoteText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.NotesID);
                    table.ForeignKey(
                        name: "FK_notes_ordermedication_OrderMedicationID",
                        column: x => x.OrderMedicationID,
                        principalTable: "ordermedication",
                        principalColumn: "OrderMedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StockReceiveds",
                columns: table => new
                {
                    StockReceivedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuantityRecived = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PharmStockId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockReceiveds", x => x.StockReceivedId);
                    table.ForeignKey(
                        name: "FK_StockReceiveds_pharmStock_PharmStockId",
                        column: x => x.PharmStockId,
                        principalTable: "pharmStock",
                        principalColumn: "PharmStockId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_addm_AnAllergiesID",
                table: "addm",
                column: "AnAllergiesID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_AnConditionsID",
                table: "addm",
                column: "AnConditionsID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_AnCurrentMedicationID",
                table: "addm",
                column: "AnCurrentMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_BedID",
                table: "addm",
                column: "BedID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_ChronicMedicationID",
                table: "addm",
                column: "ChronicMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_MedicationActiveID",
                table: "addm",
                column: "MedicationActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_PatientID",
                table: "addm",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_TreatmentCodeID",
                table: "addm",
                column: "TreatmentCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_addm_WardID",
                table: "addm",
                column: "WardID");

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_ApplicationUserId",
                table: "Administrators",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_admission_PatientID",
                table: "admission",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Anaesthesiologists_ApplicationUserId",
                table: "Anaesthesiologists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_anallergies_ActiveID",
                table: "anallergies",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_ancurrentmedication_ChronicMedicationID",
                table: "ancurrentmedication",
                column: "ChronicMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_bookingNewPatients_OperationTheatreID",
                table: "bookingNewPatients",
                column: "OperationTheatreID");

            migrationBuilder.CreateIndex(
                name: "IX_bookingNewPatients_UserId",
                table: "bookingNewPatients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPatientTreatmentCode_BookingNewPatientID",
                table: "BookingPatientTreatmentCode",
                column: "BookingNewPatientID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingPatientTreatmentCode_TreatmentCodeID",
                table: "BookingPatientTreatmentCode",
                column: "TreatmentCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_OperationTheatreID",
                table: "bookings",
                column: "OperationTheatreID");

            migrationBuilder.CreateIndex(
                name: "IX_bookings_UserId",
                table: "bookings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTreatmentCode_BookingID",
                table: "BookingTreatmentCode",
                column: "BookingID");

            migrationBuilder.CreateIndex(
                name: "IX_BookingTreatmentCode_TreatmentCodeID",
                table: "BookingTreatmentCode",
                column: "TreatmentCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_chronicmedication_ActiveID",
                table: "chronicmedication",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_chronicmedication_DosageFormID",
                table: "chronicmedication",
                column: "DosageFormID");

            migrationBuilder.CreateIndex(
                name: "IX_chronicmedication_ScheduleId",
                table: "chronicmedication",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_conditions_AdminID",
                table: "conditions",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_contra_Indications_ActiveIngredientID",
                table: "contra_Indications",
                column: "ActiveIngredientID");

            migrationBuilder.CreateIndex(
                name: "IX_contra_Indications_ConditionDiagnosisID",
                table: "contra_Indications",
                column: "ConditionDiagnosisID");

            migrationBuilder.CreateIndex(
                name: "IX_contraindication_ActiveID",
                table: "contraindication",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_contraindication_AnConditionsID",
                table: "contraindication",
                column: "AnConditionsID");

            migrationBuilder.CreateIndex(
                name: "IX_CurrentMedication_MedicationID",
                table: "CurrentMedication",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationActive_ActiveID",
                table: "MedicationActive",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_MedicationActive_ChronicMedicationID",
                table: "MedicationActive",
                column: "ChronicMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_medicationPrescriptions_PharmacyMedicationID",
                table: "medicationPrescriptions",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_medications_ActiveIngridientID",
                table: "medications",
                column: "ActiveIngridientID");

            migrationBuilder.CreateIndex(
                name: "IX_medications_DosageFormName",
                table: "medications",
                column: "DosageFormName");

            migrationBuilder.CreateIndex(
                name: "IX_notes_OrderMedicationID",
                table: "notes",
                column: "OrderMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_Nurses_ApplicationUserId",
                table: "Nurses",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_order_AddmID",
                table: "order",
                column: "AddmID");

            migrationBuilder.CreateIndex(
                name: "IX_ordercreate_AddmID",
                table: "ordercreate",
                column: "AddmID");

            migrationBuilder.CreateIndex(
                name: "IX_ordercreate_AnOrderID",
                table: "ordercreate",
                column: "AnOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ordercreate_PharmacyMedicationID",
                table: "ordercreate",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_OrderCreateID",
                table: "orderItems",
                column: "OrderCreateID");

            migrationBuilder.CreateIndex(
                name: "IX_orderItems_PharmacyMedicationID",
                table: "orderItems",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_ordermedication_AnOrderID",
                table: "ordermedication",
                column: "AnOrderID");

            migrationBuilder.CreateIndex(
                name: "IX_ordermedication_PharmacyMedicationID",
                table: "ordermedication",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_ActiveID",
                table: "PatientAllergies",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAllergies_PatientID",
                table: "PatientAllergies",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientChronicConditions_AnConditionsID",
                table: "PatientChronicConditions",
                column: "AnConditionsID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientChronicConditions_PatientID",
                table: "PatientChronicConditions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_patientConditions_ConditionID",
                table: "patientConditions",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_patientConditions_PatientID",
                table: "patientConditions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_ChronicMedicationID",
                table: "PatientMedications",
                column: "ChronicMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_PatientMedications_PatientID",
                table: "PatientMedications",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_patientVisits_BedID",
                table: "patientVisits",
                column: "BedID");

            migrationBuilder.CreateIndex(
                name: "IX_patientVisits_PatientID",
                table: "patientVisits",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_patientVitals_PatientID",
                table: "patientVitals",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_Pharmacists_ApplicationUserId",
                table: "Pharmacists",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyMedicationIngredients_ActiveID",
                table: "PharmacyMedicationIngredients",
                column: "ActiveID");

            migrationBuilder.CreateIndex(
                name: "IX_PharmacyMedicationIngredients_PharmacyMedicationID",
                table: "PharmacyMedicationIngredients",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacyMedications_DosageFormID",
                table: "pharmacyMedications",
                column: "DosageFormID");

            migrationBuilder.CreateIndex(
                name: "IX_pharmacyMedications_ScheduleId",
                table: "pharmacyMedications",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_pharmStock_PharmacyMedicationID",
                table: "pharmStock",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptionMedications_PharmacyMedicationID",
                table: "prescriptionMedications",
                column: "PharmacyMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptionMedications_PrescriptionID",
                table: "prescriptionMedications",
                column: "PrescriptionID");

            migrationBuilder.CreateIndex(
                name: "IX_PrescriptionMedicationViewModel_PrescriptionViewModelID",
                table: "PrescriptionMedicationViewModel",
                column: "PrescriptionViewModelID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_AnAllergies",
                table: "ranges",
                column: "AnAllergies");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_BedID",
                table: "ranges",
                column: "BedID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_ConditionID",
                table: "ranges",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_CurrentMedicationID",
                table: "ranges",
                column: "CurrentMedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_PatientID",
                table: "ranges",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_TreatmentID",
                table: "ranges",
                column: "TreatmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ranges_WardID",
                table: "ranges",
                column: "WardID");

            migrationBuilder.CreateIndex(
                name: "IX_StockReceiveds_PharmStockId",
                table: "StockReceiveds",
                column: "PharmStockId");

            migrationBuilder.CreateIndex(
                name: "IX_Surgeons_ApplicationUserId",
                table: "Surgeons",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ward_BedID",
                table: "ward",
                column: "BedID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "addm");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "admission");

            migrationBuilder.DropTable(
                name: "AlertMedications");

            migrationBuilder.DropTable(
                name: "anvitals");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BookingPatientTreatmentCode");

            migrationBuilder.DropTable(
                name: "BookingTreatmentCode");

            migrationBuilder.DropTable(
                name: "contra_Indications");

            migrationBuilder.DropTable(
                name: "contraindication");

            migrationBuilder.DropTable(
                name: "dayHospitals");

            migrationBuilder.DropTable(
                name: "discharge");

            migrationBuilder.DropTable(
                name: "medAdmin");

            migrationBuilder.DropTable(
                name: "medicalProfessionals");

            migrationBuilder.DropTable(
                name: "medicationPrescriptions");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "notesoforders");

            migrationBuilder.DropTable(
                name: "Nurses");

            migrationBuilder.DropTable(
                name: "orderItems");

            migrationBuilder.DropTable(
                name: "PatientAllergies");

            migrationBuilder.DropTable(
                name: "PatientChronicConditions");

            migrationBuilder.DropTable(
                name: "patientConditions");

            migrationBuilder.DropTable(
                name: "PatientMedications");

            migrationBuilder.DropTable(
                name: "patientVisits");

            migrationBuilder.DropTable(
                name: "patientVitals");

            migrationBuilder.DropTable(
                name: "Pharmacists");

            migrationBuilder.DropTable(
                name: "PharmacyMedicationIngredients");

            migrationBuilder.DropTable(
                name: "prescriptionMedications");

            migrationBuilder.DropTable(
                name: "PrescriptionMedicationViewModel");

            migrationBuilder.DropTable(
                name: "ranges");

            migrationBuilder.DropTable(
                name: "rejections");

            migrationBuilder.DropTable(
                name: "StockReceiveds");

            migrationBuilder.DropTable(
                name: "Surgeons");

            migrationBuilder.DropTable(
                name: "viewrecords");

            migrationBuilder.DropTable(
                name: "vitalranges");

            migrationBuilder.DropTable(
                name: "vitals");

            migrationBuilder.DropTable(
                name: "ancurrentmedication");

            migrationBuilder.DropTable(
                name: "MedicationActive");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "bookingNewPatients");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "conditionDiagnoses");

            migrationBuilder.DropTable(
                name: "ordermedication");

            migrationBuilder.DropTable(
                name: "ordercreate");

            migrationBuilder.DropTable(
                name: "anconditions");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "prescriptionViewModels");

            migrationBuilder.DropTable(
                name: "anallergies");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "CurrentMedication");

            migrationBuilder.DropTable(
                name: "treatmentCodes");

            migrationBuilder.DropTable(
                name: "ward");

            migrationBuilder.DropTable(
                name: "pharmStock");

            migrationBuilder.DropTable(
                name: "chronicmedication");

            migrationBuilder.DropTable(
                name: "Anaesthesiologists");

            migrationBuilder.DropTable(
                name: "operationTheatres");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "medications");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "pharmacyMedications");

            migrationBuilder.DropTable(
                name: "active");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "activeIngredients");

            migrationBuilder.DropTable(
                name: "dosageForms");

            migrationBuilder.DropTable(
                name: "schedules");
        }
    }
}
