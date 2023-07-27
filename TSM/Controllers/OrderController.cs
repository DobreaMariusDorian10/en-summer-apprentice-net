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
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();

          /*  var dtoOrders = orders.Select(e => new OrderDto()
            {
                OrderId = e.OrderId,
                customerID = e.CustomerId,
                OrderedAt = e.OrderedAt,
                NumberOfTickets = e.NumberOfTickets,
                TotalPrice = e.TotalPrice
            });
*/
          var dtoOrders= _mapper.Map<List<OrderDto>>(orders);
            return Ok(dtoOrders);
        }

        [HttpGet]
        public ActionResult<OrderDto> GetById(int id)
        {
            Order result = _orderRepository.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            var dtoOrder = new OrderDto()
            {
                OrderId = result.OrderId,
                customerID = result.CustomerId,
                OrderedAt = result.OrderedAt,
                NumberOfTickets = result.NumberOfTickets,
                TotalPrice = result.TotalPrice
            };

            return Ok(dtoOrder);
        }
        [HttpPatch]
        public ActionResult<OrderPatchDto> Patch(OrderPatchDto orderPatch)
        {
            // var eventEntity = await _eventRepository.GetById(eventPatch.EventId);
            var orderEntity = _orderRepository.GetById(orderPatch.OrderID);

            if (orderEntity == null)
            {
                return NotFound();
            }
            if (orderPatch.OrderID!=null) orderEntity.NumberOfTickets = orderPatch.NumberOfTickets;
            if (orderPatch.TotalPrice!=null) orderEntity.TotalPrice = orderPatch.TotalPrice;
            _orderRepository.Update(orderEntity);
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<OrderPatchDto>> Delete(int id)
        {
            var orderEntity = _orderRepository.GetById(id);
            if (orderEntity == null)
            {
                return NotFound();
            }
            _orderRepository.Delete(orderEntity);
            return NoContent();
        }



    }
}