using SubSet.Models;
using SubSet.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SubSet.Controllers
{
    public static class NameProcessor
    {
        private const int DIGIT_GRP = 1, TEXT_GRP = 2;

        public static void SetEpizodes(IList<FileSerial> list)
        {
            
            // get a list of matches where every match is split into 2 capture groups:
            // first group for nummbers and second group
            for (int i = 0; i < list.Count; i++)
            {
                var match = Regex.Match(list[i].PureName, @"([^\d]+)(\d+)*");
                var digitGroup = match.Groups[DIGIT_GRP];
                var textGroup = match.Groups[TEXT_GRP];

                if (digitGroup.Success)
                {
                    if (digitGroup.Captures.Count == 1)
                    {
                        list[i].Epizode = int.Parse(digitGroup.Value);
                        continue;
                    }
                }

                var thisRestJoined = String.Join("", textGroup.Captures);
                // TODO: finish implementing the rest...

            }


            //var digitsList = list.Select(file => Regex.Matches(file.PureName, @"\d+").ToIEnumerable().ToList()).ToList();
            //var restList = list.Select(file => Regex.Matches(file.PureName, @"[^\d]+").ToIEnumerable().ToList()).ToList();
            //for (int i = 0; i < digitsList.Count; i++)
            //{
            //    if (digitsList[i].Count == 1)
            //    {
            //        list[i].Epizode = int.Parse(digitsList[i][0].Value);
            //        continue;
            //    }

            //    var thisRestJoined = restList[i].JoinMatchsValue();
            //    var moshabehat = restList.Where(r => r.JoinMatchsValue() == thisRestJoined).
            //        ToDictionary(m => restList.IndexOf(m));
            //    var trueMoshabehat = moshabehat.Where(m =>
            //    {
            //        if (digitsList[m.Key].Count != digitsList[i].Count) return false;
            //        int tCount = 0;
            //        for (int j = 0; j < digitsList[i].Count; j++)
            //        {
            //            if (digitsList[m.Key][j].Value != digitsList[i][j].Value) tCount++;
            //        }
            //        return m.Key != i && tCount == 1;
            //    }).ToDictionary(m => m.Key, m => m.Value);

            //    if (trueMoshabehat.Count > 0)
            //    {
            //        for (int j = 0; j < digitsList[i].Count; j++)
            //        {
            //            if (digitsList[i][j] != digitsList[trueMoshabehat.First().Key][j])
            //            {
            //                list[i].Epizode = int.Parse(digitsList[i][j].Value);
            //                break;
            //            }
            //        }
            //    }
            //    else
            //    {

            //    }
            //}
        }
        public static void SetEpizodes(IList<FileSubtitle> list)
        {
            SetEpizodes(list.Select<FileSubtitle, FileSerial>(f => f).ToList());
        }
        public static void SetEpizodes(IList<FileMovie> list)
        {
            SetEpizodes(list.Select<FileMovie, FileSerial>(f => f).ToList());
        }

        static string JoinMatchsValue(this MatchCollection matchCollection)
        {
            string result = "";
            for (int i = 0; i < matchCollection.Count; i++)
            {
                result += matchCollection[i].Value;
            }
            return result;
        }
        static string JoinMatchsValue(this IEnumerable<Match> matchCollection)
        {
            string result = "";
            foreach (var match in matchCollection)
            {
                result += match.Value;
            }
            return result;
        }

        static IEnumerable<Match> ToIEnumerable(this MatchCollection matchCollection)
        {
            for (int i = 0; i < matchCollection.Count; i++)
            {
                yield return matchCollection[i];
            }
        }
    }
}
