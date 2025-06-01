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
    public class HardwareReservationControllerTests
    {
        [Fact]
        public async Task ReserveHardware_ReturnsNotFound_WhenEventDoesNotExist()
        {
            var mockEventService = new Mock<IEventService>();
            var mockHardwareService = new Mock<IHardwareReservationService>();
            mockEventService.Setup(s => s.GetEvent(It.IsAny<Guid>())).ReturnsAsync((Event?)null);
            var controller = new HardwareReservationController(mockEventService.Object, mockHardwareService.Object);
            var request = new HardwareReservationRequest { EventId = Guid.NewGuid() };

            var result = await controller.ReserveHardware(request);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetHardwareReservation_ReturnsNotFound_WhenReservationDoesNotExist()
        {
            var mockEventService = new Mock<IEventService>();
            var mockHardwareService = new Mock<IHardwareReservationService>();
            mockHardwareService.Setup(s => s.GetReservationAsync(It.IsAny<Guid>())).ReturnsAsync((HardwareReservation?)null);
            var controller = new HardwareReservationController(mockEventService.Object, mockHardwareService.Object);

            var result = await controller.GetHardwareReservation(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ReserveHardware_ReturnsOk_WhenReservationIsSuccessful()
        {
            // Arrange
            var mockEventService = new Mock<IEventService>();
            var mockHardwareService = new Mock<IHardwareReservationService>();
            var eventId = Guid.NewGuid();
            var eventStartDate = DateTime.UtcNow.AddDays(30);
            var @event = new Event
            {
                Id = eventId,
                StartDate = eventStartDate
            };
            var request = new HardwareReservationRequest
            {
                EventId = eventId,
                Components = new Dictionary<HardwareComponent, int>
                {
                    { HardwareComponent.Turnstile, 2 },
                    { HardwareComponent.MobileScanningTerminal, 1 }
                }
            };
            var expectedReservation = new HardwareReservation
            {
                EventId = eventId,
                ReservedComponents = new Dictionary<HardwareComponent, int>(request.Components),
                Status = ReservationStatus.PendingApproval
            };
            mockEventService.Setup(s => s.GetEvent(eventId)).ReturnsAsync(@event);
            mockHardwareService.Setup(s => s.ReserveHardwareAsync(request, eventStartDate)).ReturnsAsync(expectedReservation);
            var controller = new HardwareReservationController(mockEventService.Object, mockHardwareService.Object);

            // Act
            var result = await controller.ReserveHardware(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var reservation = Assert.IsType<HardwareReservation>(okResult.Value);
            Assert.Equal(expectedReservation.EventId, reservation.EventId);
            Assert.Equal(expectedReservation.ReservedComponents, reservation.ReservedComponents);
            Assert.Equal(expectedReservation.Status, reservation.Status);
        }
    }
}
