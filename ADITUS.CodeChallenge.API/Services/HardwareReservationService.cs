using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public class HardwareReservationService : IHardwareReservationService
  {
    private readonly Dictionary<HardwareComponent, int> m_Stock = new()
        {
            { HardwareComponent.Turnstile, 10 },
            { HardwareComponent.WirelessHandheldScanner, 20 },
            { HardwareComponent.MobileScanningTerminal, 15 }
        };

    private readonly Dictionary<Guid, HardwareReservation> m_Reservations = new();

    public Task<HardwareReservation> ReserveHardwareAsync(HardwareReservationRequest request, DateTime? eventStartDate)
    {
      if (eventStartDate == null || eventStartDate.Value < DateTime.UtcNow.AddDays(28))
        return Task.FromResult<HardwareReservation>(null);

      
      if (m_Reservations.TryGetValue(request.EventId, out var existingReservation))
      {
        foreach (var kv in request.Components)
        {
          var component = kv.Key;
          var newQty = kv.Value;
        

          if (existingReservation.ReservedComponents.TryGetValue(component, out var alreadyReservedQty))
          {
            

            if (!m_Stock.TryGetValue(component, out var available) || kv.Value > available)
              return Task.FromResult<HardwareReservation>(null); 
          }
          else
          {
            if (!m_Stock.TryGetValue(component, out var available) || newQty > available)
              return Task.FromResult<HardwareReservation>(null);
          }
        }

      
        foreach (var kv in request.Components)
        {
          if (existingReservation.ReservedComponents.ContainsKey(kv.Key))
            existingReservation.ReservedComponents[kv.Key] += kv.Value;
          else
            existingReservation.ReservedComponents[kv.Key] = kv.Value;

          m_Stock[kv.Key] -= kv.Value;
        }

        return Task.FromResult(existingReservation); 
      }

    
      foreach (var kv in request.Components)
      {
        if (!m_Stock.TryGetValue(kv.Key, out var stock) || kv.Value > stock)
          return Task.FromResult<HardwareReservation>(null);
      }

      foreach (var kv in request.Components)
      {
        m_Stock[kv.Key] -= kv.Value;
      }

      var reservation = new HardwareReservation
      {
        EventId = request.EventId,
        ReservedComponents = new Dictionary<HardwareComponent, int>(request.Components),
        Status = ReservationStatus.PendingApproval
      };

      m_Reservations[request.EventId] = reservation;
      return Task.FromResult(reservation);
    }


    public Task<HardwareReservation> GetReservationAsync(Guid eventId)
    {
      m_Reservations.TryGetValue(eventId, out var reservation);
      return Task.FromResult(reservation);
    }
  }
}