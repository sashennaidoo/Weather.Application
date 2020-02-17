using System;
using System.Collections.Generic;
using System.Linq;

namespace Weather.Application.Domain.Contracts.Repository
{
    public interface IReadRepository<T>
    {
        T Get(int id);
        IList<T> GetAll();
        public IQueryable<T> Query();
    }
}
