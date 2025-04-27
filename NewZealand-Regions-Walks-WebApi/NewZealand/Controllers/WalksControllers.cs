using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewZealand.CustomActionFilters;
using NewZealand.Interface;
using NewZealand.Models.Domain;
using NewZealand.Models.DTO;

namespace NewZealand.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksControllers : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;
        public WalksControllers(IMapper mapper, IWalkRepository walkRepository){
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }
        //Create walks
        //post api
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDTO addWalkRequestDTO)
        {
            //map dto
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDTO);
            await walkRepository.CreateAsync(walkDomainModel);
            //map domain to dto
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }
        //GetAllWalks
        //Get: /api/walks?filterOn = Name&filterQuery= Track & sortBy = Name&IsAccending = true & pageNumber = 1 & pagesize = 10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
         [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pagesize = 100){
            var walkDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
             pageNumber, pagesize);
            //map domain to dto
            return Ok(mapper.Map<List<WalkDTO>>(walkDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id){
            var walkDomainModel = await walkRepository.GetByIdAsync(id);
            if(walkDomainModel == null){
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        } 

        //update
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDTO updateWalkRequestDTO){
            //map dto to model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDTO);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);
            if(walkDomainModel == null){
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomainModel));
        }

        //delete
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] Guid id){
            var deleteWalkModel = await walkRepository.DeleteAsync(id);
            if(deleteWalkModel == null){
                return NotFound();
            }
            //map domain model to dto
            return Ok(mapper.Map<WalkDTO>(deleteWalkModel));
        }
    }
}