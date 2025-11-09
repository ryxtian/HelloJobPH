using Microsoft.AspNetCore.SignalR.Client;

namespace HelloJobPH.Employer.Services.ChatService
{
    public class ChatService
    {
        private HubConnection? _connection;
        public event Action<string, string>? OnMessageReceived;

        public async Task InitializeAsync()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7172/chathub") // HQ.Api URL
                .WithAutomaticReconnect()
                .Build();

            _connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                OnMessageReceived?.Invoke(user, message);
            });

            await _connection.StartAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            if (_connection?.State == HubConnectionState.Connected)
                await _connection.InvokeAsync("SendMessage", user, message);
        }
    }
}
