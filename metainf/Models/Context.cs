using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;

namespace metainf.Models
{
    public class MainContext : DbContext
    {
        Database.SetInitializer<MainContext>(new CreateDatabaseIfNotExists<MainContext>());
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
            MainContext.Database.CreateIfNotExists();
            Database.SetInitializer<MyContext>(new CreateDatabaseIfNotExists<MyContext>());

        }

        //public MainContext() : base()
        //{
        //    Database.SetInitializer<SchoolDBContext>(new CreateDatabaseIfNotExists<SchoolDBContext>());
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
        //        // Duplicate here any configuration sources you use.
        //        configurationBuilder.AddJsonFile(@"C:\Users\23679\source\repos\Metainf\metainf\appsettings.json");
        //        IConfiguration configuration = configurationBuilder.Build();

        //        //warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //        optionsBuilder.UseSqlServer(configuration.GetValue<string>("ConnectionStrings:DefaultDatabase"));
        //    }
        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)Database.GetService<IDatabaseCreator>();
        //    databaseCreator.CreateTables();
        //}

        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}