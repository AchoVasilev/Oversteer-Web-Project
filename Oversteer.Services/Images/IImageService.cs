namespace Oversteer.Services.Images
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CloudinaryDotNet;

    using Microsoft.AspNetCore.Http;

    using Oversteer.Data.Models.Cars;

    public interface IImageService
    {
        Task UploadImage(Cloudinary cloudinary, IEnumerable<IFormFile> images, int companyId, Car car);
    }
}
