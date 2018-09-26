using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Blob;
using ImageQueue.Models;
using ImageQueue.Helpers;
using System.IO;


namespace ImageQueue
{
    public static class ImageQueueTrigger
    {
        [FunctionName("ImageQueueTrigger")]
        public static async void Run([QueueTrigger("bierrapportmessages", Connection = "StorageConnectionString")]string queueItem, TraceWriter log)
        {
            ImageQueueMessage queueMessage = JsonConvert.DeserializeObject<ImageQueueMessage>(queueItem);
            Coordinates coordinates = RequestHelper.getCoordinates(queueMessage);
            if (coordinates == null)
            {
                return;
            }
            double temperature = await RequestHelper.TemperatureRequest(coordinates);
            Stream imageStream = await RequestHelper.ImageRequest(coordinates);
            Stream editedImageStream = ImageHelper.TextToImage(imageStream, temperature);
            Boolean status = await BlobHelper.UploadImage(editedImageStream, queueMessage);
            log.Info($"C# Queue trigger function processed: {queueItem}");
        }
    }
}
