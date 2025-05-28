namespace ADITUS.CodeChallenge.API.Domain
{
    public enum ReservationStatus
    {
        None,
        PendingApproval,
        Approved,
        Rejected
    }

    public class HardwareReservation
    {
        public Guid EventId { get; set; }
        public Dictionary<HardwareComponent, int> ReservedComponents { get; set; }
        public ReservationStatus Status { get; set; }
    }
}