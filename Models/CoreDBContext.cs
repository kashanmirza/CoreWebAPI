using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoreWebAPI.Models
{
    public partial class CoreDBContext : DbContext
    {
        public CoreDBContext()
        {
        }

        public CoreDBContext(DbContextOptions<CoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Permission> SecPermissions { get; set; }
        public virtual DbSet<SecRolePermissions> SecRolePermissions { get; set; }
        public virtual DbSet<SecRolePermissionsInterim> SecRolePermissionsInterim { get; set; }
        public virtual DbSet<SecRoles> SecRoles { get; set; }
        public virtual DbSet<SecRolesInterim> SecRolesInterim { get; set; }
        public virtual DbSet<SecUserRoles> SecUserRoles { get; set; }
        public virtual DbSet<SecUsers> SecUsers { get; set; }
        public virtual DbSet<SecUsersInterim> SecUsersInterim { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
                //                See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.

                optionsBuilder.UseSqlServer("Server=BSD-024\\KASHANDB;Database=CoreDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => e.SecPermissionId);

                entity.Property(e => e.ApiUrl).HasColumnName("ApiURL");

                entity.Property(e => e.ComponentUrl).HasColumnName("ComponentURL");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Icon).HasColumnName("icon");

                entity.Property(e => e.PermissionName).IsRequired();

                entity.Property(e => e.Text).IsRequired();

                entity.Property(e => e.Type).HasMaxLength(100);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecRolePermissions>(entity =>
            {
                entity.HasKey(e => e.SecRolePermissionId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecRolePermissionsInterim>(entity =>
            {
                entity.HasKey(e => e.SecRolePermissionId);

                entity.ToTable("SecRolePermissions_Interim");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecRoles>(entity =>
            {
                entity.HasKey(e => e.SecRoleId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecRolesInterim>(entity =>
            {
                entity.HasKey(e => e.SecRoleId);

                entity.ToTable("SecRoles_Interim");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecUserRoles>(entity =>
            {
                entity.HasKey(e => e.SecUserRoleId);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<SecUsers>(entity =>
            {
                entity.HasKey(e => e.SecUserId);

                entity.Property(e => e.Channel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IntialName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.OtpEmail).HasColumnName("OTP_Email");

                entity.Property(e => e.OtpPhoneNumber).HasColumnName("OTP_PhoneNumber");

                entity.Property(e => e.TokenExpireOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<SecUsersInterim>(entity =>
            {
                entity.HasKey(e => e.SecUserId);

                entity.ToTable("SecUsers_Interim");

                entity.Property(e => e.Channel)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.IntialName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.OtpEmail).HasColumnName("OTP_Email");

                entity.Property(e => e.OtpPhoneNumber).HasColumnName("OTP_PhoneNumber");

                entity.Property(e => e.TokenExpireOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(256);
            });
        }
    }
}
