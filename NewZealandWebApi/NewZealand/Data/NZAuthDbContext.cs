using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NewZealand.Data
{
    public class NZAuthDbContext : IdentityDbContext
    {
        public NZAuthDbContext(DbContextOptions<NZAuthDbContext> options): base(options){

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "9471469b-1b73-4e84-9d16-550fad3146a4";
            var  writterRoleId ="6d8b50e3-9dee-4570-9209-73e2dc4bc9ed" ;

            var roles = new List<IdentityRole>{
                new IdentityRole{
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole{
                    Id = writterRoleId,
                    ConcurrencyStamp = writterRoleId,
                    Name = "Writer",
                    NormalizedName = "writer".ToUpper()
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}