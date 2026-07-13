using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Veterinaria.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(IConfiguration config)
        {
            var account = new Account(
                config["Cloudinary:CloudName"],
                config["Cloudinary:ApiKey"],
                config["Cloudinary:ApiSecret"]
            );

            _cloudinary = new Cloudinary(account);
            _cloudinary.Api.Secure = true;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return null;

            using var stream = file.OpenReadStream();
            var uploadParams = new RawUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Folder = folderName,
                UseFilename = true,
                UniqueFilename = true
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);
            return uploadResult.SecureUrl?.ToString();
        }

        public async Task<bool> DeleteFileAsync(string fileUrl)
        {
            if (string.IsNullOrEmpty(fileUrl)) return false;

            try
            {
                var parts = fileUrl.Split('/');
                var uploadIndex = Array.IndexOf(parts, "upload");
                if (uploadIndex >= 0 && parts.Length > uploadIndex + 2)
                {
                    var publicIdParts = parts.Skip(uploadIndex + 2);
                    var publicId = string.Join("/", publicIdParts);

                    // Puesto que usamos RawUploadParams, el tipo de recurso en Cloudinary es Raw y el ID incluye la extensión.
                    var deletionParams = new DelResParams()
                    {
                        PublicIds = new List<string> { publicId },
                        ResourceType = ResourceType.Raw
                    };

                    var result = await _cloudinary.DeleteResourcesAsync(deletionParams);
                    return result.StatusCode == System.Net.HttpStatusCode.OK;
                }
            }
            catch { }
            return false;
        }
    }
}
