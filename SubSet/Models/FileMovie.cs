using System.IO;

namespace SubSet.Models
{
    public class FileMovie : Base.FileSerial
    {
        public FileMovie(FileInfo fileInfo) : base(fileInfo) { }
        public FileMovie(string filePath) : base(filePath) { }
    }
}