using System.IO;
using System.Linq;
using SFB;

namespace yutokun.SphericalMediaPlayer
{
    public static class FileExtension
    {
        static readonly string[] Video = { "mp4", "mov", "m4v" };
        static readonly string[] Photo = { "png", "jpg", "jpeg" };

        static string[] Media => Video.Concat(Photo).ToArray();

        public static readonly ExtensionFilter[] Filters =
        {
            new("Media", Media),
        };

        public static bool IsVideoPath(this string path)
        {
            var extension = Path.GetExtension(path).TrimStart('.');
            return Video.Contains(extension);
        }

        public static bool IsPhotoPath(this string path)
        {
            var extension = Path.GetExtension(path).TrimStart('.');
            return Photo.Contains(extension);
        }
    }
}
