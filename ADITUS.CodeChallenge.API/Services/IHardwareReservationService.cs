using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
    public interface IHardwareReservationService
    {
        Task<HardwareReservation> ReserveHardwareAsync(HardwareReservationRequest request, DateTime? eventStartDate);
        Task<HardwareReservation> GetReservationAsync(Guid eventId);
    }
}