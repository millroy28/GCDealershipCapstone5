using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace GCDealershipCapstone.Models
{
    public class InventoryAPIDAL
    {
        public HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44375/");
            return client;
        }

        public async Task<List<Inventory>> GetInventory()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("api/GCDealershipInventory");
            var inventory = await response.Content.ReadAsAsync<List<Inventory>>();
            return inventory;
        }

        //FilterBy(string make, string model, string style, int year, string color, int mileage)
        public async Task<List<Inventory>> GetInventorySearch(string make, string model, string style, int year, string color, int mileage)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"api/GCDealershipInventory/FilterBy/{make}/{model}/{style}/{year}/{color}/{mileage}");
            var inventory = await response.Content.ReadAsAsync<List<Inventory>>();
            return inventory;
        }

        public async Task<Inventory> GetCar(int id)
        {
            var client = GetHttpClient();
            var response = await client.GetAsync($"api/GCDealershipInventory/GetCar/{id}");
            var result = await response.Content.ReadAsAsync<Inventory>();
            return result;
        }
    }
}
