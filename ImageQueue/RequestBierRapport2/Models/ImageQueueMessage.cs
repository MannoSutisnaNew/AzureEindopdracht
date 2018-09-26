using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestBierRapport2.Models
{
    public class ImageQueueMessage
    {
        public ImageQueueMessage(string street, string houseNumber, string residence, string imageName)
        {
            this.street = street;
            this.houseNumber = houseNumber;
            this.residence = residence;
            this.imageName = imageName;
        }

        public string street { get; set; }
        public string houseNumber { get; set; }
        public string residence { get; set; }
        public string imageName { get; set; }
    }
}
