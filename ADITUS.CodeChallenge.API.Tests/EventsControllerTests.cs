using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ADITUS.CodeChallenge.API.Controllers;
using ADITUS.CodeChallenge.API.Services;
using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Tests
{
    public class EventsControllerTests
    {
        [Fact]
        public async Task GetEvents_ReturnsOkResultWithEvents()
        {
            var mockEventService = new Mock<IEventService>();
            var mockStatsService = new Mock<IEventStatisticsService>();
            var mockHardwareService = new Mock<IHardwareReservationService>();
            mockEventService.Setup(s => s.GetEvents()).ReturnsAsync(new[] { new Event() });
            var controller = new EventsController(mockEventService.Object, mockStatsService.Object, mockHardwareService.Object);

            var result = await controller.GetEvents();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetEvent_ReturnsNotFound_WhenEventDoesNotExist()
        {
            var mockEventService = new Mock<IEventService>();
            var mockStatsService = new Mock<IEventStatisticsService>();
            var mockHardwareService = new Mock<IHardwareReservationService>();
            mockEventService.Setup(s => s.GetEvent(It.IsAny<Guid>())).ReturnsAsync((Event?)null);
            var controller = new EventsController(mockEventService.Object, mockStatsService.Object, mockHardwareService.Object);

            var result = await controller.GetEvent(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
