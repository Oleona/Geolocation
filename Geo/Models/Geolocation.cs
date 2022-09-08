using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Geo.Models
{
    public class Geolocation
    {   

        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// широта
        /// </summary>    
        [Column("latitude")]
        public double Latitude { get; set; }

        /// <summary>
        /// долгота
        /// </summary> 
        [Column("longitude")]
        public double Longitude { get; set; }

        /// <summary>
        /// Страна
        /// </summary>
        [Column("country")]
        public string Country { get; set; }

        /// <summary>
        /// тип региона
        /// </summary>
        [Column("regionType")]
        public string RegionType { get; set; }

        /// <summary>
        /// наименование региона
        /// </summary>
        [Column("regionName")]
        public string RegionName { get; set; }

        /// <summary>
        /// тип города
        /// </summary>
        [Column("cityType")]
        public string CityType { get; set; }

        /// <summary>
        /// наименование города
        /// </summary>
        [Column("cityName")]
        public string CityName { get; set; }

        /// <summary>
        /// тип улицы
        /// </summary>
        [Column("streetType")]
        public string StreetType { get; set; }

        /// <summary>
        /// название улицы
        /// </summary>
        [Column("streetName")]
        public string StreetName { get; set; }

        /// <summary>
        /// тип дома
        /// </summary>
        [Column("houseType")]
        public string HouseType { get; set; }

        /// <summary>
        /// номер дома, строка так как может быть 7а
        /// </summary>
        [Column("houseNumber")]
        public string HouseNumber { get; set; }
    }
}
