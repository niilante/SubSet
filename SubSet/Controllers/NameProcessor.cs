using SubSet.Models;
using SubSet.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Math;

namespace SubSet.Controllers
{
    public static class NameProcessor
    {
        public static void SetEpizodes(IList<FileSerial> list)
        {
            var digitsList = list.Select(file => Regex.Matches(file.PureName, @"\d+").ToIEnumerable().ToList()).ToList();
            var restList = list.Select(file => Regex.Matches(file.PureName, @"[^\d]+").ToIEnumerable().ToList()).ToList();
            for (int i = 0; i < digitsList.Count; i++)
            {
                if (digitsList[i].Count == 1)
                {
                    list[i].Epizode = int.Parse(digitsList[i][0].Value);
                    continue;
                }

                var thisRestJoined = restList[i].JoinMatchsValue();
                var moshabehat = restList.Where(r => r.JoinMatchsValue() == thisRestJoined).
                    ToDictionary(m => restList.IndexOf(m));
                var trueMoshabehat = moshabehat.Where(m =>
                {
                    if (digitsList[m.Key].Count != digitsList[i].Count) return false;
                    int tCount = 0;
                    for (int j = 0; j < digitsList[i].Count; j++)
                    {
                        if (digitsList[m.Key][j].Value != digitsList[i][j].Value) tCount++;
                    }
                    return m.Key != i && tCount == 1;
                }).ToDictionary(m => m.Key, m => m.Value);

                if (trueMoshabehat.Count > 0)
                {
                    for (int j = 0; j < digitsList[i].Count; j++)
                    {
                        if (digitsList[i][j] != digitsList[trueMoshabehat.First().Key][j])
                        {
                            list[i].Epizode = int.Parse(digitsList[i][j].Value);
                            break;
                        }
                    }
                }
                else
                {

                }
            }
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
