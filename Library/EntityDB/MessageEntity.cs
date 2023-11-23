using DataAccessLayer.Repository.generic;

namespace DataAccessLayer.EntityDB
{
    public class MessageEntity : BaseEntity
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
        public int ParticipantId { get; set; }

        //Navigation property for participant
        public ParticipantsEntity Participant { get; set; }
    }
}
