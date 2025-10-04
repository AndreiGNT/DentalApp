using System.Reflection;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;
using DentalApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {

    }

    public DbSet<User> AllUsers {  get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<DoctorProcedure> DoctorProcedures { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // -----------------------------
        // Seed Users
        // -----------------------------
        builder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Andrei",
                LastName = "Girnet",
                PhoneNumber = "+37378121254",
                Email = "andrei.girnet@gmail.com.com",
                PasswordHash = "hashedpassword", 
                Role = "Admin",
                IsConfirmed = true,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "System"
            },

            new User
            {
                Id = 2,
                FirstName = "Ghenadie",
                LastName = "Stoian",
                PhoneNumber = "+37378121254",
                Email = "ghenadie.stoian@gmaiil.com",
                PasswordHash = "hashedpassword",
                Role = "Client",
                IsConfirmed = true,
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "System"
            }
        );

        // -----------------------------
        // Seed Procedures
        // -----------------------------
        builder.Entity<Procedure>().HasData(
            new Procedure 
            { 
                Id = 1, 
                ProcedureName = "Descaling and sanitization", 
                Duration = TimeSpan.FromMinutes(30), 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Procedure 
            { 
                Id = 2, 
                ProcedureName = "Treatment of dental caries", 
                Duration = TimeSpan.FromMinutes(45), 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Procedure 
            { 
                Id = 3, 
                ProcedureName = "Dental extractions", 
                Duration = TimeSpan.FromMinutes(45), 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Procedure 
            { 
                Id = 4, 
                ProcedureName = "Teeth whitening", 
                Duration = TimeSpan.FromMinutes(90), 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Procedure 
            { 
                Id = 5, 
                ProcedureName = "Dental implants", 
                Duration = TimeSpan.FromMinutes(120), 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            }
        );

        // -----------------------------
        // Seed Doctors
        // -----------------------------
        builder.Entity<Doctor>().HasData(
            new Doctor 
            { 
                Id = 1, 
                FullName = "Dumitru Girnet", 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Doctor 
            { 
                Id = 2, 
                FullName = "Dumitru Zaporojan",
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Doctor 
            { 
                Id = 3, 
                FullName = "Lilian Bularu",
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Doctor 
            { 
                Id = 4, 
                FullName = "Daniel Rotaru", 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            },

            new Doctor 
            { 
                Id = 5, 
                FullName = "Larisa Nastase", 
                Created = DateTimeOffset.UtcNow, 
                CreatedBy = "System" 
            }
        );

        // -----------------------------------
        // Seed DoctorProcedures - intermediar
        // -----------------------------------
        builder.Entity<DoctorProcedure>().HasData(
            // Doctor Dumitru Girnet and his procedures
            new DoctorProcedure { Id = 1, DoctorId = 1, ProcedureId = 1, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 2, DoctorId = 1, ProcedureId = 2, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 3, DoctorId = 1, ProcedureId = 4, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },

            // Doctor Dumitru Zaporojan and his procedures
            new DoctorProcedure { Id = 4, DoctorId = 2, ProcedureId = 2, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 5, DoctorId = 2, ProcedureId = 3, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },

            // Doctor Lilian Bularu and his procedures
            new DoctorProcedure { Id = 6, DoctorId = 3, ProcedureId = 2, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 7, DoctorId = 3, ProcedureId = 3, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 8, DoctorId = 3, ProcedureId = 4, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },

            // Doctor Daniel Rotaru and his procedures
            new DoctorProcedure { Id = 9, DoctorId = 4, ProcedureId = 2, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },
            new DoctorProcedure { Id = 10, DoctorId = 4, ProcedureId = 5, Created = DateTimeOffset.UtcNow, CreatedBy = "System" },

            // Doctor Larisa Nastase and his procedures
            new DoctorProcedure { Id = 11, DoctorId = 5, ProcedureId = 5, Created = DateTimeOffset.UtcNow, CreatedBy = "System" }
        );

        // -----------------------------
        // Seed Appointment
        // -----------------------------
        builder.Entity<Appointment>().HasData(
            new Appointment
            {
                Id = 1,
                UserId = 2,
                DoctorId = 5,
                ProcedureId = 5,
                StartTime = new DateTime(2025, 10, 5, 9, 0, 0),
                EndTime = new DateTime(2025, 10, 5, 11, 0, 0),
                Status = "Pending",
                Created = DateTimeOffset.UtcNow,
                CreatedBy = "ghenadie.stoian@gmaiil.com"
            }
        );
    }
}
