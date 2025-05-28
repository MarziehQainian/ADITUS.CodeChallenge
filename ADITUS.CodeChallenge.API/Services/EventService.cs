using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
  public class EventService : IEventService
  {
    private readonly IList<Event> _events;

    public EventService()
    {
      _events = new List<Event>
    {
        new Event
        {
            Id = Guid.Parse("7c63631c-18d4-4395-9c1e-886554265eb0"),
            Year = 2025,
            Name = "ADITUS Spring Expo",
            StartDate = new DateTime(2025, 3, 25),
            EndDate = new DateTime(2025, 3, 27),
            Type = EventType.OnSite
        },
        new Event
        {
            Id = Guid.Parse("751fd775-2c8e-48e0-955c-2144008e984a"),
            Year = 2025,
            Name = "ADITUS Online Days",
            StartDate = new DateTime(2025, 4, 10),
            EndDate = new DateTime(2025, 4, 12),
            Type = EventType.Online
        },
        new Event
        {
            Id = Guid.Parse("974098e0-9b3f-41d5-80c2-551600ad204a"),
            Year = 2025,
            Name = "ADITUS Hybrid Meetup",
            StartDate = new DateTime(2025, 4, 22),
            EndDate = new DateTime(2025, 4, 24),
            Type = EventType.Hybrid
        },
        new Event
        {
            Id = Guid.Parse("28669572-2b9a-4b2c-ad7e-6434ea8ab761"),
            Year = 2025,
            Name = "ADITUS Remote Connect",
            StartDate = new DateTime(2025, 5, 6),
            EndDate = new DateTime(2025, 5, 7),
            Type = EventType.Online
        },
        new Event
        {
            Id = Guid.Parse("3a17b294-8716-448c-94db-ebf9bf53f1ce"),
            Year = 2025,
            Name = "ADITUS Smart Access Day",
            StartDate = new DateTime(2025, 5, 20),
            EndDate = new DateTime(2025, 5, 26),
            Type = EventType.Hybrid
        },
        new Event
        {
            Id = Guid.Parse("d93c3e35-7b1d-4d70-9434-c23a99b298fa"),
            Year = 2025,
            Name = "ADITUS Future Tech",
            StartDate = new DateTime(2025, 6, 10),
            EndDate = new DateTime(2025, 6, 14),
            Type = EventType.OnSite
        },
        new Event
        {
            Id = Guid.Parse("e4a5b75a-3145-43e8-88b6-4a1d3a170afa"),
            Year = 2025,
            Name = "ADITUS Digital Experience",
            StartDate = new DateTime(2025, 7, 5),
            EndDate = new DateTime(2025, 7, 7),
            Type = EventType.Online
        },
        new Event
        {
            Id = Guid.Parse("f9f47b66-cdf1-45fc-9f66-94d9b87994e6"),
            Year = 2025,
            Name = "ADITUS Global Summit",
            StartDate = new DateTime(2025, 9, 10),
            EndDate = new DateTime(2025, 9, 14),
            Type = EventType.Hybrid
        }
    };
    }

    public Task<Event> GetEvent(Guid id)
    {
      var @event = _events.FirstOrDefault(e => e.Id == id);
      return Task.FromResult(@event);
    }

    public Task<IList<Event>> GetEvents()
    {
      return Task.FromResult(_events);
    }
  }
}