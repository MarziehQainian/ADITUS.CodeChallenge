using System.ComponentModel.DataAnnotations;

namespace ADITUS.CodeChallenge.API.Domain
{
    public class HardwareReservationRequest
    {
        [Required]
        public Guid EventId { get; set; }
        [Required]
        public Dictionary<HardwareComponent, int> Components { get; set; }
    }
}