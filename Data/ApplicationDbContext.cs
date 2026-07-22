using Microsoft.EntityFrameworkCore;
using VenuBooking.Models;

namespace VenuBooking.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Venue> Venues { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Venue>().ToTable("Venue");
            modelBuilder.Entity<Event>().ToTable("Event");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<EventType>().ToTable("EventType");

            modelBuilder.Entity<EventType>().HasData(
                new EventType { EventTypeID = 1, Name = "Conference" },
                new EventType { EventTypeID = 2, Name = "Wedding" },
                new EventType { EventTypeID = 3, Name = "Concert" },
                new EventType { EventTypeID = 4, Name = "Workshop" },
                new EventType { EventTypeID = 5, Name = "Exhibition" }
            );

            modelBuilder.Entity<Event>()
                .HasOne(e => e.EventType)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.EventTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Venue)
                .WithMany(v => v.Bookings)
                .HasForeignKey(b => b.VenueID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}