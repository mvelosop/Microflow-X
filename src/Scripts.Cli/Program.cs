using System;
using Microsoft.EntityFrameworkCore;
using Tenants.Data.Configuration;

namespace Scripts.Cli
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Host for EF scripts");

			// 1-2. Initialize DbContext and apply migration to verify it's working
			//---------------------------------------------------------------------

			string connectionString =
				"Server=localhost; Initial Catalog=MicroFlow.Cli; Trusted_Connection=true; MultipleActiveResultSets=true;";

			var optionsBuilder = new DbContextOptionsBuilder<TenantsDbContext>();

			optionsBuilder.UseSqlServer(connectionString);

			Console.WriteLine("Creating database / applying migrations...");

			using (var dbContext = new TenantsDbContext(optionsBuilder.Options))
			{
				dbContext.Database.Migrate();
			}

			Console.WriteLine("Done!");
		}
	}
}
