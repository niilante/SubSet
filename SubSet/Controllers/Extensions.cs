using Newtonsoft.Json;
using SubSet.Models.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SubSet.Controllers
{
    class Extensions
    {
        public static string[] SubExtensions { get; protected set; }
        public static string[] MovieExtensions { get; protected set; }
        public static string[] FontExtensions { get; protected set; }

        static string JsonDir(ExtensionType type)
        {
            return Path.Combine("Ref", type + "Extensions.json");
        }
        static string JsonContent(ExtensionType type)
        {
            var x = JsonDir(type);
            return File.ReadAllText(JsonDir(type));
        }

        static Extensions()
        {
            SubExtensions = JsonConvert.DeserializeObject<string[]>(JsonContent(ExtensionType.Sub));
            MovieExtensions = JsonConvert.DeserializeObject<string[]>(JsonContent(ExtensionType.Movie));
            FontExtensions = JsonConvert.DeserializeObject<string[]>(JsonContent(ExtensionType.Font));
        }

        public static void RunStatic()
        {

        }
    }
}
