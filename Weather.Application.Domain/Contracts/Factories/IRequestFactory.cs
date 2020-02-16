using System;
using Weather.Application.Domain.Dto.Request;

namespace Weather.Application.Domain.Contracts.Factories
{
    public interface IRequestFactory
    {
        AbstractRequest CreateRequest(string city);
    }
}
