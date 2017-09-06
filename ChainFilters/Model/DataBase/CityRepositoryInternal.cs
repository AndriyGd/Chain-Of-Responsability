using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainFilters.Model.DataBase
{
    public class CityRepositoryInternal : ICityRepository
    {
        private List<City> _cities;

        public List<City> Cities => _cities ?? (_cities = GetCities());

        private static List<City> GetCities()
        {
            var cities = new List<City>
            {
                new City {Name = "Lviv"},
                new City {Name = "Vinnitsa"},
                new City {Name = "Gdansk"},
                new City {Name = "Bothell"},
                new City {Name = "Dallas"},
                new City {Name = "Dnipro"},
                new City {Name = "Portland"},
                new City {Name = "Odessa"},
                new City {Name = "Kiev"},
                new City {Name = "Nevada"},
                new City {Name = "Kharkiv"},
                new City {Name = "Kryvyi Rih"},
                new City {Name = "Mykolayiv"},
                new City {Name = "Phoenix"},
                new City {Name = "Memphis"},
                new City {Name = "Mariupol"},
                new City {Name = "Kherson"},
                new City {Name = "Orlando"},
                new City {Name = "Ottawa"},
                new City {Name = "Montreal"},
                new City {Name = "Poltava"},
                new City {Name = "Chernihiv"},
                new City {Name = "Calgary"},
                new City {Name = "Bordeaux"},
                new City {Name = "Cherkasy"},
                new City {Name = "Sumy"},
                new City {Name = "Berlin"},
                new City {Name = "Zhytomyr"},
                new City {Name = "Melbourne"},
                new City {Name = "Rivne"},
                new City {Name = "Cambridge"},
                new City {Name = "Detroit"},
                new City {Name = "Chernivtsi"},
                new City {Name = "Ternopil’"},
                new City {Name = "Kenmore"},
                new City {Name = "Snohomish"},
                new City {Name = "Kremenchuk"},
                new City {Name = "Luts’k"},
                new City {Name = "Monroe"},
                new City {Name = "Duvall"},
                new City {Name = "Ivano-Frankivs’k"},
                new City {Name = "Carnation"},
                new City {Name = "Bila Tserkva"},
                new City {Name = "Kramators’k"},
                new City {Name = "Seattle"},
                new City {Name = "Sloviansk"},
                new City {Name = "Edmonds"},
                new City {Name = "Berdyans’k"},
            };

            return cities;
        }
    }
}
