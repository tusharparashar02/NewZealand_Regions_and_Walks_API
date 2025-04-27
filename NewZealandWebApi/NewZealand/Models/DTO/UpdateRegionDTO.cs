using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewZealand.Models.DTO
{
    public class UpdateRegionDTO
    {
        [Required]
        [MinLength(3,ErrorMessage=("Code is minimum of 3 Character"))]
        [MaxLength(3, ErrorMessage=("Code is maximum of 3 Character"))]
        public string Code {get; set;}
        [Required]
        public string Name {get; set;}
        public string? RegionImageUrl {get; set;}
    }
}