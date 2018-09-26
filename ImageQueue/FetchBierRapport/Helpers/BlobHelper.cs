using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using System.IO;
using FetchBierRapport.Models;

namespace FetchBierRapport.Helpers
{
    public static class BlobHelper
    {
        public static async Task<String> GetImageUrl(FetchRapportModel model)
        {
            CloudStorageAccount storageAccount = null;
            CloudBlobContainer cloudBlobContainer = null;
            // string storageConnectionString = Environment.GetEnvironmentVariable("DefaultEndpointsProtocol=https;AccountName=eindopdracht;AccountKey=6mIYTFe2w7/roeqmOqdMo8jIFsyvnDZ3vZanDH1viL3uokZz7WBHazA2WnlKH8immWlc24P7MnhT9KvHdfrRNA==;EndpointSuffix=core.windows.net");
            //string storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=eindopdracht;AccountKey=6mIYTFe2w7/roeqmOqdMo8jIFsyvnDZ3vZanDH1viL3uokZz7WBHazA2WnlKH8immWlc24P7MnhT9KvHdfrRNA==;EndpointSuffix=core.windows.net";
            string connectionString = Environment.GetEnvironmentVariable("StorageConnectionString");
            // Check whether the connection string can be parsed.
            if (CloudStorageAccount.TryParse(connectionString, out storageAccount))
            {
                try
                {
                    // Create the CloudBlobClient that represents the Blob storage endpoint for the storage account.
                    CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
                    // Create a container called 'quickstartblobs' and append a GUID value to it to make the name unique. 
                    cloudBlobContainer = cloudBlobClient.GetContainerReference("bierrapporten");
                    bool created = await cloudBlobContainer.CreateIfNotExistsAsync();
                    if (created)
                    {
                        // Set the permissions so the blobs are public. 
                        BlobContainerPermissions permissions = new BlobContainerPermissions
                        {
                            PublicAccess = BlobContainerPublicAccessType.Blob
                        };
                        await cloudBlobContainer.SetPermissionsAsync(permissions);
                    }
                    CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(model.imageName);
                    Boolean exists = await cloudBlockBlob.ExistsAsync();
                    if (exists)
                    {
                        return cloudBlockBlob.Uri.AbsoluteUri;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (StorageException ex)
                {
                    Console.WriteLine("Error returned from the service: {0}", ex.Message);
                    return null;
                }
            }
            else
            {
                Console.WriteLine(
                    "A connection string has not been defined in the system environment variables. " +
                    "Add a environment variable named 'storageconnectionstring' with your storage " +
                    "connection string as a value.");
                return null;
            }
        }
    }
}
