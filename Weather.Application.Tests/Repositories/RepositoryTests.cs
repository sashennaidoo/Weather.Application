using System;
using System.Collections.Generic;
using NUnit.Framework;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Service.Repositories;

namespace Weather.Application.Tests.Repositories
{
    public class RepositoryTests
    {

        private CityRepository _repo;
        [SetUp]
        public void Setup()
        {
            _repo = new CityRepository();
        }

        [Test]
        public void TestCtor_WithListOfCities_ShouldSetListToPassedIn()
        {
            var citiesList = new List<City>
            {
                {  new City { Code = 1, Name = "Cape Town" } }
                , { new City { Code = 2, Name = "Durban" } }
                , { new City { Code = 3, Name = "Johannesburg" } }
            };

            _repo = new CityRepository(citiesList);

            Assert.AreEqual(3, _repo.GetAll().Count);
        }

        [TestCase(1)]
        public void TestGet_WithValidId_ShouldReturnCityObject(int cityId)
        {
            var city = _repo.Get(cityId);
            Assert.IsInstanceOf(typeof(City), city);
        }

        [TestCase(2)]
        public void TestGet_WithNonExistingId_ShouldReturnNullObject(int cityId)
        {
            var city = _repo.Get(cityId);
            Assert.IsNull(city);
        }

        [Test]
        public void TestGetAll_ShouldReturnListOfCityObject()
        {
            var all = _repo.GetAll();
            Assert.IsInstanceOf<IList<City>>(all);
        }
    }
}
