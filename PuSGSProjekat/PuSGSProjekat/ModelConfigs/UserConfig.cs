﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PuSGSProjekat.Models;

namespace PuSGSProjekat.ModelConfigs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            //valid
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Email).IsRequired().HasMaxLength(20);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Password).IsRequired().HasMaxLength(150);

            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(20);

            builder.Property(x => x.LastName).IsRequired().HasMaxLength(20);

            builder.Property(x => x.Birthdate).HasMaxLength(20);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(20);

            builder.Property(x => x.UserType).HasConversion<string>();

            builder.Property(x => x.VerificationState).HasConversion<string>();
        }
    }
}
