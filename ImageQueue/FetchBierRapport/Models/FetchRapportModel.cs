using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchBierRapport.Models
{
    public class FetchRapportModel
    {
        public string imageName;

        public FetchRapportModel(string imageName)
        {
            this.imageName = imageName;
        }

        public List<String> missingProperties()
        {
            List<String> missingProperties = new List<String>();
            if (this.imageName == null)
            {
                missingProperties.Add("imageName");
            }
            return missingProperties;
        }
    }
}
