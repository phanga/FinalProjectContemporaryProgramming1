using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace FinalProjectContemporaryProgramming.Models.DataLayer
{
    public partial class ContemporaryProgramming_Final_Actual_Context : DbContext
    {
        public ContemporaryProgramming_Final_Actual_Context()
        {
        }

        public ContemporaryProgramming_Final_Actual_Context(DbContextOptions<ContemporaryProgramming_Final_Actual_Context> options)
            : base(options)
        {
        }

        public virtual DbSet<GabeTable> GabeTable { get; set; }
        public virtual DbSet<JohnTable> JohnTable { get; set; }
        public virtual DbSet<MattTable> MattTable { get; set; }
        public virtual DbSet<NicksTable> NickTable { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GabeTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CollegeProgram).IsUnicode(false);

                entity.Property(e => e.FullName).IsUnicode(false);

                entity.Property(e => e.YearInProgram).IsUnicode(false);
            });

            modelBuilder.Entity<JohnTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteSport).IsUnicode(false);

                entity.Property(e => e.FavoriteTvshow).IsUnicode(false);

                entity.Property(e => e.FavoriteVideoGame).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            modelBuilder.Entity<MattTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteBreakfeast).IsUnicode(false);

                entity.Property(e => e.FavoriteDessert).IsUnicode(false);

                entity.Property(e => e.FavoriteDinner).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            modelBuilder.Entity<NicksTable>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.FavoriteClass).IsUnicode(false);

                entity.Property(e => e.FavoriteTa).IsUnicode(false);

                entity.Property(e => e.FavoriteTeacher).IsUnicode(false);

                entity.Property(e => e.FirstName).IsUnicode(false);

                entity.Property(e => e.LastName).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
