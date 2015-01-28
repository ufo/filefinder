using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace NppFileSearch
{
    class FileMaskMatcher
    {
        public enum MatchType
        {
            FileName,
            Directory,
            FullPath
        }

        List<Regex> strictMasks;
        List<Regex> greedyMasks;
        public FileMaskMatcher(List<string> fileMaskList)
        {
            strictMasks = new List<Regex>();
            greedyMasks = new List<Regex>();
            foreach (string mask in fileMaskList)
            {
                string convertedMask = Environment.ExpandEnvironmentVariables(mask);
                convertedMask = Regex.Escape(convertedMask).Replace("\\*", ".*").Replace("\\?", ".");
                strictMasks.Add(new Regex("^" + convertedMask + "$", RegexOptions.IgnoreCase));
                greedyMasks.Add(new Regex(convertedMask, RegexOptions.IgnoreCase));
            }
        }
        public bool IsMatch(string name, MatchType matchType)
        {
            if (matchType == MatchType.FileName)
            {
                foreach (Regex fileMask in strictMasks)
                {
                    if (fileMask.IsMatch(name))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.Directory)
            {
                foreach (Regex fileMask in greedyMasks)
                {
                    if (fileMask.IsMatch(name))
                    {
                        return true;
                    }
                }
            }
            else if (matchType == MatchType.FullPath)
            {
                if (IsMatch(Path.GetFileName(name), MatchType.FileName) ||
                    IsMatch(Path.GetDirectoryName(name), MatchType.Directory))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
