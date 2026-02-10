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
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-6T7MQMA;Initial Catalog=TheCharity;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");

            return new TheCharityDbContext(optionsBuilder.Options);
        }
    }
}
