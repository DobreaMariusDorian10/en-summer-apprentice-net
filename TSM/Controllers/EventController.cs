using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TMS.Api.Repositories;
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
        private readonly ILogger _logger;
        public EventController(IEventRepository eventRepository, IMapper mapper,ILogger<EventController> logger )
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            _logger = logger;//C:\temp
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
        public  async Task<ActionResult<EventDto>> GetById(int id)
        {
            try {
                Event result = await _eventRepository.GetById(id);

                if (result == null)
                {
                    throw new Exception("Nu exista in baza de date");
                }

                //var dtoEvent = new EventDto()
                //{
                //    EventId = result.EventId,
                //    EventDescription = result.EventDescription,
                //    EventName = result.EventName,
                //    EventType = result.EventType?.EventTypeName ?? string.Empty,
                //    Venue = result.Venue?.LocationName ?? string.Empty
                //};                                          // Astea 2 sunt 
                var eventDto = _mapper.Map<EventDto>(result);  // 

                return Ok(eventDto);
            } catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            // var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

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
            var eventEntity = await _eventRepository.GetById(id);
            if (eventEntity == null)
            {
                return NotFound();
            }
            _eventRepository.Delete( eventEntity);
            return NoContent();
        }

    }
}