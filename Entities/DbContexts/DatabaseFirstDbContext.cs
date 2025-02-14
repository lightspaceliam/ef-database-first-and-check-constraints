using Microsoft.EntityFrameworkCore;

namespace Entities.DbContexts;

public class DatabaseFirstDbContext : DbContext
{
	public DatabaseFirstDbContext(DbContextOptions<DatabaseFirstDbContext> options)
		: base (options) { }

	public DatabaseFirstDbContext() { }
	
	public DbSet<CareTeam> CareTeams { get; set; }
	public DbSet<Patient> Patients { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Server=tcp:localhost,1433;Initial Catalog=DatabaseFirstDb;Persist Security Info=False;User ID=SA;Password=Local@DevPa55word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
	
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		
	}
}