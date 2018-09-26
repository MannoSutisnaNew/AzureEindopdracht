using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;


namespace RequestBierRapport2.Helpers
{
    class QueueHelper
    {
        public static Boolean PlaceQueueMessage(string bierRapportJson)
        {
            // Retrieve storage account from connection string.
            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            // Create the queue client.
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a container.
            CloudQueue queue = queueClient.GetQueueReference("bierrapportmessages");

            // Create the queue if it doesn't already exist
            queue.CreateIfNotExists();

            // Create a message and add it to the queue.
            CloudQueueMessage message = new CloudQueueMessage(bierRapportJson);
            queue.AddMessage(message);
            return true;
        }
    }
}
