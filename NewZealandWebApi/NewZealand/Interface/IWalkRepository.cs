using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewZealand.Models.Domain;

namespace NewZealand.Interface
{
    public interface IWalkRepository
    {
       Task<Walk> CreateAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true
        , int pageNumber=1 , int pagesize = 100);
        Task<Walk?> GetByIdAsync(Guid id);
        Task<Walk> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteAsync(Guid id);
    }
}