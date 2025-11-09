using HelloJobPH.Server.Data;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.SignalR;


namespace HelloJobPH.Server.Service.ChatService
{
    public class ChatHub : Hub
    {
        private readonly ApplicationDbContext _context;

        public ChatHub(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SendMessage(string senderId, string receiverId, string message)
        {
            var chatMessage = new ChatMessage
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Message = message,
                SentAt = DateTime.UtcNow
            };
            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            await Clients.Users(senderId, receiverId)
                .SendAsync("ReceiveMessage", senderId, receiverId, message, chatMessage.SentAt);
        }

    }
}
