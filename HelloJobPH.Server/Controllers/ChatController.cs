using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ChatController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("history/{receiverId}/{senderId}")]
        public async Task<IActionResult> GetHistory(string receiverId, string senderId)
        {
            var messages = await _context.ChatMessages
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.SenderId == receiverId && m.ReceiverId == senderId))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Ok(messages);
        }


        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDtos dto)
        {

            // Map DTO to Entity
            var message = new ChatMessage
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Message = dto.Message,
                SentAt = DateTime.UtcNow
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Message sent successfully!", MessageId = message.Id });
        }

    }

}
