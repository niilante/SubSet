using SubSet.Models;
using SubSet.Models.Enums;
using SubSet.Controllers;
using static SubSet.Controllers.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using static SubSet.Controllers.NameProcessor;

namespace SubSet
{
    public class SubSet
    {
        public Setting Setting { get; set; }
        protected DirectoryInfo Directory { get; set; }
        public SubSetStatus Status { get; protected set; }
        public string StatusMessage { get; protected set; }
        public byte StatusPercent { get; protected set; }

        protected List<FileMovie> Movies { get; set; }
        protected List<FileSubtitle> Subs { get; set; }
        protected List<FileFont> Fonts { get; set; }


        public SubSet(string directory)
        {
            Movies = new List<FileMovie>();
            Subs = new List<FileSubtitle>();
            Fonts = new List<FileFont>();

            Setting = new Setting();
            Directory = new DirectoryInfo(directory);
            Status = SubSetStatus.Idle;
            StatusMessage = "";
            StatusPercent = 0;
        }

        public async Task SetSubtitles()
        {
            if (Status != SubSetStatus.Idle)
                throw new Exception();
            await Task.Run(() =>
            {
                Status = SubSetStatus.Analize;
                StatusMessage = "Analizing";
                var allFiles = Directory.GetFiles("*.*", SearchOption.AllDirectories);

                RunStatic();

                foreach (var file in allFiles)
                {
                    if (MovieExtensions.Any(e => e == file.Extension))
                        Movies.Add(new FileMovie(file));
                    if (SubExtensions.Any(e => e == file.Extension))
                        Subs.Add(new FileSubtitle(file));
                    if (FontExtensions.Any(e => e == file.Extension))
                        Fonts.Add(new FileFont(file));
                }

                if (!Setting.SearchMovieInSubdir)
                    Movies = Movies.Where(movie => movie.DirectoryPath == Directory.FullName).ToList();
                if (!Setting.SearchSubInSubdir)
                    Subs = Subs.Where(sub => sub.DirectoryPath == Directory.FullName).ToList();

                StatusPercent = 10;

                foreach (var fontFile in Fonts)
                {
                    fontFile.Install();
                }
                StatusPercent = 20;
                SetEpizodes(Subs);
                StatusPercent = 50;
                SetEpizodes(Movies);
                StatusPercent = 80;
                foreach (var sub in Subs)
                {
                    sub.Movie = Movies.FirstOrDefault(m => m.Epizode == sub.Epizode);
                }
                StatusPercent = 90;
                StatusPercent = 0;
                Status = SubSetStatus.SettingSub;
                StatusMessage = "RenamingSubs";
                for (int i = 0; i < Subs.Count; i++)
                {
                    var sub = Subs[i];
                    if (sub.Movie == null) continue;
                    var subPath = Path.Combine(sub.Movie.DirectoryPath, sub.Movie.PureName + sub.Extension);
                    if (File.Exists(subPath))
                        continue;
                    sub.Info.MoveTo(subPath);
                    StatusPercent = (byte)(i * 100 / Subs.Count);
                }
                Status = SubSetStatus.Completed;
                StatusPercent = 100;
                StatusMessage = "Completed";
            });
        }
    }
}
