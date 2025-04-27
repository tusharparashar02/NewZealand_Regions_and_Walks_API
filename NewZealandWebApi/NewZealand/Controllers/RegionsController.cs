using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewZealand.CustomActionFilters;
using NewZealand.Data;
using NewZealand.Interface;
using NewZealand.Models.Domain;
using NewZealand.Models.DTO;

namespace NewZealand.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private readonly NZDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;
        public RegionsController(NZDbContext dbContext, IRegionRepository regionRepository, IMapper mapper){
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        //GetAll
        [HttpGet]
        [Authorize(Roles ="Reader")]
        public async Task<ActionResult> GetAll(){
            //get data from databse- domain model
            var regionsDomain = await regionRepository.GetAllAsync();
            //map by automapper
            var regionDto = mapper.Map<List<RegionDTO>>(regionsDomain);
            //return dto
            return Ok(regionDto);
        }
        //getByID
        [HttpGet]

        [Route("{id:Guid}")]
        [Authorize(Roles ="Reader")]
        public async Task<ActionResult>  GetById(Guid id){
            //get region from the databse
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if(regionDomain==null){
                return NotFound();
            }
            //map-convert the domain models in to dto
            var regionDto = mapper.Map<RegionDTO>(regionDomain);
            return Ok(regionDto);
        }

        //post
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles ="Writer")]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDTO addRegionRequestDTO){
            //Map or convert DTO to domain models
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDTO);
            //use domain model to create region
           regionDomainModel =  await regionRepository.CreateAsync(regionDomainModel);
            //map domain model back to dto
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return CreatedAtAction(nameof(GetById), new {id = regionDomainModel.Id}, regionDTO);
        }

        //update
        [HttpPut]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDTO updateRegionDTO ){
            //Map dto to domain model
            var regionDomainModel = mapper.Map<Region>(updateRegionDTO);
            //Check if region exist
           regionDomainModel =  await regionRepository.UpdateAsync(id, regionDomainModel);
            if(regionDomainModel == null){
                return NotFound();
            }
            
            //convert domain model to dto
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);
        }

        //Delete
        [HttpDelete]
        [Route("{id:Guid}")]
        [Authorize(Roles ="Writer, Reader")]
        public async Task<IActionResult> Delete([FromRoute] Guid id){
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel== null){
                return NotFound();
            }
            
            //return deleted region back
            //,ap Domain model to dto
            var regionDTO = mapper.Map<RegionDTO>(regionDomainModel);
            return Ok(regionDTO);
        }
    } 
}