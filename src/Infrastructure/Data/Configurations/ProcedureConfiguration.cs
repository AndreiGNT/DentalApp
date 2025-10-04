using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(u => u.ProcedureName)
               .HasMaxLength(100)
               .IsRequired();

        builder.Property(u => u.Duration)
               .IsRequired();

        builder.HasMany(p => p.DoctorProcedures)
               .WithOne(dp => dp.Procedure)
               .HasForeignKey(dp => dp.ProcedureId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
