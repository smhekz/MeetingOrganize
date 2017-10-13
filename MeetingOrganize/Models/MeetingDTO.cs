using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MeetingOrganize.Models
{
    public class MeetingDTO
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        
        //public string Date { get; set; }
        //public string StartTime { get; set; }
        
        //public string FinishTime { get; set; }
        public string ParticipantName { get; set; }

     


    }
}