using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MeetingOrganize.Models;

namespace MeetingOrganize.Controllers
{
    public class MeetingsController : ApiController
    {
        private MeetingOrganizeContext db = new MeetingOrganizeContext();

        // GET: api/Meetings
        public IQueryable<MeetingDTO> GetMeetings()
        {
            var meetings = from b in db.Meetings
                           select new MeetingDTO()
                           {
                               Id = b.Id,
                               Subject = b.Subject,
                               ParticipantName = b.Participant.Name
                           };

            return meetings;
        }

        // GET: api/Books/5
        [ResponseType(typeof(MeetingDetailDTO))]
        public async Task<IHttpActionResult> GetMeeting(int id)
        {
            var meeting = await db.Meetings.Include(b => b.Participant).Select(b =>
                new MeetingDetailDTO()
                {
                    Id = b.Id,
                    Subject = b.Subject,
                    Date = b.Date,
                    StartTime = b.StartTime,
                    FinishTime = b.FinishTime,
                    ParticipantName = b.Participant.Name
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (meeting == null)
            {
                return NotFound();
            }

            return Ok(meeting);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeeting(int id, Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meeting.Id)
            {
                return BadRequest();
            }

            db.Entry(meeting).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(Meeting))]
        public async Task<IHttpActionResult> PostMeeting(Meeting meeting)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            db.Meetings.Add(meeting);
            await db.SaveChangesAsync();

            // Load author name
            db.Entry(meeting).Reference(x => x.Participant).Load();

            var dto = new MeetingDTO()
            {
                Id = meeting.Id,
                Subject = meeting.Subject,          
                //Date = meeting.Date,
                //StartTime = meeting.StartTime,
                //FinishTime = meeting.FinishTime,
                ParticipantName = meeting.Participant.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = meeting.Id }, dto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Meeting))]
        public async Task<IHttpActionResult> DeleteMeeting(int id)
        {
            Meeting meeting = await db.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            db.Meetings.Remove(meeting);
            await db.SaveChangesAsync();

            return Ok(meeting);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeetingExists(int id)
        {
            return db.Meetings.Count(e => e.Id == id) > 0;
        }
    }
}
