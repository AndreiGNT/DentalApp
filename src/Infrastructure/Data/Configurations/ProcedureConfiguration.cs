using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.ProcedureName)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(p => p.Duration)
               .IsRequired();

        builder.Property(p => p.Price)
               .IsRequired();
    }
}
