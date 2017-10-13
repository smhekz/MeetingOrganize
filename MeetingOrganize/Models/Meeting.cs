using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MeetingOrganize.Models
{
    public class Meeting
    {
        public int Id { get; set; }
        [Required]
        public string Subject { get; set; }
        public string Date { get; set; }
        public string StartTime { get; set; }
        public string FinishTime { get; set; }
        public int ParticipantId { get; set; }
        public virtual Participant Participant { get; set; }

    }
    
}