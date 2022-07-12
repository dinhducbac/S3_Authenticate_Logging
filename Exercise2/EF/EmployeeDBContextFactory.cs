using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Exercise2.EF
{
    public class EmployeeDBContextFactory : IDesignTimeDbContextFactory<EmployeeDBContext>
    {
        public EmployeeDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var connectStrings = configuration.GetConnectionString("EmployeeConnectionStrings");
            var optionsBuilder = new DbContextOptionsBuilder<EmployeeDBContext>();
            optionsBuilder.UseMySQL(connectStrings);

            return new EmployeeDBContext(optionsBuilder.Options);
        }
    }
}
