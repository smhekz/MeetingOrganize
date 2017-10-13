namespace MeetingOrganize.Migrations
{

    using MeetingOrganize.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MeetingOrganize.Models.MeetingOrganizeContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
        protected override void Seed(MeetingOrganize.Models.MeetingOrganizeContext context)
        {
            context.Participants.AddOrUpdate(x => x.Id,
                new Participant() { Id = 1, Name = "Semih Bey" },
                new Participant() { Id = 2, Name = "Caner Bey" },
                new Participant() { Id = 3, Name = "Elif Haným" },
                new Participant() { Id = 3, Name = "Sinan Bey" }
                );

            context.Meetings.AddOrUpdate(x => x.Id,
                new Meeting()
                {
                    Id = 1,
                    Subject = "Mevzuat",
                    Date = "12.10.2012",
                    StartTime = "12.30",
                    FinishTime = "13.30",
                    ParticipantId = 1
                },
                new Meeting()
                {
                    Id = 2,
                    Subject = "Tüzel Müþteri",
                    Date = "10.11.2015",
                    StartTime = "11.00",
                    FinishTime = "12.00",
                    ParticipantId = 2
                },
                new Meeting()
                {
                    Id = 3,
                    Subject = "Code refactor",
                    Date = "10.10.2010",
                    StartTime = "20.00",
                    FinishTime = "21.00",
                    ParticipantId = 2
                }
                
                );
        }
        
    }
}
