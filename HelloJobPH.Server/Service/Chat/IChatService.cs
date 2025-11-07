using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.Chat
{
    public interface IChatService
    {

            Task<ChatMessageDtos> SaveChatAsync(ChatMessageDtos chat);
            Task<List<ChatMessageDtos>> GetChatHistoryAsync(string user1, string user2);
        
    }
}
