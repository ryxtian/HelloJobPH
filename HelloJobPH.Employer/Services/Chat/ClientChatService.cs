using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace HelloJobPH.Employer.Services.Chat
{
    public class ClientChatService : IClientChatService
    {
        private readonly HttpClient _http;
        public ClientChatService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ChatMessageDtos> ChatMessage(ChatMessageDtos chat)
        {
            var response = await _http.PostAsJsonAsync("api/chat/send", chat);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ChatMessageDtos>();
        }
    }
}
