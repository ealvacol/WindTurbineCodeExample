using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindturbineMessageProvider.Models
{
    public class Site
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public List<Turbine> Turbines { get; set; }
    }
}
