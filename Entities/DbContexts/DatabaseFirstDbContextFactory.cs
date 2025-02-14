using Microsoft.EntityFrameworkCore;

namespace Entities.DbContexts;

public class DatabaseFirstDbContextFactory
{
	public DatabaseFirstDbContext CreateDbContext(string[] args)
	{
		var optionsBuilder = new DbContextOptionsBuilder<DatabaseFirstDbContext>();
		//  Local Windows - not tested.
		// optionsBuilder.UseSqlServer("Server=localhost;Database=DatabaseFirstDb;Integrated Security=True;MultipleActiveResultSets=True");

		//  Docker - you will need to add, name and set your own: container and credentials.
		optionsBuilder.UseSqlServer(
			"Server=tcp:localhost,1433;Initial Catalog=DatabaseFirstDb;Persist Security Info=False;User ID=SA;Password=Local@DevPa55word;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

		return new DatabaseFirstDbContext(optionsBuilder.Options);
	}
}