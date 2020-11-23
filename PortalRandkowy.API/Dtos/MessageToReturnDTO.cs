using PortalRandkowy.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalRandkowy.API.Dtos
{
    public class MessageToReturnDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderUserName { get; set; }

        public string SenderPhotoUrl { get; set; }
        public int RecipientId { get; set; }
        public string RecipientUserName { get; set; }
        public string RecipientPhotoUrl { get; set; }
        public string Content { get; set; }
        public bool Isread { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime DateSend { get; set; }
        public bool SenderDelete { get; set; }
        public bool RecipientDelete { get; set; }
    }
}
