﻿using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace PharmacyWebAPI.Utility.Services.IServices
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> AddPhotoAsync(IFormFile file);

        Task<DeletionResult> DeletePhotoAsync(string publicId);
    }
}