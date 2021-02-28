using RestApiClientApp_v1.Models;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestApiClientApp_v1.Services
{
    public static class ReferentialAPIInfo
    {

        const string url = "https://referential.p.rapidapi.com/v1";
       // const string urlParameters = "/lang?fields=iso_a2%2Clang_3%2Cflag&lang=en";
        const string apiKey = "<your-API-key>";
        const string apiHost = "referential.p.rapidapi.com";

        public static List<Continent> GetContinents()
        {
            string urlParameters = "/continent?lang=tr&fields=value%2C%20iso_a2";
            var continents = APICall.RunAsync< List<Continent>>(url, apiKey, apiHost, urlParameters).GetAwaiter().GetResult();
            
            if (continents != null)
            {
                for (int i = 0; i < continents.Count; i++)
                {
                    continents[i].Id = i + 1;
                }
            }

            return continents;

        }

        public static List<Country> GetCountriesByContinentId(string continentCode)
        {
            string urlParameters = "/country?lang=tr&fields=value%2C%20iso_a2&continent_code=" + continentCode ;
            
            var countries = APICall.RunAsync<List<Country>>(url, apiKey, apiHost, urlParameters).GetAwaiter().GetResult();

            return countries;

        }

        public static List<City> GetCitiesByCountryId(string countryCode)
        {
            string urlParameters = "";

            if (countryCode.Equals("tr"))
                urlParameters = "/state?lang=tr&iso_a2=" + countryCode;
            else
                urlParameters = "/city?lang=tr&iso_a2=" + countryCode;

            var cities = APICall.RunAsync<List<City>>(url, apiKey, apiHost, urlParameters).GetAwaiter().GetResult();

            if (cities == null)
            { 
                urlParameters = "/city?lang=tr&iso_a2=" + countryCode;
                cities = APICall.RunAsync<List<City>>(url, apiKey, apiHost, urlParameters).GetAwaiter().GetResult();
            }

            return cities;

        }
    }
}
