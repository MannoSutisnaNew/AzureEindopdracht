using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FetchBierRapport.Models
{
    public class ErrorModel
    {
        public string error;

        public ErrorModel(string error)
        {
            this.error = error;
        }
    }
}
