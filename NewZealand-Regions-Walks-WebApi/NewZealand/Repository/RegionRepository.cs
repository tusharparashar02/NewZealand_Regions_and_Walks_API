using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewZealand.Data;
using NewZealand.Interface;
using NewZealand.Models.Domain;

namespace NewZealand.Repository
{
    public class RegionRepository : IRegionRepository{
        private readonly NZDbContext dbContext;
        public RegionRepository(NZDbContext dbContext){
            this.dbContext = dbContext;
        }
        public async  Task<List<Region>> GetAllAsync(){
            return await dbContext.Regions.ToListAsync();
        }
        public async Task<Region?> GetByIdAsync(Guid id){
            return await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id== id);
        }
        public async  Task<Region> CreateAsync(Region region){
             await dbContext.Regions.AddAsync(region);
             await dbContext.SaveChangesAsync();
             return region;
        }
        public async Task<Region?> UpdateAsync(Guid id, Region region){
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id == id);
            if(existingRegion == null){
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;
            
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
        public async Task<Region?> DeleteAsync(Guid id){
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id  == id);
            if(existingRegion==null){
                return null;
            }
            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }
    }
}