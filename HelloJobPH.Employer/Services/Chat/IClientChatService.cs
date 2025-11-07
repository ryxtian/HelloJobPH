using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.Chat
{
    public interface IClientChatService
    {
        Task<ChatMessageDtos> ChatMessage(ChatMessageDtos chat);
    }
}
