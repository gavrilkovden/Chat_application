using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityDB
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int ParticipantId { get; set; }

        //Navigation property for participant
        public ParticipantsEntity Participant { get; set; }
    }
}
