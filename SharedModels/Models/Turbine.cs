using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindturbineMessageProvider.Models
{
    public class Turbine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Version { get; set; }
        public int MaxProduction { get; set; }
        public double CurrentProduction { get; set; }
        public double Windspeed { get; set; }
        public string WindDirection { get; set; }
    }
}
