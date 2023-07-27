using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using TSM.Models;
using TSM.Repositories;

namespace TMS.Api.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event eventEntity)
        {
            _dbContext.Remove(eventEntity);
            _dbContext.SaveChanges();        
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;

            return events;
        }

        public Event GetById(int id)
        {
            //var result = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();
            var result = _dbContext.Events
                .Where(e => e.EventId == id)
                .Include(e => e.Venue)
                .Include(e => e.EventType)
                .FirstOrDefault();

            if (result == null) {
                throw new Exception("The object was not found!");
                    } 
            return result;
        }

        public void Update(Event @event)
        {
           _dbContext.Entry(@event).State=EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}