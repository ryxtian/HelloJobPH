using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace HelloJobPH.Server.ChatSystemHub
{
    public class Chathub : Hub
    {

        //public async Task SendMessage(ChatMessageDtos message)
        //{
        //   await Clients.All.SendAsync("ReceiveMessage",message);
        //}
        public async Task SendMessage(string sender, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", sender, message);
        }
    }
}
