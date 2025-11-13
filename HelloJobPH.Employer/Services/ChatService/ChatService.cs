using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.ChatService
{
    public class ChatService
    {
        private readonly HttpClient _http;
        private HubConnection? _connection;
        public event Action<string, string>? OnMessageReceived;

        public ChatService(HttpClient http)
        {
            _http = http;
        }

        public async Task InitializeAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7172/chathub") // HQ.Api URL
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (senderId, message) =>
            {
                OnMessageReceived?.Invoke(senderId, message);
            });

            await _connection.StartAsync();
        }
        public async Task<List<ChatMessageDtos>> ChatHistory(string receiverId, string senderId)
        {
            var response = await _http.GetFromJsonAsync<List<ChatMessageDtos>>(
                $"api/chat/history/{senderId}/{receiverId}");
            return response ?? new List<ChatMessageDtos>();
        }


        public async Task SendMessage(string receiverId, string message, string senderId)
        {
            var chatMessage = new ChatMessageDtos
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.UtcNow
            };

            // Optional — persist the message to API
            await _http.PostAsJsonAsync("api/chat/send", chatMessage);

            // Then broadcast via SignalR
            if (_connection?.State == HubConnectionState.Connected)
                await _connection.InvokeAsync("SendMessage", senderId, message);
        }
    }
}
