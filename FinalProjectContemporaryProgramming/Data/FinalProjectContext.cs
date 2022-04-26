using Microsoft.EntityFrameworkCore;
using FinalProjectContemporaryProgramming.Models;

namespace FinalProjectContemporaryProgramming.Data 
{
    public class FinalProjectContext : DbContext
    {
        public FinalProjectContext(DbContextOptions<FinalProjectContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder builder) 
        {
                builder.Entity<GabesTable>().HasData(
                    new GabesTable { 
                        Id = 1, 
                        FullName = "Gabe Newell", 
                        Birthday = "Jan 1, 1991", 
                        CollegeProgram = "Information Technology", 
                        YearInProgram = "3"}
                );

                builder.Entity<NicksTable>().HasData(
                    new NicksTable { 
                        Id = 1, 
                        FirstName = "Gabe", 
                        LastName = "Newell",
                        FavoriteTa = "exampleTA", 
                        FavoriteTeacher = "Information Technology", 
                        FavoriteClass = "3",
                        IAmALiar= 1 }
                );

                builder.Entity<MattsTable>().HasData(
                    new MattsTable { 
                        Id = 1, 
                        FirstName = "Matt", 
                        LastName = "Caudill",
                        FavoriteBreakfast = "Eggs and Ham", 
                        FavoriteDinner = "Ham", 
                        FavoriteDessert = "Dessert Eggs"
                        }
                );

                builder.Entity<JohnsTable>().HasData(
                    new JohnsTable { 
                        Id = 1, 
                        FirstName = "John", 
                        LastName = "Doe",
                        FavoriteSport = "Handegg", 
                        FavoriteVideoGame = "Knights of the Old Republic II", 
                        FavoriteTVShow = "The Wire"
                        }
                );
        }

        public DbSet<GabesTable> TheGabeTable { get; set; }
        public DbSet<NicksTable> TheNicksTable { get; set; }
        public DbSet<MattsTable> TheMattsTable { get; set; }
        public DbSet<JohnsTable> TheJohnsTable { get; set; }
    }
    
}