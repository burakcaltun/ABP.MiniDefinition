using System;

using AutoMapper;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Extensions;
using MiniDefinition.Infrastructure.Data;
using MiniDefinition.Domain.Entities;
using MiniDefinition.Domain.Repositories.Interfaces;
using MiniDefinition.Dto;
using MiniDefinition.Configuration.AutoMapper;
using MiniDefinition.Test.Setup;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Xunit;

namespace MiniDefinition.Test.Controllers
{
    public class CurrenciesControllerIntTest
    {
        public CurrenciesControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _currencyRepository = _factory.GetRequiredService<ICurrencyRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = config.CreateMapper();

            InitTest();
        }

        private const string DefaultCode = "AAAAAAAAAA";
        private const string UpdatedCode = "BBBBBBBBBB";

        private const string DefaultName = "AAAAAAAAAA";
        private const string UpdatedName = "BBBBBBBBBB";

        private static readonly int? DefaultNumber = 1;
        private static readonly int? UpdatedNumber = 2;

        private static readonly bool? DefaultIsPassive = false;
        private static readonly bool? UpdatedIsPassive = true;

        private static readonly DateTime DefaultDatePassive = DateTime.UnixEpoch;
        private static readonly DateTime UpdatedDatePassive = DateTime.UtcNow;

        private static readonly bool? DefaultApprovalStatus = false;
        private static readonly bool? UpdatedApprovalStatus = true;

        private static readonly UNKNOWN_TYPE DefaultProcessID = ;
        private static readonly UNKNOWN_TYPE UpdatedProcessID = ;

        private readonly AppWebApplicationFactory<TestStartup> _factory;
        private readonly HttpClient _client;
        private readonly ICurrencyRepository _currencyRepository;

        private Currency _currency;

        private readonly IMapper _mapper;

        private Currency CreateEntity()
        {
            return new Currency
            {
                Code = DefaultCode,
                Name = DefaultName,
                Number = DefaultNumber,
                IsPassive = DefaultIsPassive,
                DatePassive = DefaultDatePassive,
                ApprovalStatus = DefaultApprovalStatus,
                ProcessID = DefaultProcessID,
            };
        }

        private void InitTest()
        {
            _currency = CreateEntity();
        }

        [Fact]
        public async Task CreateCurrency()
        {
            var databaseSizeBeforeCreate = await _currencyRepository.CountAsync();

            // Create the Currency
            CurrencyDto _currencyDto = _mapper.Map<CurrencyDto>(_currency);
            var response = await _client.PostAsync("/api/currencies", TestUtil.ToJsonContent(_currencyDto));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the Currency in the database
            var currencyList = await _currencyRepository.GetAllAsync();
            currencyList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testCurrency = currencyList.Last();
            testCurrency.Code.Should().Be(DefaultCode);
            testCurrency.Name.Should().Be(DefaultName);
            testCurrency.Number.Should().Be(DefaultNumber);
            testCurrency.IsPassive.Should().Be(DefaultIsPassive);
            testCurrency.DatePassive.Should().Be(DefaultDatePassive);
            testCurrency.ApprovalStatus.Should().Be(DefaultApprovalStatus);
            testCurrency.ProcessID.Should().Be(DefaultProcessID);
        }

