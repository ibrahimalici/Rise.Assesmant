using ContactsAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ContactsAPI.Persistance
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration Configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }
        public DbSet<Report> Reports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string cnn = Configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            optionsBuilder.UseNpgsql(cnn);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new KisiConfiguration());
            modelBuilder.ApplyConfiguration(new IletisimConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
        }
    }
}
