namespace TSM.Models.Dto
{
    public class OrderPatchDto
    {
        public int OrderID { get; set; }
        public double NumberOfTickets { get; set; }
        public double TotalPrice { get; set; }
    }
}
