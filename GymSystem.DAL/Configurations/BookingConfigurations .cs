using GymSystem.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Configurations
{
   internal class BookingConfigurations : IEntityTypeConfiguration<Booking>
    {

        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Ignore(X => X.Id);
            builder.Property(X => X.CreatedAt)
                   .HasColumnName("BookingDate")
                   .HasDefaultValueSql("GETDATE()");

            builder.HasOne(X => X.Session)
       .WithMany(X => X.Bookings)
       .HasForeignKey(X => X.SessionId)
       .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(X => X.Member)
        .WithMany(X => X.Bookings)
        .HasForeignKey(X => X.MemberId)
        .OnDelete(DeleteBehavior.NoAction); 

            builder.HasKey(X => new { X.SessionId, X.MemberId });
        }
    }
}
