using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            // Checks to see if file param is empty
            if (file.Length > 0)
            {
                // Logic to upload the image file to Cloudinary
                // Gets file as a stream of data
                using var stream = file.OpenReadStream();
                var uploadParems = new ImageUploadParams 
                {
                    File = new FileDescription(file.FileName, stream),
                    // Ensures that the photos have an aspect ratio of a square and the crop will focus on the face shown in the image
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParems);
            }
            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string PublicId)
        {
            var deleteParams = new DeletionParams(PublicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}