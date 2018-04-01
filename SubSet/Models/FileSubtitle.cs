using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubSet.Models
{
    public class FileSubtitle : Base.FileSerial
    {
        public FileSubtitle(FileInfo fileInfo) : base(fileInfo) { }
        public FileSubtitle(string filePath) : base(filePath) { }
        public FileMovie Movie { get; set; }

    }
}
