namespace Oversteer.Services.Images
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;

    using Microsoft.AspNetCore.Http;

    using Oversteer.Data.Models.Cars;

    public class ImageService : IImageService
    {
        public async Task UploadImage(Cloudinary cloudinary, IEnumerable<IFormFile> images, int companyId, Car car)
        {
            var AllowedExtensions = new[] { "jpg", "png", "gif", "jpeg" };

            foreach (var image in images)
            {
                var extension = Path.GetExtension(image.FileName).TrimStart('.');

                if (!AllowedExtensions.Any(x => extension.EndsWith(x)))
                {
                    throw new Exception($"Invalid image extension {extension}");
                }

                string imageName = image.FileName;

                byte[] destinationImage;
                using (var memoryStream = new MemoryStream())
                {
                    await image.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var ms = new MemoryStream(destinationImage))
                {
                    // Cloudinary doesn't work with &
                    imageName = imageName.Replace("&", "And");

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(imageName, ms),
                        PublicId = imageName,
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    var dbImage = new CarImage()
                    {
                        CompanyId = companyId,
                        Extension = extension,
                        Url = uploadResult.SecureUrl.AbsoluteUri
                    };

                    car.CarImages.Add(dbImage);
                }
            }
        }
    }
}
