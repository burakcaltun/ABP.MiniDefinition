
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
    [Route("api/exchange-rate-entries")]
    [ApiController]
    public class ExchangeRateEntriesController : ControllerBase
    {
        private const string EntityName = "exchangeRateEntry";
        private readonly ILogger<ExchangeRateEntriesController> _log;
        private readonly IMapper _mapper;
        private readonly IExchangeRateEntryService _exchangeRateEntryService;

        public ExchangeRateEntriesController(ILogger<ExchangeRateEntriesController> log,
        IMapper mapper,
        IExchangeRateEntryService exchangeRateEntryService)
        {
            _log = log;
            _mapper = mapper;
            _exchangeRateEntryService = exchangeRateEntryService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<ExchangeRateEntryDto>> CreateExchangeRateEntry([FromBody] ExchangeRateEntryDto exchangeRateEntryDto)
        {
            _log.LogDebug($"REST request to save ExchangeRateEntry : {exchangeRateEntryDto}");
            if (exchangeRateEntryDto.Id != 0)
                throw new BadRequestAlertException("A new exchangeRateEntry cannot already have an ID", EntityName, "idexists");

            ExchangeRateEntry exchangeRateEntry = _mapper.Map<ExchangeRateEntry>(exchangeRateEntryDto);
            await _exchangeRateEntryService.Save(exchangeRateEntry);
            return CreatedAtAction(nameof(GetExchangeRateEntry), new { id = exchangeRateEntry.Id }, exchangeRateEntry)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, exchangeRateEntry.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateExchangeRateEntry(long id, [FromBody] ExchangeRateEntryDto exchangeRateEntryDto)
        {
            _log.LogDebug($"REST request to update ExchangeRateEntry : {exchangeRateEntryDto}");
            if (exchangeRateEntryDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != exchangeRateEntryDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            ExchangeRateEntry exchangeRateEntry = _mapper.Map<ExchangeRateEntry>(exchangeRateEntryDto);
            await _exchangeRateEntryService.Save(exchangeRateEntry);
            return Ok(exchangeRateEntry)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, exchangeRateEntry.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExchangeRateEntryDto>>> GetAllExchangeRateEntries(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of ExchangeRateEntries");
            var result = await _exchangeRateEntryService.FindAll(pageable);
            var page = new Page<ExchangeRateEntryDto>(result.Content.Select(entity => _mapper.Map<ExchangeRateEntryDto>(entity)).ToList(), pageable, result.TotalElements);
            return Ok(((IPage<ExchangeRateEntryDto>)page).Content).WithHeaders(page.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExchangeRateEntry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get ExchangeRateEntry : {id}");
            var result = await _exchangeRateEntryService.FindOne(id);
            ExchangeRateEntryDto exchangeRateEntryDto = _mapper.Map<ExchangeRateEntryDto>(result);
            return ActionResultUtil.WrapOrNotFound(exchangeRateEntryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExchangeRateEntry([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete ExchangeRateEntry : {id}");
            await _exchangeRateEntryService.Delete(id);
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
