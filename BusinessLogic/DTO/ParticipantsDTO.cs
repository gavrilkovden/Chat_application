using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO
{
    public class ParticipantsDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
