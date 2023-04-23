using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedModels.Models
{
    public class SiteOverview
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<TurbineOverview> TurbinesOverviews { get; set; }

        public DateTime TimeStamp { get; set; }

        public string RedisId => $"{Id}_{TimeStamp}";
    }
}
