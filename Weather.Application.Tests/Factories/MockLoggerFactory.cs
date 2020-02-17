using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace Weather.Application.Tests.Factories
{
    public static class MockLoggerFactory
    {
        public static Mock<ILogger> CreateMockLogger()
        {
            var logger = new Mock<ILogger>();
            logger.Setup(c => c.LogCritical(It.IsAny<string>())).Verifiable();
            logger.Setup(c => c.LogDebug(It.IsAny<string>())).Verifiable();
            logger.Setup(c => c.LogError(It.IsAny<string>())).Verifiable();

            return logger;
        }
    }
}
