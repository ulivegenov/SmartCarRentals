namespace SmartCarRentals.Services.Data.AppServices.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadImageAsync(IFormFile formFile, string fileName, string folder);
    }
}
