using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.FullName)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasMany(d => d.DoctorProcedures)
               .WithOne(dp => dp.Doctor)
               .HasForeignKey(dp => dp.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
