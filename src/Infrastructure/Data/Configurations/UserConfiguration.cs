﻿using DentalApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DentalApp.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.LastName)
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.PhoneNumber)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(u => u.Email)
               .HasMaxLength(100)
               .IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();

        builder.Property(u => u.PasswordHash)
                .IsRequired();

        builder.Property(u => u.Role)
               .HasMaxLength(20)
               .IsRequired();

        builder.Property(u => u.IsConfirmed)
               .IsRequired();

        builder.Property(u => u.ConfirmationToken)
               .HasMaxLength(200);
    }
}
