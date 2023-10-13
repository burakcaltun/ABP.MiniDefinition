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
    public class CitiesControllerIntTest
    {
        public CitiesControllerIntTest()
        {
            _factory = new AppWebApplicationFactory<TestStartup>().WithMockUser();
            _client = _factory.CreateClient();

            _cityRepository = _factory.GetRequiredService<ICityRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            _mapper = config.CreateMapper();

            InitTest();
        }

        private const string DefaultCityCode = "AAAAAAAAAA";
        private const string UpdatedCityCode = "BBBBBBBBBB";

        private const string DefaultCityName = "AAAAAAAAAA";
        private const string UpdatedCityName = "BBBBBBBBBB";

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
        private readonly ICityRepository _cityRepository;

        private City _city;

        private readonly IMapper _mapper;

        private City CreateEntity()
        {
            return new City
            {
                CityCode = DefaultCityCode,
                CityName = DefaultCityName,
                IsPassive = DefaultIsPassive,
                DatePassive = DefaultDatePassive,
                ApprovalStatus = DefaultApprovalStatus,
                ProcessID = DefaultProcessID,
            };
        }

        private void InitTest()
        {
            _city = CreateEntity();
        }

        [Fact]
        public async Task CreateCity()
        {
            var databaseSizeBeforeCreate = await _cityRepository.CountAsync();

            // Create the City
            CityDto _cityDto = _mapper.Map<CityDto>(_city);
            var response = await _client.PostAsync("/api/cities", TestUtil.ToJsonContent(_cityDto));
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            // Validate the City in the database
            var cityList = await _cityRepository.GetAllAsync();
            cityList.Count().Should().Be(databaseSizeBeforeCreate + 1);
            var testCity = cityList.Last();
            testCity.CityCode.Should().Be(DefaultCityCode);
            testCity.CityName.Should().Be(DefaultCityName);
            testCity.IsPassive.Should().Be(DefaultIsPassive);
            testCity.DatePassive.Should().Be(DefaultDatePassive);
            testCity.ApprovalStatus.Should().Be(DefaultApprovalStatus);
            testCity.ProcessID.Should().Be(DefaultProcessID);
        }

        [Fact]
        public async Task CreateCityWithExistingId()
        {
            var databaseSizeBeforeCreate = await _cityRepository.CountAsync();
            // Create the City with an existing ID
            _city.Id = 1L;

            // An entity with an existing ID cannot be created, so this API call must fail
            CityDto _cityDto = _mapper.Map<CityDto>(_city);
            var response = await _client.PostAsync("/api/cities", TestUtil.ToJsonContent(_cityDto));

            // Validate the City in the database
            var cityList = await _cityRepository.GetAllAsync();
            cityList.Count().Should().Be(databaseSizeBeforeCreate);
        }

        [Fact]
        public async Task GetAllCities()
        {
            // Initialize the database
            await _cityRepository.CreateOrUpdateAsync(_city);
            await _cityRepository.SaveChangesAsync();

            // Get all the cityList
            var response = await _client.GetAsync("/api/cities?sort=id,desc");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.[*].id").Should().Contain(_city.Id);
            json.SelectTokens("$.[*].cityCode").Should().Contain(DefaultCityCode);
            json.SelectTokens("$.[*].cityName").Should().Contain(DefaultCityName);
            json.SelectTokens("$.[*].isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.[*].datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.[*].approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.[*].processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetCity()
        {
            // Initialize the database
            await _cityRepository.CreateOrUpdateAsync(_city);
            await _cityRepository.SaveChangesAsync();

            // Get the city
            var response = await _client.GetAsync($"/api/cities/{_city.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var json = JToken.Parse(await response.Content.ReadAsStringAsync());
            json.SelectTokens("$.id").Should().Contain(_city.Id);
            json.SelectTokens("$.cityCode").Should().Contain(DefaultCityCode);
            json.SelectTokens("$.cityName").Should().Contain(DefaultCityName);
            json.SelectTokens("$.isPassive").Should().Contain(DefaultIsPassive);
            json.SelectTokens("$.datePassive").Should().Contain(DefaultDatePassive);
            json.SelectTokens("$.approvalStatus").Should().Contain(DefaultApprovalStatus);
            json.SelectTokens("$.processId").Should().Contain(DefaultProcessID);
        }

        [Fact]
        public async Task GetNonExistingCity()
        {
            var maxValue = long.MaxValue;
            var response = await _client.GetAsync("/api/cities/" + maxValue);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task UpdateCity()
        {
            // Initialize the database
            await _cityRepository.CreateOrUpdateAsync(_city);
            await _cityRepository.SaveChangesAsync();
            var databaseSizeBeforeUpdate = await _cityRepository.CountAsync();

            // Update the city
            var updatedCity = await _cityRepository.QueryHelper().GetOneAsync(it => it.Id == _city.Id);
            // Disconnect from session so that the updates on updatedCity are not directly saved in db
            //TODO detach
            updatedCity.CityCode = UpdatedCityCode;
            updatedCity.CityName = UpdatedCityName;
            updatedCity.IsPassive = UpdatedIsPassive;
            updatedCity.DatePassive = UpdatedDatePassive;
            updatedCity.ApprovalStatus = UpdatedApprovalStatus;
            updatedCity.ProcessID = UpdatedProcessID;

            CityDto updatedCityDto = _mapper.Map<CityDto>(updatedCity);
            var response = await _client.PutAsync($"/api/cities/{_city.Id}", TestUtil.ToJsonContent(updatedCityDto));
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            // Validate the City in the database
            var cityList = await _cityRepository.GetAllAsync();
            cityList.Count().Should().Be(databaseSizeBeforeUpdate);
            var testCity = cityList.Last();
            testCity.CityCode.Should().Be(UpdatedCityCode);
            testCity.CityName.Should().Be(UpdatedCityName);
            testCity.IsPassive.Should().Be(UpdatedIsPassive);
            testCity.DatePassive.Should().BeCloseTo(UpdatedDatePassive, 1.Milliseconds());
            testCity.ApprovalStatus.Should().Be(UpdatedApprovalStatus);
            testCity.ProcessID.Should().Be(UpdatedProcessID);
        }

        [Fact]
        public async Task UpdateNonExistingCity()
        {
            var databaseSizeBeforeUpdate = await _cityRepository.CountAsync();

            // If the entity doesn't have an ID, it will throw BadRequestAlertException
            CityDto _cityDto = _mapper.Map<CityDto>(_city);
            var response = await _client.PutAsync("/api/cities/1", TestUtil.ToJsonContent(_cityDto));
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            // Validate the City in the database
            var cityList = await _cityRepository.GetAllAsync();
            cityList.Count().Should().Be(databaseSizeBeforeUpdate);
        }

        [Fact]
        public async Task DeleteCity()
        {
            // Initialize the database
            await _cityRepository.CreateOrUpdateAsync(_city);
            await _cityRepository.SaveChangesAsync();
            var databaseSizeBeforeDelete = await _cityRepository.CountAsync();

            var response = await _client.DeleteAsync($"/api/cities/{_city.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);

            // Validate the database is empty
            var cityList = await _cityRepository.GetAllAsync();
            cityList.Count().Should().Be(databaseSizeBeforeDelete - 1);
        }

        [Fact]
        public void EqualsVerifier()
        {
            TestUtil.EqualsVerifier(typeof(City));
            var city1 = new City
            {
                Id = 1L
            };
            var city2 = new City
            {
                Id = city1.Id
            };
            city1.Should().Be(city2);
            city2.Id = 2L;
            city1.Should().NotBe(city2);
            city1.Id = 0;
            city1.Should().NotBe(city2);
        }
    }
}
