using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Weather.Application.Domain.Contracts;
using Weather.Application.Domain.Contracts.Factories;
using Weather.Application.Domain.Enums;
using Weather.Application.Domain.Exceptions;

namespace Weather.Application.ConsoleApp
{
    public class WeatherApplication : IApplication
    {
        #region Private Members

        private readonly ILogger _logger;
        private readonly IWeatherBuilderFactory _weatherBuilderFactory;

        // A map the inputs from the console so that we don't have to
        // perform the same set of steps for checking input
        // which would seem very feature envious on struct checking
        private Dictionary<string, List<string>> _choiceMap => new Dictionary<string, List<string>>
        {
            { "Run", new List<string>{ "1", "2" } },
            { "City", new List<string>{ "1", "2"} },
            { "Format", new List<string> { "1", "2", "3"} }
        };

        #endregion Private Members

        public WeatherApplication(ILogger logger, IWeatherBuilderFactory weatherBuilderFactory)
        {
            _logger = logger;
            _weatherBuilderFactory = weatherBuilderFactory;
        }

        public void Run()
        {
            // Run Options Menu
            _logger.LogDebug("Entering run selection menu");

            Console.WriteLine("Welcome to the Weather Service console app. Please select an option from the menu:");
            // Get the selection from the user
            var run = ProcessRunSelection();
            // if 2 then exit
            if (run == "2")
            {
                _logger.LogDebug("Exiting application");
                return;
            }
            // Get the City selection from the user
            if (!Int32.TryParse(ProcessCitySelection(), out int city))
            {
                _logger.LogCritical("Unable to read city selection");
                return;
            }

            if (city == 2)
                return;
            // Get the selection from the console and parse into the Weather Display Type enum
            // In this case, the enum is mapped to the input from the console
            // (1 - Raw) , (2 - Details), (3 - Info)  
            if (!Enum.TryParse(ProcessFormatSelection(), out WeatherDisplayType formatSelection))
            {
                _logger.LogCritical("Unable to read Format selection");
                return;
            }
            try
            {
                // Go to the Builder Factory and create an instance of a builder as per the
                // WeatherDisplayType enum and process to a string
                var formatted = _weatherBuilderFactory.Create(formatSelection).BuildAndFormat(city);
                Console.WriteLine(formatted);
            }
            catch (WeatherBuilderException ex)
            {
                Console.WriteLine($"Error occured during building of Weather information {ex} ");
            }
            catch (WeatherFormatException wex)
            {
                Console.WriteLine($"Error occured during formatting of Weather information {wex} ");
            }
            finally
            {
                // For Easier testing of the application
                Console.WriteLine("Would you like to repeat the process");
                Console.WriteLine("1. Yes");
                Console.WriteLine("Any other key - Exit");
                var repeat = Console.ReadLine();
                if (repeat == "1")
                    Run();
            }
            
        }

        #region Processors

        private string ProcessRunSelection()
        {
            ShowRunOptions();
            var runChoice = Console.ReadLine();
            var options = _choiceMap["Run"];

            _logger.LogDebug($"Run Option Selected {runChoice}");

            while (!StateProcessed("Run", runChoice, options))
            {
                ShowInvalidSelectionMessage();
                ShowRunOptions();
                runChoice = Console.ReadLine();
                _logger.LogDebug($"Run Option Selected {runChoice}");
            }

            return runChoice;
        }


        private string ProcessCitySelection()
        {
            var options = _choiceMap["City"];
            Console.WriteLine("We currently only support Cape Town's weather");
            ShowCityOptions();

            var city = Console.ReadLine();
            _logger.LogDebug($"City Option Selected {city}");
            while (!StateProcessed("City", city, options))
            {
                ShowInvalidSelectionMessage();
                ShowCityOptions();
                city = Console.ReadLine();
                _logger.LogDebug($"City Option Selected {city}");
            }

            return city;
        }

        private string ProcessFormatSelection()
        {
            var options = _choiceMap["Format"];
            Console.WriteLine("How would you like to view the data?");
            ShowFormatOptions();

            var format = Console.ReadLine();
            _logger.LogDebug($"Format Option Selected {format}");
            while (!StateProcessed("Format", format, options))
            {
                ShowInvalidSelectionMessage();
                ShowFormatOptions();
                format = Console.ReadLine();
                _logger.LogDebug($"City Option Selected {format}");
            }

            return format;
        }

        public bool StateProcessed(string action, string selection, List<string> options)
        {
            return (options.Contains(selection));
        }


        #endregion Processors

        #region Options Display

        private void ShowRunOptions()
        {
            Console.WriteLine();
            Console.WriteLine("1. Choose City");
            Console.WriteLine("2. Exit");
        }

        private void ShowCityOptions()
        {
            Console.WriteLine();
            Console.WriteLine("1. Get Cape Town's current weather");
            Console.WriteLine("2. Exit");
        }

        private void ShowFormatOptions()
        {
            Console.WriteLine();
            Console.WriteLine("1. Raw JSON");
            Console.WriteLine("2. Formatted");
            Console.WriteLine("3. Nicely formatted and only displaying data interesting to the public");
        }

        private void ShowInvalidSelectionMessage()
        {
            Console.WriteLine("Invalid Selection");
        }
        #endregion Display Options
    }
}
