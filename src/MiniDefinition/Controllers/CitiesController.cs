
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using JHipsterNet.Core.Pagination;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Crosscutting.Exceptions;
using MiniDefinition.Dto;
using MiniDefinition.Web.Extensions;
using MiniDefinition.Web.Filters;
using MiniDefinition.Web.Rest.Utilities;
using AutoMapper;
using System.Linq;
using MiniDefinition.Domain.Repositories.Interfaces;
using MiniDefinition.Domain.Services.Interfaces;
using MiniDefinition.Infrastructure.Web.Rest.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace MiniDefinition.Controllers
{
    [Authorize]
    [Route("api/cities")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private const string EntityName = "city";
        private readonly ILogger<CitiesController> _log;
        private readonly IMapper _mapper;
        private readonly ICityService _cityService;

        public CitiesController(ILogger<CitiesController> log,
        IMapper mapper,
        ICityService cityService)
        {
            _log = log;
            _mapper = mapper;
            _cityService = cityService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CityDto>> CreateCity([FromBody] CityDto cityDto)
        {
            _log.LogDebug($"REST request to save City : {cityDto}");
            if (cityDto.Id != 0)
                throw new BadRequestAlertException("A new city cannot already have an ID", EntityName, "idexists");

            City city = _mapper.Map<City>(cityDto);
            await _cityService.Save(city);
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, city.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCity(long id, [FromBody] CityDto cityDto)
        {
            _log.LogDebug($"REST request to update City : {cityDto}");
            if (cityDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != cityDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            City city = _mapper.Map<City>(cityDto);
            await _cityService.Save(city);
            return Ok(city)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, city.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetAllCities(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Cities");
            var result = await _cityService.FindAll(pageable);
            var page = new Page<CityDto>(result.Content.Select(entity => _mapper.Map<CityDto>(entity)).ToList(), pageable, result.TotalElements);
            return Ok(((IPage<CityDto>)page).Content).WithHeaders(page.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get City : {id}");
            var result = await _cityService.FindOne(id);
            CityDto cityDto = _mapper.Map<CityDto>(result);
            return ActionResultUtil.WrapOrNotFound(cityDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete City : {id}");
            await _cityService.Delete(id);
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
