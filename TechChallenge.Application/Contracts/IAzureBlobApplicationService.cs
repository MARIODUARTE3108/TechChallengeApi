using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechChallenge.Application.Contracts
{
    public interface IAzureBlobApplicationService
    {
        public string UploadFiles(IFormFile file);
    }
}
