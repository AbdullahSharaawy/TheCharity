using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TheCharityDAL.Database
{
    public class TheCharityDbContextFactory : IDesignTimeDbContextFactory<TheCharityDbContext>
    {
        public TheCharityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TheCharityDbContext>();

            // Hardcode your connection string here for migrations
            optionsBuilder.UseSqlServer("Server=.;Database=Charity;Integrated Security=True;TrustServerCertificate=True;");

            return new TheCharityDbContext(optionsBuilder.Options);
        }
    }
}
