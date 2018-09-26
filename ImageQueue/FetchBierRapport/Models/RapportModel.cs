using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchBierRapport.Models
{
    public class RapportModel
    {
        public string imageUrl;

        public RapportModel(string imageUrl)
        {
            this.imageUrl = imageUrl;
        }
    }
}
