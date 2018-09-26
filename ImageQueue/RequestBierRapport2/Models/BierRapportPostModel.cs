using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestBierRapport2.Models
{
    class BierRapportPostModel
    {
        public string street;
        public string houseNumber;
        public string residence;

        public BierRapportPostModel(string street, string houseNumber, string residence)
        {
            this.street = street;
            this.houseNumber = houseNumber;
            this.residence = residence;
        }

        public List<String> missingProperties()
        {
            List<String> missingProperties = new List<String>();
            if (this.street == null)
            {
                missingProperties.Add("street");
            }
            if (this.houseNumber == null)
            {
                missingProperties.Add("houseNumber");
            }
            if (this.residence == null)
            {
                missingProperties.Add("residence");
            }
            return missingProperties;
        }
    }
}
