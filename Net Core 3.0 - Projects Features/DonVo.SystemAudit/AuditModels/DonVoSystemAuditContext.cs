using Microsoft.EntityFrameworkCore;

namespace DonVo.SystemAudit.AuditModels
{
    public partial class DonVoSystemAuditContext : DbContext
    {
        public DonVoSystemAuditContext()
        {
        }

        public DonVoSystemAuditContext(DbContextOptions<DonVoSystemAuditContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AuditContosoRetailDw> AuditContosoRetailDW { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuditContosoRetailDw>(entity =>
            {
                entity.HasKey(e => e.Guid);

                entity.ToTable("AuditContosoRetailDW");

                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ColumnName).HasMaxLength(150);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.EmailUser)
                    .HasMaxLength(150)
                    .IsFixedLength();

                entity.Property(e => e.IpUser).HasMaxLength(50);

                entity.Property(e => e.OperationName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TableName)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
