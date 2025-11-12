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
        [HttpGet("history/{user1}/{user2}")]
        public async Task<IActionResult> GetHistory(string user1, string user2)
        {
            var messages = await _context.ChatMessages
                .Where(m => (m.SenderId == user1 && m.ReceiverId == user2) ||
                            (m.SenderId == user2 && m.ReceiverId == user1))
                .OrderBy(m => m.SentAt)
                .ToListAsync();

            return Ok(messages);
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessageDtos dto)
        {
            var userId = Utility.Utilities.GetUserId();

            var hrId = await _context.HumanResource
                .FirstOrDefaultAsync(i=>i.UserAccountId == userId);


            if (string.IsNullOrWhiteSpace(dto.Message))
                return BadRequest("Message body cannot be empty.");

            // Validate sender and receiver IDs
            if (dto.SenderId == null || dto.ReceiverId == null)
                return BadRequest("Invalid sender or receiver ID.");

            // Map DTO to Entity
            var message = new ChatMessage
            {
                SenderId = hrId.HumanResourceId.ToString(),
                ReceiverId = dto.ReceiverId,
                Message = dto.Message,
                SentAt = DateTime.UtcNow
            };

            //_context.ChatMessages.Add(message);
            //await _context.SaveChangesAsync();

            return Ok(new { Message = "Message sent successfully!", MessageId = message.Id });
        }

    }

}
