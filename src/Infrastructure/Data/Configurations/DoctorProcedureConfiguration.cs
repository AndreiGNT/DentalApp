using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class DoctorProcedureConfiguration : IEntityTypeConfiguration<DoctorProcedure>
{
    public void Configure(EntityTypeBuilder<DoctorProcedure> builder)
    {
        builder.HasKey(dp => dp.Id);

        builder.HasOne(dp => dp.Doctor)
               .WithMany(d => d.DoctorProcedures)
               .HasForeignKey(dp => dp.DoctorId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(dp => dp.Procedure)
               .WithMany()
               .HasForeignKey(dp => dp.ProcedureId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
