using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SubSet.Models
{
    public class FileFont : Base.FileBase
    {
        public FileFont(FileInfo fileInfo) : base(fileInfo) { }
        public FileFont(string filePath) : base(filePath) { }

        public void Install()
        {
            Process.Start(Path);
        }
    }
}
