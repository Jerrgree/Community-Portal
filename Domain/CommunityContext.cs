using System;using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Domain.Models;

namespace Domain
{
    public class CommunityContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: Move connection to config
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=CommunityDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.UserName);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsRequired(true);

                entity.Property(e => e.IsActive)
                    .IsRequired(true)
                    .HasDefaultValue(true);

                entity.Property(e => e.Role_ID)
                    .IsRequired(true)
                    .HasDefaultValue(1);

                entity.HasOne(u => u.Role)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.Role_ID)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("FK_User_UserRole");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasAlternateKey(e => e.Name);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsRequired(true);

            });

            modelBuilder.Entity<UserRole>().HasData
                (
                new UserRole { Id = 1, Name = "User" },
                new UserRole { Id = 2, Name = "Moderator" },
                new UserRole { Id = 3, Name = "Admin" }
                );
        }
    }
}
