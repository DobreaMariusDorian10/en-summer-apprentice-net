﻿using System;
using System.Collections.Generic;

namespace TSM.Models;

public partial class TicketCategory
{
    public int TicketCategoryId { get; set; }

    public double? Price { get; set; }

    public int? EventId { get; set; }

    public string? Description { get; set; }

    public virtual Event? Event { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
