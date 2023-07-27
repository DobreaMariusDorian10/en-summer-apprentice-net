namespace TSM.Models.Dto
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public int customerID { get; set; }
        public DateTime OrderedAt { get; set; }
        public double NumberOfTickets { get; set; }
        public double TotalPrice { get; set; }
        public string EventName { get; set; }   
    }
}
