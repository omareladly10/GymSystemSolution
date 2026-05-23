using GymSystem.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GymSystem.Models;

namespace GymSystem.Contexts
{
    public class GymDbContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;database=GymDb;trusted_connection=true;trustserverCertificate=true");


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Plan>(new PlanConfigurations());

        }


        public DbSet<Plan> Plans { get; set; }  

    }
}
