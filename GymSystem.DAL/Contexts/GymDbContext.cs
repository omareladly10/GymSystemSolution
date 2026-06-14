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

      
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //    {
        //        optionsBuilder.UseSqlServer("Server=.;database=GymDb;trusted_connection=true;trustserverCertificate=true");


        //    }

        public GymDbContext(DbContextOptions<GymDbContext> options) : base(options)
        
        {
        
        
        }


            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {

            
                     modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbContext).Assembly);

            }


        public DbSet<Plan> Plans { get; set; }

             

                public DbSet<Booking> Bookings { get; set; }

                public DbSet<Category> Categorys { get; set; }

               

                public DbSet<HealthRecord> HealthRecords { get; set; }


                public DbSet<Member> Members { get; set; }


                public DbSet<Membership> Memberships { get; set; }


                public DbSet<Session> Sessions { get; set; }

                public DbSet<Trainer> Trainers { get; set; }



   }
}
  
