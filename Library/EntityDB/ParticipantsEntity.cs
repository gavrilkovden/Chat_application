using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityDB
{
    public class ParticipantsEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public bool IsAdmin { get; set; } = false;

        // Navigation property for chat messages
        public ICollection<MessageEntity> Messages { get; set; }

        // Navigation property for user communication
        public UserEntity User { get; set; }

        // Navigation property for communication with the chat
        public ChatEntity Chat { get; set; }
    }
}
