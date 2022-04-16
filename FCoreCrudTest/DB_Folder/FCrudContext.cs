using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using FCoreCrudTest.Models;

#nullable disable

namespace FCoreCrudTest.DB_Folder
{
    public partial class FCrudContext : DbContext
    {
        public FCrudContext()
        {
        }

        public FCrudContext(DbContextOptions<FCrudContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FuserTable> FuserTables { get; set; }
        public virtual DbSet<LoginTable> LoginTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=CHETUIWK1144\\SQLSERVER;Database=FCrud;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuserTable>(entity =>
            {
                entity.ToTable("FUserTable");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Age)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Technology)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LoginTable>(entity =>
            {
                entity.ToTable("LoginTable");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<FCoreCrudTest.Models.User_Class> User_Class { get; set; }

        public DbSet<FCoreCrudTest.Models.LogInModel> LogInModel { get; set; }
    }
}
