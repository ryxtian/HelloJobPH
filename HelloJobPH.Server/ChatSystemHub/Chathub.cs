using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace HelloJobPH.Server.ChatSystemHub
{
    public class Chathub : Hub
    {
        private readonly ApplicationDbContext _context;
        public Chathub(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task SendMessage(ChatMessageDtos message)
        {
           await Clients.All.SendAsync("ReceiveMessage",message);
        }
    }
}
