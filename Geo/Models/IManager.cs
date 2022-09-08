using System.Collections;
using System.Threading.Tasks;

namespace Geo.Models
{
    public interface  IManager
    {
        public Task<string> GetReverseGeocodeAsync(double latitude, double longitude);
        public Task<ArrayList> GetSuggestAddressAsync(string inputName);
    }
}
