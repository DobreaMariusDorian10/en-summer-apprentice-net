using System.Collections.Generic;
using TSM.Models;

namespace TSM.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll();
        Event GetById(int id);
        int Add(Event @event);
        void Update(Event @event);
        void Delete(Event eventEntity);
    }
}
