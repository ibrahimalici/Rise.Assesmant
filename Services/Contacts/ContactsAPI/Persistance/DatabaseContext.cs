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

        public DbSet<Kisi> Kisiler { get; set; }
        public DbSet<Iletisim> IletisimBilgileri { get; set; }

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
        }
    }
}
