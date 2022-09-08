using Geo.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Geo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoController : ControllerBase
    {
        private readonly IManager manager;
        public GeoController(IManager manager)
        {
            this.manager = manager;
        }

        [HttpGet("ReverseGeocode")]

        public async Task<string> GetAsync(double latitude, double longitude) =>
            await manager.GetReverseGeocodeAsync(Math.Round(latitude, 6), Math.Round(longitude, 6));

        [HttpGet("Autocomplete")]
        public async Task<ArrayList> GetAsync(string inputName) =>
            await manager.GetSuggestAddressAsync(inputName);
    }
}
