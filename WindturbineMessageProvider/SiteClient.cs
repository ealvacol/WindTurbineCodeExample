using Newtonsoft.Json;
using WindturbineMessageProvider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindturbineMessageProvider
{
    public static class SiteClient
    {
        private const string _clientUri = "https://windturbinesapi20230422203748.azurewebsites.net/WindParkInformation";
        static HttpClient httpClient = new HttpClient();

        internal static async Task<IEnumerable<Site>> GetSitesAsync()
        {
            List<Site> sites;
            HttpResponseMessage response = await httpClient.GetAsync(_clientUri);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                sites = JsonConvert.DeserializeObject<List<Site>>(json);
            }
            else
            {
                throw new InvalidOperationException($"Recieved Status {response.IsSuccessStatusCode}");
            }

            return sites;
        }
    }
}
