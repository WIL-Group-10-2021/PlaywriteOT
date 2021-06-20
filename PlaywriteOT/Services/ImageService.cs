using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PlaywriteOT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaywriteOT.Services
{
    public class ImageService 
    {
        private readonly Cloudinary _cloudinary;

        public ImageService(IOptions<CloudinarySettings> config)
        {
            var acc = new Account
            (
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        public async Task<ImageUploadResult> AddImageAsync(IFormFile file)
        {
            var upload = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
    /*                Transformation = new Transformation().Height(500).Width(500)
                        .Crop("fill").Gravity("face")*/
                };

                upload = await _cloudinary.UploadAsync(uploadParams); //uploads to cloudinary
            }

            return upload;  //db needs to store 3 properties of this
        }

        public async Task<ImageUploadResult> AddPDFAsync(IFormFile file)
        {
            var upload = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                };

                upload = await _cloudinary.UploadAsync(uploadParams); //uploads to cloudinary  
            }

            return upload;  //db needs to store 3 properties of this
        }

        public async Task<DeletionResult> DeleteImageAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
