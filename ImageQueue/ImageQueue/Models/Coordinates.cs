using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageQueue.Models
{
    public class Coordinates
    {
        public string longitude { get; set; }
        public string latitude { get; set; }

        public Coordinates(string longitude, string latitude)
        {
            this.longitude = longitude;
            this.latitude = latitude;
        }
    }
}
