using System;
using System.Collections.Generic;

namespace CountryList.Models;

public partial class HotelRestaurant
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Type { get; set; }
    public int? Stars { get; set; }
    public int? IdCountry { get; set; }
    public virtual Country? IdCountryNavigation { get; set; }
}
