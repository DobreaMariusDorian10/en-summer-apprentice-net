using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Data;
using TSM.Models;
using TSM.Repositories;

namespace TMS.Api.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public int Add(Order @order)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order orderEntity)
        {
            _dbContext.Remove(orderEntity);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var Orders = _dbContext.Orders.Include(e=>e.TicketCategory).Include(e=>e.TicketCategory.Event);

            return Orders;
        }

        public Order GetById(int id)
        {
            //var result =  _dbContext.Orders.Where(e => e.OrderId == id).FirstOrDefaultAsync();
            var result = _dbContext.Orders
                .Where(e => e.OrderId == id).FirstOrDefault();
                //.Include(e => e.TicketCategory).Include(e => e.TicketCategory.Description)
                //.FirstOrDefault();

            if (result == null)
            {
                throw new Exception("The object was not found!");
            }
            return result;
        }

        /*    public Event GetById(int id)
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
        }*/

        public void Update(Order @order)
        {
            _dbContext.Entry(@order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}