using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace metainf.Models
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();
            databaseCreator.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Connection> Connection { get; set; }
        public DbSet<FromTo> FromTo { get; set; }
        public DbSet<Column> Column { get; set; }
    }
}