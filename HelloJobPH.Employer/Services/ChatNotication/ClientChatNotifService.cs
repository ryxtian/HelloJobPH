using HelloJobPH.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.ChatNotication
{
    public class ClientChatNotifService : IClientChatNotifService
    {
        private readonly HttpClient _httpClient;
        public ClientChatNotifService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<ChatMessageDtos>> NotificationList(string userId)
        {
            var notifications = await _httpClient.GetFromJsonAsync<List<ChatMessageDtos>>($"api/chat/notifications/{userId}");
            return notifications ?? new List<ChatMessageDtos>();
        }
    }
}
