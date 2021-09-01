using Microsoft.AspNetCore.StaticFiles;
using Photography.Api.Core;
using Photography.Api.Models;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Photography.Api.Data
{
    public static class SeedData
    {
        public static void Seed(PhotographyDbContext context)
        {
            var passwordHasher = new PasswordHasher();

            var user = new User("Quinntyne","password", passwordHasher);

            if(context.Users.SingleOrDefault(x => x.Username == user.Username) == null)
            {
                context.Users.Add(user);

                context.SaveChanges();
            }

            var provider = new FileExtensionContentTypeProvider();

            var directory = $"{System.Environment.CurrentDirectory}\\Data\\Seed\\Images";

            foreach (var path in System.IO.Directory.GetFiles(directory))
            {
                var name = System.IO.Path.GetFileName(path);

                if(context.Photos.SingleOrDefault(x => x.Name == name) == null)
                {
                    provider.TryGetContentType(name, out string contentType);

                    var photo = new Photo(name);

                    var bytes = StaticFileLocator.Get(name);

                    using(var image = Image.FromStream(new MemoryStream(bytes)))
                    {
                        photo.Update(bytes, contentType, image.PhysicalDimension.Height, image.PhysicalDimension.Width);
                    }

                    context.Photos.Add(photo);

                    context.SaveChanges();
                }
            }
        }
    }
}
