using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IMESServer.Common
{
    public static class FileExtension
    {
        private const string Info = "info";
        private const string Debug = "debug";
        private const string Err = "err";
        private const string Perf = "perf";
        private const string Parser = "parser";

        public static bool IsCorrectFileName(this string fileName)
        {
            string exp = string.Format("^({0}|{1}|{2}|{3}|{4})", Info, Debug, Err, Perf, Parser);
            exp += @"(\d{8})?\.log$";

            return Regex.IsMatch(fileName, exp);
        }

        public static bool IsCorrectFolderName(this string name)
        {
            string exp2 = @"^\d{4}-\d{2}-\d{2}$";
            return Regex.IsMatch(name, exp2);
        }

        public static bool IsExpired(this string path, int dayOfYear, int expiredTime)
        {
            int today = DateTime.Now.DayOfYear;

            int gap = today - dayOfYear;

            if (gap >= 0)
            {
                if (gap > expiredTime)
                {
                    return true;
                }
            }
            else
            {
                if (gap + 365 > expiredTime)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool DeleteExpiredFile(this string path, int expiredTime)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path))
            {
                FileInfo info = new FileInfo(path);

                if (info != null)
                {
                    if (!info.Name.IsCorrectFileName() || path.IsExpired(info.LastWriteTime.DayOfYear, expiredTime)) 
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch
                        {
                            return false ;
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool DeleteExpiredFolder(this string path, int expiredTime)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo info = new DirectoryInfo(path);

                if (info != null)
                {
                    if (!info.Name.IsCorrectFolderName() || path.IsExpired(info.LastWriteTime.DayOfYear, expiredTime))
                    {
                        try
                        {
                            Directory.Delete(path, true);
                        }
                        catch
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
