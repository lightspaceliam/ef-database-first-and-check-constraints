// See https://aka.ms/new-console-template for more information

using Entities.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var host = Host.CreateDefaultBuilder(args)
	.ConfigureAppConfiguration((context, config) =>
	{
		/*
		 HACK: override the project base path for simple use of reading the appsettings file.
		 Normally sensitive configuration properties stored in UserSecrets or a Key Vault.
		 */
		// config.AddUserSecrets<Program>();
		
		var projectPath = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
		config.SetBasePath(projectPath);
		
		//  Pick up configuration settings.
		config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
	})
	.ConfigureLogging(logging =>
	{
		logging.AddConsole();
	})
	.ConfigureServices((hostContext, services) =>
	{
		services.AddLogging();
		
		services.AddDbContext<DatabaseFirstDbContext>(options =>
		{
			options.UseSqlServer(
				hostContext.Configuration["ConnectionStrings:DockerConnection"],
				optionsBuilder =>
				{
					optionsBuilder.ExecutionStrategy(
						context => new SqlServerRetryingExecutionStrategy(context, 10, TimeSpan.FromMilliseconds(200), null));
				});
		});
	})
	.Build();

var context = host.Services.GetRequiredService<DatabaseFirstDbContext>();

var patients = await context.Patients
	.Include(p => p.CareTeam)
	.ToListAsync();

Console.WriteLine("\nQuery patient data and print to console:\n");
foreach (var patient in patients)
{
	Console.WriteLine($"Patient: {patient.FullName}, in care team: {patient.CareTeam.Name}");
}

Console.WriteLine("\nHello, brief demo to migrating to Entity Framework!");