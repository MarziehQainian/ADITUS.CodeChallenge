using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using ADITUS.CodeChallenge.API.Domain;

namespace ADITUS.CodeChallenge.API.Services
{
    public interface IEventStatisticsService
    {
        Task<EventStatistics> GetOnlineStatisticsAsync(Guid eventId);
        Task<EventStatistics> GetOnsiteStatisticsAsync(Guid eventId);
    }

    public class EventStatisticsService : IEventStatisticsService
    {
        private readonly HttpClient m_HttpClient;
        public EventStatisticsService(HttpClient httpClient)
        {
            m_HttpClient = httpClient;
        }

        public async Task<EventStatistics> GetOnlineStatisticsAsync(Guid eventId)
        {
            var url = $"https://codechallenge-statistics.azurewebsites.net/api/online-statistics/{eventId}";
            return await FetchStatisticsAsync(url);
        }

        public async Task<EventStatistics> GetOnsiteStatisticsAsync(Guid eventId)
        {
            var url = $"https://codechallenge-statistics.azurewebsites.net/api/onsite-statistics/{eventId}";
            return await FetchStatisticsAsync(url);
        }

        private async Task<EventStatistics> FetchStatisticsAsync(string url)
        {
            try
            {
                var response = await m_HttpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var json = await response.Content.ReadAsStringAsync();
                return System.Text.Json.JsonSerializer.Deserialize<EventStatistics>(json, new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch
            {
                return null;
            }
        }
    }
}