        [Fact]
        public async Task CreateCurrencyWithExistingId()
        {
            var databaseSizeBeforeCreate = await _currencyRepository.CountAsync();
            // Create the Currency with an existing ID
            _currency.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            CurrencyDto _currencyDto = _mapper.Map<CurrencyDto>(_currency);
            var response = await _client.PostAsync("/api/currencies", TestUtil.ToJsonContent(_currencyDto));

            // Validate the Currency in the database
            var currencyList = await _currencyRepository.GetAllAsync();
            currencyList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllCurrencies()
        {
            // Initialize the database
            await _currencyRepository.CreateOrUpdateAsync(_currency);
            await _currencyRepository.SaveChangesAsync();

            // Get all the currencyList
            var response = await _client.GetAsync("/api/currencies?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_currency.Id);
            json.SelectTokens("$.[*].code").Should().Contain(DefaultCode);
            json.SelectTokens("$.[*].name").Should().Contain(DefaultName);
            json.SelectTokens("$.[*].number").Should().Contain(DefaultNumber);
            json.SelectTokens("$.[*].isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.[*].datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.[*].approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.[*].processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetCurrency()
        {
            // Initialize the database
            await _currencyRepository.CreateOrUpdateAsync(_currency);
            await _currencyRepository.SaveChangesAsync();

            // Get the currency
            var response = await _client.GetAsync($"/api/currencies/{_currency.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_currency.Id);
            json.SelectTokens("$.code").Should().Contain(DefaultCode);
            json.SelectTokens("$.name").Should().Contain(DefaultName);
            json.SelectTokens("$.number").Should().Contain(DefaultNumber);
            json.SelectTokens("$.isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetNonExistingCurrency()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/currencies/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCurrency()
        {
            // Initialize the database
            await _currencyRepository.CreateOrUpdateAsync(_currency);
            await _currencyRepository.SaveChangesAsync();
            var databaseSizeBeforeUpdate = await _currencyRepository.CountAsync();

            // Update the currency
            var updatedCurrency = await _currencyRepository.QueryHelper().GetOneAsync(it => it.Id == _currency.Id);
            // Disconnect from session so that the updates on updatedCurrency are not directly saved in db
            //TODO detach
            updatedCurrency.Code = UpdatedCode;
            updatedCurrency.Name = UpdatedName;
            updatedCurrency.Number = UpdatedNumber;
            updatedCurrency.IsPassive = UpdatedIsPassive;
            updatedCurrency.DatePassive = UpdatedDatePassive;
            updatedCurrency.ApprovalStatus = UpdatedApprovalStatus;
            updatedCurrency.ProcessID = UpdatedProcessID;

            CurrencyDto updatedCurrencyDto = _mapper.Map<CurrencyDto>(updatedCurrency);
            var response = await _client.PutAsync($"/api/currencies/{_currency.Id}", TestUtil.ToJsonContent(updatedCurrencyDto));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the Currency in the database
            var currencyList = await _currencyRepository.GetAllAsync();
            currencyList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testCurrency = currencyList.Last();
            testCurrency.Code.Should().Be(UpdatedCode);
            testCurrency.Name.Should().Be(UpdatedName);
            testCurrency.Number.Should().Be(UpdatedNumber);
            testCurrency.IsPassive.Should().Be(UpdatedIsPassive);
            testCurrency.DatePassive.Should().BeCloseTo(UpdatedDatePassive, 1.Milliseconds());
            testCurrency.ApprovalStatus.Should().Be(UpdatedApprovalStatus);
            testCurrency.ProcessID.Should().Be(UpdatedProcessID);
        }

        [Fact]
        public async Task UpdateNonExistingCurrency()
        {
            var databaseSizeBeforeUpdate = await _currencyRepository.CountAsync();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            CurrencyDto _currencyDto = _mapper.Map<CurrencyDto>(_currency);
            var response = await _client.PutAsync("/api/currencies/1", TestUtil.ToJsonContent(_currencyDto));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the Currency in the database
            var currencyList = await _currencyRepository.GetAllAsync();
            currencyList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteCurrency()
        {
            // Initialize the database
            await _currencyRepository.CreateOrUpdateAsync(_currency);
            await _currencyRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _currencyRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/currencies/{_currency.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var currencyList = await _currencyRepository.GetAllAsync();
            currencyList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(Currency));
            var currency1 = new Currency
            {
                Id = 1L
            };
            var currency2 = new Currency
            {
                Id = currency1.Id
            };
            currency1.Should().Be(currency2);
            currency2.Id = 2L;
            currency1.Should().NotBe(currency2);
            currency1.Id = 0;
            currency1.Should().NotBe(currency2);
        }
    }
}
