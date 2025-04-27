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
        public NZDbContext(DbContextOptions dbContextOptions): base(dbContextOptions){

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        
    }
}