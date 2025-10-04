using DentalApp.Domain.Entities;

namespace DentalApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> AllUsers { get; }
    DbSet<Procedure> Procedures { get; }
    DbSet<Doctor> Doctors { get; }
    DbSet<DoctorProcedure> DoctorProcedures { get; }
    DbSet<Appointment> Appointments { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
