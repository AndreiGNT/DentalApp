using DentalApp.Domain.Entities;
using DentalApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.UserId)
               .IsRequired();

        builder.HasOne<ApplicationUser>()
               .WithMany()
               .HasForeignKey(a => a.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Doctor)
               .WithMany()
               .HasForeignKey(a => a.DoctorId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Procedure)
               .WithMany()
               .HasForeignKey(a => a.ProcedureId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.StartTime)
               .IsRequired();

        builder.Property(a => a.EndTime)
               .IsRequired();

        builder.Property(a => a.Status)
               .IsRequired()
               .HasMaxLength(20);

        builder.HasIndex(a => new { a.DoctorId, a.StartTime });
    }
}
