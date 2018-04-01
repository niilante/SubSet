using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubSet.Models.Base
{
    public abstract class FileBase
    {
        public FileInfo Info { get; protected set; }
        public string Path { get { return Info.FullName; } }
        public string Name { get { return Info.Name; } }
        public string PureName { get { return System.IO.Path.GetFileNameWithoutExtension(Path); } }
        public string Extension { get { return Info.Extension; } }
        public DirectoryInfo Directory { get { return Info.Directory; } }
        public string DirectoryPath { get { return Directory.FullName; } }

        public FileBase(FileInfo fileInfo) => Info = fileInfo;
        public FileBase(string filePath) : this(new FileInfo(filePath)) { }
    }
}
