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
    public class CountriesControllerIntTest
    {
        public CountriesControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _countryRepository = _factory.GetRequiredService<ICountryRepository>();

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

        private static readonly int? DefaultCustomsCode = 1;
        private static readonly int? UpdatedCustomsCode = 2;

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
        private readonly ICountryRepository _countryRepository;

        private Country _country;

        private readonly IMapper _mapper;

        private Country CreateEntity()
        {
            return new Country
            {
                Code = DefaultCode,
                Name = DefaultName,
                CustomsCode = DefaultCustomsCode,
                IsPassive = DefaultIsPassive,
                DatePassive = DefaultDatePassive,
                ApprovalStatus = DefaultApprovalStatus,
                ProcessID = DefaultProcessID,
            };
        }

        private void InitTest()
        {
            _country = CreateEntity();
        }

        [Fact]
        public async Task CreateCountry()
        {
            var databaseSizeBeforeCreate = await _countryRepository.CountAsync();

            // Create the Country
            CountryDto _countryDto = _mapper.Map<CountryDto>(_country);
            var response = await _client.PostAsync("/api/countries", TestUtil.ToJsonContent(_countryDto));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the Country in the database
            var countryList = await _countryRepository.GetAllAsync();
            countryList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testCountry = countryList.Last();
            testCountry.Code.Should().Be(DefaultCode);
            testCountry.Name.Should().Be(DefaultName);
            testCountry.CustomsCode.Should().Be(DefaultCustomsCode);
            testCountry.IsPassive.Should().Be(DefaultIsPassive);
            testCountry.DatePassive.Should().Be(DefaultDatePassive);
            testCountry.ApprovalStatus.Should().Be(DefaultApprovalStatus);
            testCountry.ProcessID.Should().Be(DefaultProcessID);
        }

        [Fact]
        public async Task CreateCountryWithExistingId()
        {
            var databaseSizeBeforeCreate = await _countryRepository.CountAsync();
            // Create the Country with an existing ID
            _country.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            CountryDto _countryDto = _mapper.Map<CountryDto>(_country);
            var response = await _client.PostAsync("/api/countries", TestUtil.ToJsonContent(_countryDto));

            // Validate the Country in the database
            var countryList = await _countryRepository.GetAllAsync();
            countryList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllCountries()
        {
            // Initialize the database
            await _countryRepository.CreateOrUpdateAsync(_country);
            await _countryRepository.SaveChangesAsync();

            // Get all the countryList
            var response = await _client.GetAsync("/api/countries?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_country.Id);
            json.SelectTokens("$.[*].code").Should().Contain(DefaultCode);
            json.SelectTokens("$.[*].name").Should().Contain(DefaultName);
            json.SelectTokens("$.[*].customsCode").Should().Contain(DefaultCustomsCode);
            json.SelectTokens("$.[*].isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.[*].datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.[*].approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.[*].processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetCountry()
        {
            // Initialize the database
            await _countryRepository.CreateOrUpdateAsync(_country);
            await _countryRepository.SaveChangesAsync();

            // Get the country
            var response = await _client.GetAsync($"/api/countries/{_country.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_country.Id);
            json.SelectTokens("$.code").Should().Contain(DefaultCode);
            json.SelectTokens("$.name").Should().Contain(DefaultName);
            json.SelectTokens("$.customsCode").Should().Contain(DefaultCustomsCode);
            json.SelectTokens("$.isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetNonExistingCountry()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/countries/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCountry()
        {
            // Initialize the database
            await _countryRepository.CreateOrUpdateAsync(_country);
            await _countryRepository.SaveChangesAsync();
            var databaseSizeBeforeUpdate = await _countryRepository.CountAsync();

            // Update the country
            var updatedCountry = await _countryRepository.QueryHelper().GetOneAsync(it => it.Id == _country.Id);
            // Disconnect from session so that the updates on updatedCountry are not directly saved in db
            //TODO detach
            updatedCountry.Code = UpdatedCode;
            updatedCountry.Name = UpdatedName;
            updatedCountry.CustomsCode = UpdatedCustomsCode;
            updatedCountry.IsPassive = UpdatedIsPassive;
            updatedCountry.DatePassive = UpdatedDatePassive;
            updatedCountry.ApprovalStatus = UpdatedApprovalStatus;
            updatedCountry.ProcessID = UpdatedProcessID;

            CountryDto updatedCountryDto = _mapper.Map<CountryDto>(updatedCountry);
            var response = await _client.PutAsync($"/api/countries/{_country.Id}", TestUtil.ToJsonContent(updatedCountryDto));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the Country in the database
            var countryList = await _countryRepository.GetAllAsync();
            countryList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testCountry = countryList.Last();
            testCountry.Code.Should().Be(UpdatedCode);
            testCountry.Name.Should().Be(UpdatedName);
            testCountry.CustomsCode.Should().Be(UpdatedCustomsCode);
            testCountry.IsPassive.Should().Be(UpdatedIsPassive);
            testCountry.DatePassive.Should().BeCloseTo(UpdatedDatePassive, 1.Milliseconds());
            testCountry.ApprovalStatus.Should().Be(UpdatedApprovalStatus);
            testCountry.ProcessID.Should().Be(UpdatedProcessID);
        }

        [Fact]
        public async Task UpdateNonExistingCountry()
        {
            var databaseSizeBeforeUpdate = await _countryRepository.CountAsync();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            CountryDto _countryDto = _mapper.Map<CountryDto>(_country);
            var response = await _client.PutAsync("/api/countries/1", TestUtil.ToJsonContent(_countryDto));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the Country in the database
            var countryList = await _countryRepository.GetAllAsync();
            countryList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteCountry()
        {
            // Initialize the database
            await _countryRepository.CreateOrUpdateAsync(_country);
            await _countryRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _countryRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/countries/{_country.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var countryList = await _countryRepository.GetAllAsync();
            countryList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(Country));
            var country1 = new Country
            {
                Id = 1L
            };
            var country2 = new Country
            {
                Id = country1.Id
            };
            country1.Should().Be(country2);
            country2.Id = 2L;
            country1.Should().NotBe(country2);
            country1.Id = 0;
            country1.Should().NotBe(country2);
        }
    }
}
