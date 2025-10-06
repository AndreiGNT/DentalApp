using System.Reflection;
using System.Reflection.Emit;
using DentalApp.Application.Common.Interfaces;
using DentalApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DentalApp.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
    {

    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Procedure> Procedures { get; set; }
    public DbSet<DoctorProcedure> DoctorProcedures { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<Procedure>().HasData(
            new Procedure { Id = 1, ProcedureName = "Descaling and sanitization", Duration = TimeSpan.FromMinutes(30), Price = 500 },
            new Procedure { Id = 2, ProcedureName = "Treatment of dental caries", Duration = TimeSpan.FromMinutes(45), Price = 450 },
            new Procedure { Id = 3, ProcedureName = "Dental extractions", Duration = TimeSpan.FromMinutes(60), Price = 600 },
            new Procedure { Id = 4, ProcedureName = "Teeth whitening", Duration = TimeSpan.FromMinutes(45), Price = 2000 },
            new Procedure { Id = 5, ProcedureName = "Dental implants", Duration = TimeSpan.FromMinutes(90), Price = 18000 }
        );

        builder.Entity<Doctor>().HasData(
            new Doctor
            {
                Id = 1,
                FullName = "Dr. Dumitru Girnet",
                Created = DateTime.UtcNow,
                CreatedBy = "system"
            },
            new Doctor
            {
                Id = 2,
                FullName = "Dr. Dumitru Zaporojan",
                Created = DateTime.UtcNow,
                CreatedBy = "system"
            },
            new Doctor
            {
                Id = 3,
                FullName = "Dr. Ghenadie Stoian",
                Created = DateTime.UtcNow,
                CreatedBy = "system"
            }
        );

        builder.Entity<DoctorProcedure>().HasData(
            new DoctorProcedure { Id = 1, DoctorId = 1, ProcedureId = 1 },
            new DoctorProcedure { Id = 2, DoctorId = 1, ProcedureId = 2 },
            new DoctorProcedure { Id = 3, DoctorId = 2, ProcedureId = 3 },
            new DoctorProcedure { Id = 4, DoctorId = 2, ProcedureId = 4 },
            new DoctorProcedure { Id = 5, DoctorId = 3, ProcedureId = 5 }
        );

        //builder.Entity<Appointment>().HasData(
        //    new Appointment
        //    {
        //        Id = 1,
        //        UserId = "test-user-1",       
        //        DoctorId = 1,                 
        //        ProcedureId = 1,              
        //        StartTime = new DateTime(2025, 10, 7, 9, 0, 0),
        //        EndTime = new DateTime(2025, 10, 7, 9, 30, 0),
        //        Status = "Confirmed",
        //        Created = DateTime.UtcNow,
        //        CreatedBy = "system"
        //    },
        //    new Appointment
        //    {
        //        Id = 2,
        //        UserId = "test-user-2",
        //        DoctorId = 2,
        //        ProcedureId = 3,
        //        StartTime = new DateTime(2025, 10, 7, 10, 0, 0),
        //        EndTime = new DateTime(2025, 10, 7, 10, 45, 0),
        //        Status = "Pending",
        //        Created = DateTime.UtcNow,
        //        CreatedBy = "system"
        //    }
        //);
    }
}
