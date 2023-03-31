using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PharmacyWebAPI.Utility.Services.IServices;
using PharmacyWebAPI.Utility.Setting;

namespace PharmacyWebAPI.Utility.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySetting> config)
        {
            var acc = new Account
                (
                    config.Value.CloudName,
                    config.Value.ApiKey,
                    config.Value.ApiKey
                );
            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.Name, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteResult = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteResult);
            return result;
        }
    }
}