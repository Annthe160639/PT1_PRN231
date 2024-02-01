using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.Models
{
    public partial class PRN292_SU17_1Context : DbContext
    {
        public PRN292_SU17_1Context()
        {
        }

        public PRN292_SU17_1Context(DbContextOptions<PRN292_SU17_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<DummyDetail> DummyDetails { get; set; } = null!;
        public virtual DbSet<DummyMaster> DummyMasters { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=.\\ANNT;database=PRN292_SU17_1;uid=g3;pwd=123456; TrustServerCertificate=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DummyDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId);

                entity.ToTable("DummyDetail");

                entity.Property(e => e.DetailId)
                    .ValueGeneratedNever()
                    .HasColumnName("detail_id");

                entity.Property(e => e.DetailName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("detail_name");

                entity.Property(e => e.MasterId).HasColumnName("master_id");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.DummyDetails)
                    .HasForeignKey(d => d.MasterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DummyDetail_DummyMaster");
            });

            modelBuilder.Entity<DummyMaster>(entity =>
            {
                entity.HasKey(e => e.MasterId);

                entity.ToTable("DummyMaster");

                entity.Property(e => e.MasterId)
                    .ValueGeneratedNever()
                    .HasColumnName("master_id");

                entity.Property(e => e.MasterName)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("master_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
