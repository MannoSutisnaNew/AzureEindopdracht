using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQueue.Models
{
    public class BierRapportModel
    {
        public string displayTemperature;
        public string temperatureCondition;
        public string beerStatus;

        public BierRapportModel(string displayTemperature, string temperatureCondition, string beerStatus)
        {
            this.displayTemperature = displayTemperature;
            this.temperatureCondition = temperatureCondition;
            this.beerStatus = beerStatus;
        }
    }
}
