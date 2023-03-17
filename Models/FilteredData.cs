namespace CountryList.Models
{
    public class FilteredData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int? Stars { get; set; }
        public int IdCountry { get; set; }
        public string? CountryName { get; set; }
        public string? Population { get; set; }
        public string? IsoCode { get; set; }

    }
}
