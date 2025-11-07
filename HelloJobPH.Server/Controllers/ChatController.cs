using HelloJobPH.Server.ChatSystemHub;
using HelloJobPH.Server.Service.Chat;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<Chathub> _hubContext;
        private readonly IChatService _chatService;
        public ChatController(IHubContext<Chathub> hubContext, IChatService chatService)
        {
            _hubContext= hubContext;
            _chatService= chatService;
        }
        [HttpPost("send")]
        public async Task<ActionResult<ChatMessageDtos>> Send([FromBody] ChatMessageDtos chat)
        {
            var saved = await _chatService.SaveChatAsync(chat);

           // await _hubContext.Clients.User(chat.ReceiverId!)
                //.SendAsync("ReceiveMessage",);

            return Ok(saved);
        }
        [HttpGet("history")]
        public async Task<ActionResult<List<ChatMessageDtos>>> GetHistory([FromQuery] string user1, [FromQuery] string user2)
        {
            var chats = await _chatService.GetChatHistoryAsync(user1, user2);
            return Ok(chats);
        }
    }
}
