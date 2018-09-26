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

using RequestBierRapport2.Models;
using RequestBierRapport2.Helpers;


namespace RequestBierRapport2
{
    public static class RequestBierRapport
    {
        [FunctionName("RequestBierRapport")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            BierRapportPostModel model = await req.Content.ReadAsAsync<BierRapportPostModel>();
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
            string bierRapportName = "bierrapport" + Guid.NewGuid().ToString() + ".png";
            ImageQueueMessage queueMessage = new ImageQueueMessage(model.street, model.houseNumber, model.residence, bierRapportName);
            string queueMessageJson = JsonConvert.SerializeObject(queueMessage);
            QueueHelper.PlaceQueueMessage(queueMessageJson);
            BierRapportNameModel bierRapportNameModel = new BierRapportNameModel(bierRapportName);
            string bierRapportNameJson = JsonConvert.SerializeObject(bierRapportNameModel);
            return new HttpResponseMessage()
            {
                Content = new StringContent(bierRapportNameJson, System.Text.Encoding.UTF8, "application/json")
            };
        }
    }
}
