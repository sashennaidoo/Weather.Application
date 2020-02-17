using System;
using System.Collections.Generic;
using System.Linq;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Repository;

namespace Weather.Application.Service.Repositories
{
    public class CityRepository : IReadonlyRepository<City>
    {
        private List<City> _cityEntities = new List<City>
        {
            new City { Code = 1, Name = "Cape Town" }
        };

        public CityRepository()
        {
            // Autofac registration
        }

        public CityRepository(IList<City> cities = null)
        {
            _cityEntities = cities?.ToList() ?? _cityEntities;
        }

        public City Get(int id)
        {
            return _cityEntities.FirstOrDefault(cities => cities.Code == id);
        }

        public IList<City> GetAll()
        {
            return _cityEntities;
        }

        public IQueryable<City> Query()
        {
            return _cityEntities.AsQueryable();
        }
    }
}
