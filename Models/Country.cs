using System;
using System.Collections.Generic;

namespace CountryList.Models;

public partial class Country
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Population { get; set; }
    public string? IsoCode { get; set; }
    public virtual ICollection<HotelRestaurant> HotelRestaurants { get; } = new List<HotelRestaurant>();
}
