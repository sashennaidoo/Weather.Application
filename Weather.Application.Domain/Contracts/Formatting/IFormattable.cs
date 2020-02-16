using System;
namespace Weather.Application.Domain.Contracts.Formatting
{
    public interface IFormattable<T>
    {
        T Format();
    }
}
