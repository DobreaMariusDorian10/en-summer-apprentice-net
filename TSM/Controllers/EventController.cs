using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TSM.Models;
using TSM.Models.Dto;
using TSM.Repositories;

namespace TMS.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = events.Select(e => new EventDto()
            {
                EventId = e.EventId,
                EventDescription = e.EventDescription,
                EventName = e.EventName,
                EventType = e.EventType?.EventTypeName ?? string.Empty,
                Venue = e.Venue?.LocationName ?? string.Empty
            });


            return Ok(dtoEvents);
        }


        [HttpGet]
        public ActionResult<EventDto> GetById(int id)
        {
            Event result = _eventRepository.GetById(id);

            if (result == null)
            {
                return NotFound();
            }

            //var dtoEvent = new EventDto()
            //{
            //    EventId = result.EventId,
            //    EventDescription = result.EventDescription,
            //    EventName = result.EventName,
            //    EventType = result.EventType?.EventTypeName ?? string.Empty,
            //    Venue = result.Venue?.LocationName ?? string.Empty
            //};                                          // Astea 2 sunt 
            var eventDto=_mapper.Map<EventDto>(result);  // 
            
            return Ok(eventDto);
        }


        [HttpPatch]
        public ActionResult<EventPatchDto> Patch(EventPatchDto eventPatch)
        {
            // var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            var eventEntity = _eventRepository.GetById(eventPatch.EventId);

            if (eventEntity == null)
            {
                return NotFound();
            }
            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            _eventRepository.Update(eventEntity);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<EventPatchDto>> Delete(int id)
        {
            var eventEntity =  _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete(eventEntity);
            return NoContent();
        }

    }
}