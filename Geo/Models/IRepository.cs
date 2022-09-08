using Dadata.Model;
using System.Threading.Tasks;

namespace Geo.Models
{
    public interface IRepository
    {
        public Task<Geolocation> SearchByLatAndLonAsync(double latitude, double longitude);
        public Task CreateNewPointAsync(double latitude, double longitude, Address address);
    }
}
