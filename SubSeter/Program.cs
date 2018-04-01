using System;
using System.IO;

namespace SubSeter
{
    class Program
    {
        static void Main(string[] args)
        {
            Environment.CurrentDirectory = Directory.GetParent(Environment.GetCommandLineArgs()[0]).FullName;
            var subset = new SubSet.SubSet(args[0]);
            subset.SetSubtitles().Wait();
        }
    }
}
