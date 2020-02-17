using System;
using System.Collections.Generic;
using System.Linq;

namespace Weather.Application.Domain.Contracts.Repository
{
    public interface IReadonlyRepository<T>
    {
        T Get(int id);
        IList<T> GetAll();
        public IQueryable<T> Query();
    }
}
