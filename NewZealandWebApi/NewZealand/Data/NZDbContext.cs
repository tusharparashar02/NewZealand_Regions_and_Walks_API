using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewZealand.Models.Domain;

namespace NewZealand.Data
{
    public class NZDbContext : DbContext
    {
        public NZDbContext(DbContextOptions<NZDbContext> dbContextOptions): base(dbContextOptions){

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Image> Images {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed the data for difficulties
            //Easy, hard, medium
            var difficulties = new List<Difficulty>(){
                new Difficulty(){
                    Id = Guid.Parse("981c0b18-c325-41c6-b52d-af49c3abfdae"),
                    Name = "Easy"
                },
                new Difficulty(){
                    Id = Guid.Parse("30547172-2b6a-4d45-92c6-d4f57a0302db"),
                    Name = "Medium"
                },
                new Difficulty(){
                    Id = Guid.Parse("d696e25f-643e-4f9a-891e-204170e41a32"),
                    Name = "Hard"
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for region
            var regions = new List<Region>{
                new Region{
                    Id = Guid.Parse("551f264a-8f7d-47f3-b7d5-a49e33d08c53"),
                    Name = "Noida",
                    Code = "UP",
                    RegionImageUrl = "https://media.gettyimages.com/id/1224427501/photo/aerial-night-shot-of-buildings-with-homes-and-offices-with-more-skyscrapers-in-the-distance.jpg?s=612x612&w=gi&k=20&c=hJkyRqEHkK9NjoAwuNtKTHJPZ1LvvaRT5A0Ns_fP7JE="
                },
                 new Region{
                    Id = Guid.Parse("28bb9c0c-6372-4acf-9247-06d65e3c59f6"),
                    Name = "Gurugram",
                    Code = "Harayana",
                    RegionImageUrl = "https://media.gettyimages.com/id/1224427501/photo/aerial-night-shot-of-buildings-with-homes-and-offices-with-more-skyscrapers-in-the-distance.jpg?s=612x612&w=gi&k=20&c=hJkyRqEHkK9NjoAwuNtKTHJPZ1LvvaRT5A0Ns_fP7JE="
                }
            };

            modelBuilder.Entity<Region>().HasData(regions);
     } 
    }
}