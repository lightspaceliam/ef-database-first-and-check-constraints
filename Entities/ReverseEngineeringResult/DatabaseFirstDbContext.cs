using Microsoft.EntityFrameworkCore;

namespace Entities.ReverseEngineeringResult;

/// <summary>
/// Output from EF's reverse engineering.
/// </summary>
public partial class ReverseEngineeringDatabaseFirstDbContext : DbContext
{
    public ReverseEngineeringDatabaseFirstDbContext()
    {
    }

    public ReverseEngineeringDatabaseFirstDbContext(DbContextOptions<ReverseEngineeringDatabaseFirstDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<tbl_CareTeam> tbl_CareTeams { get; set; }

    public virtual DbSet<Patient> tbl_Patients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Initial Catalog=DatabaseFirstDb;Persist Security Info=False;User ID=SA;Password=Local@DevPa55word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<tbl_CareTeam>(entity =>
        {
            entity.HasKey(e => e.iCareTeamID).HasName("PK_tbl_CareTeams_iCareTeamId");
        });

        modelBuilder.Entity<tbl_Patient>(entity =>
        {
            entity.HasKey(e => e.iPatientID).HasName("PK_tbl_Patients_iPatientID");

            entity.HasOne(d => d.iCareTeam).WithMany(p => p.tbl_Patients).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
