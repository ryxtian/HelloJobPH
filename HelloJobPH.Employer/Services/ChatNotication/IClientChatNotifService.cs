using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.ChatNotication
{
    public interface IClientChatNotifService
    {
        Task<List<ChatMessageDtos>> NotificationList(string userId);
    }
}
