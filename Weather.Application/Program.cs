using System;
using Autofac;
using Weather.Application.ConsoleApp;
using Weather.Application.Domain.Contracts;
using Weather.Application.Domain.Logging;

namespace Weather.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            // Build the DI container - I chose autofac because of a lambda resolution
            // feature that allows us to select the right builder for the job
            var container = ContainerConfiiguration.BuildContainer();
            var app = container.Resolve<IApplication>();

            app.Run();
        }

    }
}
