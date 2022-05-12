using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TicketGenerator.Models
{
    public partial class TickectCreatingDbContext : DbContext
    {
        public TickectCreatingDbContext()
        {
        }

        public TickectCreatingDbContext(DbContextOptions<TickectCreatingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Login> Logins { get; set; } = null!;
        public virtual DbSet<MultipleComment> MultipleComments { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Tickect> Tickects { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=TickectCreatingDb;Integrated Security=SSPI");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Login>(entity =>
            {
                entity.HasKey(e => e.Uid)
                    .HasName("PK__login__C5B1966238AD55E7");

                entity.ToTable("login");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("EmailID");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PassWord)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNo)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.SignIn).HasColumnType("datetime");

                entity.Property(e => e.SignOut).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Logins)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__login__RoleID__398D8EEE");
            });

            modelBuilder.Entity<MultipleComment>(entity =>
            {
                entity.ToTable("MultipleComment");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Comments)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.MultipleComments)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK__MultipleCom__UId__4316F928");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tickect>(entity =>
            {
                entity.HasKey(e => e.TicketNo)
                    .HasName("PK__tickects__712CCE6474684089");

                entity.ToTable("tickects");

                entity.Property(e => e.AssignTo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.DateAndTime).HasColumnType("datetime");

                entity.Property(e => e.Message)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.Tickects)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK__tickects__UId__403A8C7D");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserRole__1788CCACEA3FB612");

                entity.ToTable("UserRole");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Uid).HasColumnName("UId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__UserRole__RoleID__3C69FB99");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.Uid)
                    .HasConstraintName("FK__UserRole__UId__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
