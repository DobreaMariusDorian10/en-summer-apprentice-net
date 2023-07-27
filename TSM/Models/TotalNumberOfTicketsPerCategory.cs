using System;
using System.Collections.Generic;

namespace TSM.Models;

public partial class TotalNumberOfTicketsPerCategory
{
    public int? TicketCategoryId { get; set; }

    public int? NrOfTickets { get; set; }

    public double? TotalOrderAmount { get; set; }
}
