using Microsoft.Extensions.Configuration;
using Photography.Api.Models;

namespace Photography.Api.Features
{
    public static class PhotoExtensions
    {
        public static PhotoDto ToServeDto(this Photo photo)
        {
            return new ()
            {
                Bytes = photo.Bytes,
                ContentType = photo.ContentType
            };
        }

        public static PhotoDto ToDto(this Photo photo, IConfiguration configuration)
        {
            return new()
            {
                PhotoId = photo.PhotoId,                
                ContentType = photo.ContentType,
                Name = photo.Name,
                Height = photo.Height,
                Width = photo.Width,
                Src = $"{configuration["BaseUrl"]}api/photo/serve/{photo.PhotoId}"
            };
        }

    }
}
