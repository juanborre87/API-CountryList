namespace CountryList.Models
{
    public class CountryWeb
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Population { get; set; }
        public string? IsoCode { get; set; }
        public List<HotelWeb> Hotel { get; set; }
        public List<RestaurantWeb> Restaurant { get; set;}

    }
}
