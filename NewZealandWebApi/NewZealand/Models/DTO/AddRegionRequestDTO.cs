using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewZealand.Models.DTO
{
    public class AddRegionRequestDTO
    {
        public string Code {get; set;}
        public string Name {get; set;}
        public string? RegionImageUrl {get; set;}
    }
}