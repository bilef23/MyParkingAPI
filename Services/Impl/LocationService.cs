using System.Globalization;
using System.Net;
using MongoDB.Driver.Linq;
using MyParking.Models;

namespace MyParking.Services.Impl;

using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

    public class LocationService : ILocationService
    {
        private  IHttpClientFactory _httpClientFactory;

        public LocationService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<List<string>> GetAddressFromCoordinatesAsync(double latitude, double longitude)
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            httpClient.DefaultRequestHeaders.Add("User-Agent", "MyParking");
            var url = $"https://nominatim.openstreetmap.org/reverse?lat={latitude.ToString(CultureInfo.InvariantCulture)}&lon={longitude.ToString(CultureInfo.InvariantCulture)}&format=json";

            try
            {
                var response = await httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
        
                var json = await response.Content.ReadAsStringAsync();
                var locationData = JsonConvert.DeserializeObject<NominatimResponse>(json);
        
                await Task.Delay(1000);
                var addresses = locationData.DisplayName.Split(',').Take(3).ToList();
                
                return addresses;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting address: {ex.Message}");
                return new List<string>();
            }
        }
        
    }



