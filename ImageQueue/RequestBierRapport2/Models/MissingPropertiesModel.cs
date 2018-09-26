using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RequestBierRapport2.Models
{
    class MissingPropertiesModel
    {
        public List<String> missingProperties;

        public MissingPropertiesModel(List<String> missingProperties)
        {
            this.missingProperties = missingProperties;
        }
    }
}
