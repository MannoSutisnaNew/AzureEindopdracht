using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

using FetchBierRapport.Models;
using FetchBierRapport.Helpers;


namespace FetchBierRapport
{
    public static class FetchBierRapport
    {
        // endpoint of function is https://{function app name}.azurewebsites.net/api/{function name}
        [FunctionName("FetchBierRapport")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            FetchRapportModel model = await req.Content.ReadAsAsync<FetchRapportModel>();
            List<String> missingProperties = model.missingProperties();
            if (missingProperties.Count > 0)
            {
                MissingPropertiesModel missingPropertiesModel = new MissingPropertiesModel(missingProperties);
                string missingPropertiesJson = JsonConvert.SerializeObject(missingPropertiesModel);
                return new HttpResponseMessage()
                {
                    Content = new StringContent(missingPropertiesJson, System.Text.Encoding.UTF8, "application/json")
                };
            }
            string imageUrl = await BlobHelper.GetImageUrl(model);
            if (imageUrl == null)
            {
                ErrorModel error = new ErrorModel("Your image is not available at this moment");
                string missingPropertiesJson = JsonConvert.SerializeObject(error);
                return new HttpResponseMessage()
                {
                    Content = new StringContent(missingPropertiesJson, System.Text.Encoding.UTF8, "application/json")
                };
            }
            else
            {
                RapportModel rapport = new RapportModel(imageUrl);
                string missingPropertiesJson = JsonConvert.SerializeObject(rapport);
                return new HttpResponseMessage()
                {
                    Content = new StringContent(missingPropertiesJson, System.Text.Encoding.UTF8, "application/json")
                };
            }
        }
    }
}