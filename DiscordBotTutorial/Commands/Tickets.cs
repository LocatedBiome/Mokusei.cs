using Newtonsoft.Json;
using System;
using System.IO;

namespace DiscordBotTutorial.Commands
{
        class Ticket
    {
        public ulong createdBy = 0;
        public string title = "";
        public string text = "";
        public Ticket (ulong createdBy, string title, string text)
        {
            this.title = title;
            this.text = text;
            this.createdBy = createdBy;
            
            
        }
        public static Ticket ReadTicket(Guid id)
        {
            return JsonConvert.DeserializeObject<Ticket>(File.ReadAllText($"tickets/{id.ToString()}.json"));
        }
        public static void SaveTicket(Ticket ticket, Guid id)
        {
            File.WriteAllText($"tickets/{id.ToString()}.json", JsonConvert.SerializeObject(ticket));
        }
    }
}