using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Veterinaria.Services
{
    public interface ICloudinaryService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
        Task<bool> DeleteFileAsync(string fileUrl);
    }
}
