using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WIRKDEVELOPER.Migrations
{
    public partial class Adddb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activeIngredients",
                columns: table => new
                {
                    ActiveIngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                name: "admission",
                columns: table => new
                {
                    AdmissionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Suburb = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedID = table.Column<int>(type: "int", nullable: false),
                    ConditionID = table.Column<int>(type: "int", nullable: false),
                    WardID = table.Column<int>(type: "int", nullable: false),
                    AllergiesID = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    TreatmentID = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_admission", x => x.AdmissionID);
                });

            migrationBuilder.CreateTable(
                name: "anallergies",
                columns: table => new
                {
                    AllergiesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActiveIngridient = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reaction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anallergies", x => x.AllergiesID);
                });

            migrationBuilder.CreateTable(
                name: "anconditions",
                columns: table => new
                {
                    AnConditionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Condition = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_anconditions", x => x.AnConditionID);
                });

            migrationBuilder.CreateTable(
                name: "ancurrentmedication",
                columns: table => new
                {
                    CurrentMedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dosage = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ancurrentmedication", x => x.CurrentMedicationID);
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
                    FirstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
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
                name: "bookingPatients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientSurname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingPatients", x => x.PatientID);
                });

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookings", x => x.BookingID);
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
                name: "discharge",
                columns: table => new
                {
                    DischargePatientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    staffId = table.Column<int>(type: "int", nullable: false),
                    AdmissionId = table.Column<int>(type: "int", nullable: false),
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
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
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
                name: "notes",
                columns: table => new
                {
                    NotesID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicationordered = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notes", x => x.NotesID);
                });

            migrationBuilder.CreateTable(
                name: "operationTheatres",
                columns: table => new
                {
                    TheatreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheatreName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_operationTheatres", x => x.TheatreID);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    AnOrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Patient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Medicationordered = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Instructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.AnOrderID);
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
                    Units = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    VitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LowLimit = table.Column<int>(type: "int", nullable: false),
                    HighLimit = table.Column<int>(type: "int", nullable: false),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Temperature = table.Column<int>(type: "int", nullable: false),
                    RespiratoryRate = table.Column<int>(type: "int", nullable: false),
                    BloodPressure = table.Column<int>(type: "int", nullable: false),
                    BpPosition = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vitals", x => x.VitalID);
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
                name: "patients",
                columns: table => new
                {
                    PatientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientPatient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactNumber = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.PatientID);
                    table.ForeignKey(
                        name: "FK_patients_admins_AdminID",
                        column: x => x.AdminID,
                        principalTable: "admins",
                        principalColumn: "AdminID");
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
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
                name: "medications",
                columns: table => new
                {
                    MedicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DosageFormID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_medications_dosageForms_DosageFormID",
                        column: x => x.DosageFormID,
                        principalTable: "dosageForms",
                        principalColumn: "DosageFormID",
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
                name: "prescriptions",
                columns: table => new
                {
                    PrescriptionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientID = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    prescriber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Medication = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityAndInstructions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Urgent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prescriptions", x => x.PrescriptionID);
                    table.ForeignKey(
                        name: "FK_prescriptions_AspNetUsers_ID",
                        column: x => x.ID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_prescriptions_patients_PatientID",
                        column: x => x.PatientID,
                        principalTable: "patients",
                        principalColumn: "PatientID");
                });

            migrationBuilder.CreateTable(
                name: "contra_Indications",
                columns: table => new
                {
                    ContraIndication = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    ConditionDiagnosisID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contra_Indications", x => x.ContraIndication);
                    table.ForeignKey(
                        name: "FK_contra_Indications_conditionDiagnoses_ConditionDiagnosisID",
                        column: x => x.ConditionDiagnosisID,
                        principalTable: "conditionDiagnoses",
                        principalColumn: "ConditionDiagnosisID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_contra_Indications_medications_MedicationID",
                        column: x => x.MedicationID,
                        principalTable: "medications",
                        principalColumn: "MedicationID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stocks",
                columns: table => new
                {
                    stockID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicationID = table.Column<int>(type: "int", nullable: false),
                    MedicationName = table.Column<int>(type: "int", nullable: true),
                    Restocklevel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    restovkLevel = table.Column<int>(type: "int", nullable: true),
                    stockOnhand = table.Column<int>(type: "int", nullable: false),
                    stockreceive = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stocks", x => x.stockID);
                    table.ForeignKey(
                        name: "FK_stocks_medications_MedicationName",
                        column: x => x.MedicationName,
                        principalTable: "medications",
                        principalColumn: "MedicationID");
                    table.ForeignKey(
                        name: "FK_stocks_medications_restovkLevel",
                        column: x => x.restovkLevel,
                        principalTable: "medications",
                        principalColumn: "MedicationID");
                });

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
                name: "IX_conditions_AdminID",
                table: "conditions",
                column: "AdminID");

            migrationBuilder.CreateIndex(
                name: "IX_contra_Indications_ConditionDiagnosisID",
                table: "contra_Indications",
                column: "ConditionDiagnosisID");

            migrationBuilder.CreateIndex(
                name: "IX_contra_Indications_MedicationID",
                table: "contra_Indications",
                column: "MedicationID");

            migrationBuilder.CreateIndex(
                name: "IX_medications_ActiveIngridientID",
                table: "medications",
                column: "ActiveIngridientID");

            migrationBuilder.CreateIndex(
                name: "IX_medications_DosageFormID",
                table: "medications",
                column: "DosageFormID");

            migrationBuilder.CreateIndex(
                name: "IX_patientConditions_ConditionID",
                table: "patientConditions",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_patientConditions_PatientID",
                table: "patientConditions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_patients_AdminID",
                table: "patients",
                column: "AdminID");

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
                name: "IX_prescriptions_ID",
                table: "prescriptions",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_prescriptions_PatientID",
                table: "prescriptions",
                column: "PatientID");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_MedicationName",
                table: "stocks",
                column: "MedicationName");

            migrationBuilder.CreateIndex(
                name: "IX_stocks_restovkLevel",
                table: "stocks",
                column: "restovkLevel");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admission");

            migrationBuilder.DropTable(
                name: "anallergies");

            migrationBuilder.DropTable(
                name: "anconditions");

            migrationBuilder.DropTable(
                name: "ancurrentmedication");

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
                name: "bookingPatients");

            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "contra_Indications");

            migrationBuilder.DropTable(
                name: "discharge");

            migrationBuilder.DropTable(
                name: "medAdmin");

            migrationBuilder.DropTable(
                name: "medicalProfessionals");

            migrationBuilder.DropTable(
                name: "notes");

            migrationBuilder.DropTable(
                name: "operationTheatres");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "patientConditions");

            migrationBuilder.DropTable(
                name: "patientVisits");

            migrationBuilder.DropTable(
                name: "patientVitals");

            migrationBuilder.DropTable(
                name: "prescriptions");

            migrationBuilder.DropTable(
                name: "stocks");

            migrationBuilder.DropTable(
                name: "viewrecords");

            migrationBuilder.DropTable(
                name: "vitalranges");

            migrationBuilder.DropTable(
                name: "vitals");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "conditionDiagnoses");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "Bed");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "medications");

            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "activeIngredients");

            migrationBuilder.DropTable(
                name: "dosageForms");
        }
    }
}
