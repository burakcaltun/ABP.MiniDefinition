
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
    [Route("api/currencies")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private const string EntityName = "currency";
        private readonly ILogger<CurrenciesController> _log;
        private readonly IMapper _mapper;
        private readonly ICurrencyService _currencyService;

        public CurrenciesController(ILogger<CurrenciesController> log,
        IMapper mapper,
        ICurrencyService currencyService)
        {
            _log = log;
            _mapper = mapper;
            _currencyService = currencyService;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ActionResult<CurrencyDto>> CreateCurrency([FromBody] CurrencyDto currencyDto)
        {
            _log.LogDebug($"REST request to save Currency : {currencyDto}");
            if (currencyDto.Id != 0)
                throw new BadRequestAlertException("A new currency cannot already have an ID", EntityName, "idexists");

            Currency currency = _mapper.Map<Currency>(currencyDto);
            await _currencyService.Save(currency);
            return CreatedAtAction(nameof(GetCurrency), new { id = currency.Id }, currency)
                .WithHeaders(HeaderUtil.CreateEntityCreationAlert(EntityName, currency.Id.ToString()));
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCurrency(long id, [FromBody] CurrencyDto currencyDto)
        {
            _log.LogDebug($"REST request to update Currency : {currencyDto}");
            if (currencyDto.Id == 0) throw new BadRequestAlertException("Invalid Id", EntityName, "idnull");
            if (id != currencyDto.Id) throw new BadRequestAlertException("Invalid Id", EntityName, "idinvalid");
            Currency currency = _mapper.Map<Currency>(currencyDto);
            await _currencyService.Save(currency);
            return Ok(currency)
                .WithHeaders(HeaderUtil.CreateEntityUpdateAlert(EntityName, currency.Id.ToString()));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetAllCurrencies(IPageable pageable)
        {
            _log.LogDebug("REST request to get a page of Currencies");
            var result = await _currencyService.FindAll(pageable);
            var page = new Page<CurrencyDto>(result.Content.Select(entity => _mapper.Map<CurrencyDto>(entity)).ToList(), pageable, result.TotalElements);
            return Ok(((IPage<CurrencyDto>)page).Content).WithHeaders(page.GeneratePaginationHttpHeaders());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCurrency([FromRoute] long id)
        {
            _log.LogDebug($"REST request to get Currency : {id}");
            var result = await _currencyService.FindOne(id);
            CurrencyDto currencyDto = _mapper.Map<CurrencyDto>(result);
            return ActionResultUtil.WrapOrNotFound(currencyDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrency([FromRoute] long id)
        {
            _log.LogDebug($"REST request to delete Currency : {id}");
            await _currencyService.Delete(id);
            return NoContent().WithHeaders(HeaderUtil.CreateEntityDeletionAlert(EntityName, id.ToString()));
        }
    }
}
