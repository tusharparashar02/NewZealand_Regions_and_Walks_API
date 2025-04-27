using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZealand.Data;
using NewZealand.Interface;
using NewZealand.Models.Domain;
using NewZealand.Models.DTO;

namespace NewZealand.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegionsController : ControllerBase
    {
        private readonly NZDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        public RegionsController(NZDbContext dbContext, IRegionRepository regionRepository){
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }
        //GetAll
        [HttpGet]

        public async Task<ActionResult> GetAll(){
            //get data from databse- domain model
            var regionsDomain = await regionRepository.GetAllAsync();
            //map domain models to dtos
            var regionDto = new List<RegionDTO>();
            foreach(var region in regionsDomain){
                regionDto.Add(new RegionDTO(){
                    Id  = region.Id,
                    Code = region.Code,
                    Name = region.Name,
                    RegionImageUrl = region.RegionImageUrl
                });
            }
            //return dto
            return Ok(regionDto);
        }
        //getByID
        [HttpGet]

        [Route("{id:Guid}")]
        public async Task<ActionResult>  GetById(Guid id){
            // var region = dbContext.Regions.Find(id);

            //Now using Linq method
            //get region from the databse
            var region = await regionRepository.GetByIdAsync(id);
            if(region==null){
                return NotFound();
            }
            //map-convert the domain models in to dto
            var regionDto = new RegionDTO{
                Id  = region.Id,
                Code = region.Code,
                Name = region.Name,
                RegionImageUrl = region.RegionImageUrl
            };
            return Ok(regionDto);
        }

        //post
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO){
            //Map or convert DTO to domain models
            var regionDomainModel = new Region{
                Code = addRegionRequestDTO.Code,
                Name = addRegionRequestDTO.Name,
                RegionImageUrl = addRegionRequestDTO.RegionImageUrl
            };
            //use domain model to create region
           regionDomainModel =  await regionRepository.CreateAsync(regionDomainModel);
            //map domain model back to dto
            var regionDTO = new RegionDTO{
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDTO);

        }

        //update
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO ){
            //Map dto to domain model
            var regionDomainModel = new Region{
                Code = updateRegionDTO.Code,
                Name = updateRegionDTO.Name,
                RegionImageUrl = updateRegionDTO.RegionImageUrl
            };
            //Check if region exist
           regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null){
                return NotFound();
            }
            
            //convert domain model to dto
            var regionDTO = new RegionDTO{
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }

        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel== null){
                return NotFound();
            }
            
            //return deleted region back
            //,ap Domain model to dto
            var regionDTO = new RegionDTO{
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDTO);
        }
    } 
}