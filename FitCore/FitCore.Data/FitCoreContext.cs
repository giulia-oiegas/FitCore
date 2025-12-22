using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FitCore.Data
{
    public class FitCoreContext : DbContext
    {
        public FitCoreContext()
        {

        }

        public FitCoreContext(DbContextOptions<FitCoreContext> options) : base(options)
        {
        }

        //definim tabelele din sql
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<GymClass> GymClasses { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //configurare fallback (doar daca nu e setat din web project)
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FitCoreDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //aici se configureaza relatii speciale daca e cazul
            //de ex pretul sa aiba 2 zecimale
            modelBuilder.Entity<MembershipType>()
                .Property(m => m.Price)
                .HasColumnType("decimal(18,2)");
        }
    }
}
