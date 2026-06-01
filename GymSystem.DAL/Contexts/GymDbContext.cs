using GymSystem.DAL.Configurations;
using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Contexts
{
   public class GymDbContext : DbContext
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
