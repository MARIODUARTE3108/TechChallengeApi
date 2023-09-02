using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechChallenge.Application.Contracts;

namespace TechChallenge.Application.Services
{
    public class AzureBlobApplicationService : IAzureBlobApplicationService
    {
        private readonly BlobContainerClient _containerClient;

        public AzureBlobApplicationService(IConfiguration configuration)
        {
            string azureConnectionString = configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
            string containerName = configuration.GetSection("AzureBlobStorage:ContainerName").Value;

            _containerClient = new BlobContainerClient(azureConnectionString, containerName);
        }
        public string UploadFiles(IFormFile file)
        {
            var azureResponse = new List<Azure.Response<BlobContentInfo>>();
            string fileExtension = Path.GetExtension(file.FileName);


            using MemoryStream fileUploadStream = new MemoryStream();
            file.CopyTo(fileUploadStream);
            fileUploadStream.Position = 0;
            BlobContainerClient blobContainerClient = _containerClient;
            var uniqueName = Guid.NewGuid().ToString() + fileExtension;
            BlobClient blobClient = blobContainerClient.GetBlobClient(uniqueName);

            blobClient.Upload(fileUploadStream, new BlobUploadOptions()
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = "image/jpg"
                }
            }, cancellationToken: default);
            return blobClient.Uri.AbsoluteUri;
        }
    }
}
