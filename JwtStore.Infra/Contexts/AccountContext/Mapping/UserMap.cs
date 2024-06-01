﻿using JwtStore.Core.Contexts.AccountContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JwtStore.Infra.Contexts.AccountContext.Mapping;

public class UserMap :IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
       
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasColumnName("Name")
            .HasColumnType("NVARCHAR")
            .HasMaxLength(120)
            .IsRequired();
        
        builder.Property(x => x.Image)
            .HasColumnName("Image")
            .HasColumnType("VARCHAR")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .Property(x => x.Address)
            .HasColumnName("Email")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.Code)
            .HasColumnName("EmailVerificationCode")
            .IsRequired();
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.ExpireAt)
            .HasColumnName("EmailVerificationExpireAt")
            .IsRequired(false);
        
        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Property(x => x.VerifiedAt)
            .HasColumnName("EmailVerificationVerifiedAt")
            .IsRequired(false);

        builder.OwnsOne(x => x.Email)
            .OwnsOne(x => x.Verification)
            .Ignore(x => x.IsActive);
        
        builder.OwnsOne(x => x.Password)
            .Property(x => x.Hash)
            .HasColumnName("PasswordHash")
            .IsRequired();
        
        builder.OwnsOne(x => x.Password)
            .Property(x => x.ResetCode)
            .HasColumnName("PasswordResetCode")
            .IsRequired();

        builder.HasMany(x => x.Roles)
            .WithMany(x => x.Users)
            .UsingEntity<Dictionary<string, object>>(
                "UserRole",
                role => role
                    .HasOne<Role>()
                    .WithMany()
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade),
                user => user
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}