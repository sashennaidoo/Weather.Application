using System;
using Autofac;
using Microsoft.Extensions.Logging;
using Weather.Application.ConsoleApp.ContainerRegistration;
using Weather.Application.ConsoleApp.Managers;
using Weather.Application.Domain.Contracts;
using Weather.Application.Domain.Contracts.Builders;
using Weather.Application.Domain.Contracts.Entities;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Contracts.Http;
using Weather.Application.Domain.Contracts.Repository;
using Weather.Application.Domain.Dto;
using Weather.Application.Domain.Enums;
using Weather.Application.Domain.Logging;
using Weather.Application.Domain.Services;
using Weather.Application.Service.Builders;
using Weather.Application.Service.Factories;
using Weather.Application.Service.Repositories;

namespace Weather.Application.ConsoleApp
{
    public static class ContainerConfiiguration
    {
        public static IContainer BuildContainer()
        {
            var containerBuilder = new ContainerBuilder();
            var config = new Configuration();
            // Register WeatherDetail RestClient
            containerBuilder.RegisterType<RestClient<WeatherDetails>>()
                            .As<IRestClient<WeatherDetails>>()
                            .WithParameters(new NamedParameter[] {
                            new NamedParameter("baseUrl", config.ApiUrl), new NamedParameter("apiKey", config.ApiKey) });

            // We will be using Microsofts logging extension
            containerBuilder.Register((c,p) => new LogFactory()
                                                .CreateLogger(Domain.Enums.LoggerType.Console, config.LogLevel, "Weather", null))
                                                .As<ILogger>();

            //City Repository
            containerBuilder.RegisterType<CityRepository>().As<IReadonlyRepository<City>>().UsingConstructor(new DefaultConstructorSelector());
            // Weather Builder services
            containerBuilder.RegisterType<WeatherBuilderFactory>().As<IWeatherBuilderFactory>();
            
            containerBuilder.RegisterType<RequestFactory>().As<IRequestFactory>();
            containerBuilder.RegisterType<WeatherApplication>().As<IApplication>().SingleInstance();

            return containerBuilder.Build();
        }

    }
}
