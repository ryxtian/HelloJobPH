using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Chat
{
    public class ChatService : IChatService
    {
        private readonly ApplicationDbContext _context;
        public ChatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ChatMessageDtos> SaveChatAsync(ChatMessageDtos chat)
        {
            var entity = new ChatMessage
            {
                SenderId = chat.SenderId,
                ReceiverId = chat.ReceiverId,
                CompanyName = chat.CompanyName,
                Message = chat.Message,
                SentAt = chat.SentAt,
                IsRead = chat.IsRead
            };

            _context.ChatMessages.Add(entity);
            await _context.SaveChangesAsync();

            chat.Id = entity.Id;
            return chat;
        }
        public async Task<List<ChatMessageDtos>> GetChatHistoryAsync(string user1, string user2)
        {
            return await _context.ChatMessages
                .Where(x => (x.SenderId == user1 && x.ReceiverId == user2) ||
                            (x.SenderId == user2 && x.ReceiverId == user1))
                .OrderBy(x => x.SentAt)
                .Select(x => new ChatMessageDtos
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    ReceiverId = x.ReceiverId,
                    CompanyName = x.CompanyName,
                    Message = x.Message,
                    //SentAt = x.SentAt,
                    IsRead = x.IsRead
                }).ToListAsync();
        }
    }
}
