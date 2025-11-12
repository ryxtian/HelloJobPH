using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.SignalR.Client;
using System.Net.Http;
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

            _connection.On<string, string>("ReceiveMessage", (receiverId, message) =>
            {
                OnMessageReceived?.Invoke(receiverId, message);
            });

            await _connection.StartAsync();
        }

        public async Task SendMessage(string receiverId, string message)
        {
            var chatMessage = new ChatMessageDtos
            {
                //SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.UtcNow
            };

            var response = await _http.PostAsJsonAsync("api/chat/send", chatMessage);


            if (_connection?.State == HubConnectionState.Connected)
                await _connection.InvokeAsync("SendMessage", receiverId, message);

        }
    }
}
