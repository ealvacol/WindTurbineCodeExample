using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindturbineMessageProvider.Models;
using SharedModels.Models;
using Microsoft.Extensions.Hosting.WindowsServices;
using WindturbineMessageProvider.Helpers;

namespace WindturbineMessageProvider
{
    public static class Aggregator
    {
        public const int AGGREGATION_PERIOD_S = 10;
        public const int AGGREGATION_INTERVAL_S  = 2;

        internal static List<Site> SitesCache { get; set; } = new List<Site>();  

        public static async void StartLoop()
        {
            while (true)
            {
                Console.WriteLine($"Starting aggreagation cycle: {SitesCache.Count}");
                var startTime = DateTime.Now;
                while ((DateTime.Now - startTime).TotalSeconds < AGGREGATION_PERIOD_S)
                {
                    var sites = await SiteClient.GetSitesAsync();
                    await Task.Delay(AGGREGATION_INTERVAL_S * 1000);
                    
                    Console.WriteLine("Getting Values from API");

                    SitesCache.AddRange(sites);
                }
                var aggregatedTurbines = Aggregate();
                aggregatedTurbines.ForEach(t => RabbitMqHelper.SendAggregatedTurbines(t));
                SitesCache.Clear();
            }
        }

        private static List<SiteOverview> Aggregate()
        {
            Console.WriteLine("Aggregating Turbines");

            var siteGropus = SitesCache.GroupBy(s => s.Id);
            var output = new List<SiteOverview>();

            foreach (var siteGroup in siteGropus)
            {
                var turbinesGroups = siteGroup.SelectMany(s => s.Turbines).GroupBy(Turbine => Turbine.Id);
                var currentSite = siteGroup.FirstOrDefault();
                var site = new SiteOverview 
                { 
                    Country = currentSite.Country,
                    Id = currentSite.Id, 
                    Name = currentSite.Name, 
                    Region = currentSite.Region,
                    TimeStamp = GetRoundedDateTime(),
                };

                var turbineOverviews = turbinesGroups.Select(group => new TurbineOverview
                {
                    Id = group.Key,
                    Name = group.First().Name,
                    Manufacturer = group.First().Manufacturer,
                    CurrentProduction = group.Average(t => t.CurrentProduction),
                    Windspeed = group.Average(turbine => turbine.Windspeed)
                }).ToList() ;

                site.TurbinesOverviews = turbineOverviews;
                output.Add(site);
            }

            return output;
        }

        private static DateTime GetRoundedDateTime()
        {
            var dateTime = DateTime.Now;
            dateTime = dateTime.AddSeconds(-dateTime.Second);
            return dateTime;
        }
    }
}
