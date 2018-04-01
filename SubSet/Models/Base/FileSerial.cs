using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubSet.Models.Base
{
    public class FileSerial : FileBase
    {
        public FileSerial(FileInfo fileInfo) : base(fileInfo) { }
        public FileSerial(string filePath) : base(filePath) { }

        public int? Epizode { get; set; } = null;
    }
}
