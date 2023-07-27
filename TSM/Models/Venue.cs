using System;
using System.Collections.Generic;

namespace TSM.Models;

public partial class Venue
{
    public int VenueId { get; set; }

    public string? LocationName { get; set; }

    public string? LocationType { get; set; }

    public int? Capacity { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
