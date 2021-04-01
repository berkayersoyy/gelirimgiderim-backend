using System.IO;

namespace Core.Utilities.FileReader
{
    public static class FileReader
    {
        public static string ReadFileName(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.FullName;
        }

        public static string ReadFileExtension(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return fileInfo.Extension;
        }
    }
}