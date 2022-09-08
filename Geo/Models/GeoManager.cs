using Dadata;
using System.Collections;
using System.Threading.Tasks;

namespace Geo.Models
{
    public class GeoManager : IManager
    {
        private static readonly string token = "52a89f937dabdf357c3b7aaa652416e0142f00c1";
        public Storage storage = Storage.GetInstance();

        private readonly IRepository repository;
        public GeoManager(IRepository repository)
        {
            this.repository = repository;
        }


        public async Task<string> GetReverseGeocodeAsync(double latitude, double longitude)
        {
            // проверим есть ли точка в хранилище и если есть сразу возвращаем ответ
            string answerFromStorage = storage.FindAndGetPoint(latitude, longitude);
            if (answerFromStorage != null)
                return($"Координаты найдены в хранилище: {answerFromStorage}");

            // точки в хранилище нет          
            // проверим есть ли точка в базе
            var valueInDb = await repository.SearchByLatAndLonAsync(latitude, longitude);
            if (valueInDb != null)
            {
                return $"Координаты найдены в базе: страна {valueInDb.Country}, {valueInDb.RegionType} {valueInDb.RegionName}, {valueInDb.CityType} {valueInDb.CityName}, {valueInDb.StreetType} {valueInDb.StreetName}, {valueInDb.HouseType} {valueInDb.HouseNumber} ";
            }
            // точки нет ни в хранилище ни в базе
            // делаем запрос к Dadata
            var api = new SuggestClientAsync(token);
            var result = await api.Geolocate(latitude, longitude);
            var address = result.suggestions[0].data;
            var answerFromDaData =
                @$"Координаты относятся к стране {address.country},
                            {address.region_type_full} {address.region}, 
                            {address.city_type_full} {address.city}, 
                            {address.street_type_full} {address.street},
                            {address.house_type_full} {address.house} ";
            // записываем точку в базу
            await repository.CreateNewPointAsync(latitude, longitude, address);
            // записываем точку в кеш
            storage.AddPoint(latitude, longitude, answerFromDaData);
            return answerFromDaData;

        }

        public async Task<ArrayList> GetSuggestAddressAsync(string inputName)
        {
            ArrayList results = new();
            string name = inputName;
            var api = new SuggestClientAsync(token);
            var response = await api.SuggestAddress(name);
            var address = response.suggestions[0].data;

            results.Add(address.country + " " + address.region_type_full + " " + address.region + " ");

            for (int i = 0; i < response.suggestions.Count; i++)
            {
                var answer = response.suggestions[i].data;
                string result = answer.city_type_full + " " + answer.city + " " + answer.street_type + " " + answer.street + " ";
                results.Add(result);
            }
            return results;
        }
    }
}
