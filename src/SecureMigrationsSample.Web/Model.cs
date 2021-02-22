using Microsoft.EntityFrameworkCore;

namespace SecureMigrationsSample.Web
{
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options)
            : base(options)
        {
        }

        public DbSet<Secret> Chamber { get; set; }
    }

    public class Secret
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}