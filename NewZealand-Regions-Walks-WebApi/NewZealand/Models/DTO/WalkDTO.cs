using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewZealand.Models.DTO
{
    public class WalkDTO
    {
         public Guid Id{ get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
        public double LengthInKm {get; set;}
        public string? WalkImageUrl {get; set;}
        public Guid DifficultyId {get; set;}

        public Guid RegionId {get; set;}
        public RegionDTO Region{get; set;}
        public DifficultyDTO Difficulty {get; set;}

    }
}