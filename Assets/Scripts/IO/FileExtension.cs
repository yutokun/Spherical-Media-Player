using System.IO;
using System.Linq;
using SFB;

namespace yutokun.SphericalMediaPlayer
{
    public static class FileExtension
    {
        static readonly ExtensionFilter Movie = new("Movies", "mp4", "mov", "m4v");
        static readonly ExtensionFilter Photo = new("Photos", "png", "jpg", "jpeg");

        public static readonly ExtensionFilter[] Filters =
        {
            Movie,
            Photo,
        };

        public static bool IsMoviePath(this string path)
        {
            var extension = Path.GetExtension(path).TrimStart('.');
            return Movie.Extensions.Contains(extension);
        }

        public static bool IsPhotoPath(this string path)
        {
            var extension = Path.GetExtension(path).TrimStart('.');
            return Photo.Extensions.Contains(extension);
        }
    }
}
