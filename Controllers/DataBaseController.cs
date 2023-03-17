using CountryList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CountryList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataBaseController : ControllerBase
    {
        private readonly DbcountryListContext _context;
        public DataBaseController( DbcountryListContext context ) { 
            _context = context;
        }

        [HttpGet("list")]
        public async Task<IActionResult> Get([FromQuery] int? page, [FromQuery] string? filter, [FromQuery] int? rowsPerPage)
        {
            int _page = page ?? 1;
            string _filter = filter ?? "";
            int _rowsPerPage = rowsPerPage ?? 1;
            var buffer = Get(new List<CountryWeb>(), _filter);
            decimal totalRecords = buffer.Count();
            int totalPages = Convert.ToInt32(Math.Ceiling(totalRecords / _rowsPerPage));
            buffer = buffer.Skip((_page - 1) * _rowsPerPage).Take(_rowsPerPage).ToList();
            Response.Headers.Add("TotalRecords", totalRecords.ToString());

            return Ok(new
            {
                pages = totalPages,
                records = buffer,
                currentPages = _page 
            });
        }
        private List<CountryWeb> Get(List<CountryWeb> countries, string filter)
        {
            SqlParameter filterSql = new("@filter", filter);
            List<FilteredData> filteredData = _context.FilteredData.FromSql($"dbo.List {filterSql}").ToList();
            List<string> countryName = new List<string>();
            foreach (FilteredData filtered in filteredData) { countryName.Add(filtered.CountryName); }
            countryName = new HashSet<string>(countryName).ToList();
            foreach (string country in countryName)
            {
                CountryWeb countryWeb = new CountryWeb();
                countryWeb.Restaurant = new List<RestaurantWeb>();
                countryWeb.Hotel = new List<HotelWeb>();
                foreach (FilteredData filtered in filteredData)
                {
                    if (filtered.CountryName == country)
                    {
                        countryWeb.Id = filtered.Id;
                        countryWeb.Name = filtered.CountryName;
                        countryWeb.Population = filtered.Population;
                        countryWeb.IsoCode = filtered.IsoCode;
                        if (!string.IsNullOrEmpty(filtered.Type))
                        {
                            RestaurantWeb restaurant = new RestaurantWeb
                            {
                                Id = filtered.Id,
                                Name = filtered.Name,
                                Type = filtered.Type
                            };
                            countryWeb.Restaurant.Add(restaurant);
                        }
                        else
                        {
                            HotelWeb hotel = new HotelWeb
                            {
                                Id = filtered.Id,
                                Name = filtered.Name,
                                Stars = filtered.Stars
                            };
                            countryWeb.Hotel.Add(hotel);
                        }

                    }

                }
                countries.Add(countryWeb);
            }

            return countries;
        }

    }
}
