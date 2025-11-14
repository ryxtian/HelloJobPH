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

            var userId = Utility.Utilities.GetUserId();

            var HR = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);



            // Map DTO to Entity
            var message = new ChatMessage
            {
                SenderId = dto.SenderId,
                ReceiverId = dto.ReceiverId,
                Message = dto.Message,
                SentAt = DateTime.UtcNow,
                HumanResourcesId =HR.HumanResourceId
            };

            _context.ChatMessages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Message sent successfully!", MessageId = message.Id });
        }

        [HttpGet("notifications/{applicantId}")]
        public async Task<IActionResult> NotificationList(string applicantId)
        {
            var notifications = await _context.ChatMessages
                .Where(m => m.ReceiverId == applicantId)
                .GroupBy(m => m.SenderId) // group by sender
                .Select(g => g
                    .OrderByDescending(m => m.SentAt) // latest message first
                    .Select(m => new ChatMessageDtos
                    {
                        Id = m.Id,
                        SenderId = m.SenderId,
                        ReceiverId = m.ReceiverId,
                        Message = m.Message,
                        SentAt = m.SentAt,
                        ApplicantName = m.Applicant.Firstname+" "+m.Applicant.Surname
                    })
                    .FirstOrDefault() // take only one per sender
                )
                .ToListAsync();

            return Ok(notifications);
        }

    }

}
