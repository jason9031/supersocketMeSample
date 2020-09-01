using IMESAgent.SocketClientEngine.FileLoaders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Globalization;
using AppClientSuperSocket;
using System.IO;
using System.Text.RegularExpressions;
using IMESServer.Common;

namespace IMESAgent.SocketClientEngine
{
    public static class FunctionExtension
    {
        public static void AddSegment(this IList<ArraySegment<byte>> ls, byte[] buffer, int offset, int size)
        {
            var segment = new ArraySegment<byte>(buffer, offset, size);

            ls.Add(segment);
        }

        public static CommonActions ToCommonAction(this string key)
        {
            int value = -1;
            if (int.TryParse(key, out value) == true)
            {
                return (CommonActions)value;
            }

            return CommonActions.None;
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
        public static bool IsCorrectFolderName(this string name)
        {
            string exp2 = @"^\d{4}-\d{2}-\d{2}$";
            return Regex.IsMatch(name, exp2);
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
                            return false;
                        }
                        return true;
                    }
                }
            }

            return false;
        }

        public static string ToCommonAction(this int key)
        {
            int value = -1;
            if (int.TryParse(key.ToString(), out value) == true)
            {
                return ((CommonActions)value).ToString();
            }

            return CommonActions.None.ToString();
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> list) where T : class
        {
            var tp = typeof(T);
            var table = new DataTable();
            var pInfos = tp.GetProperties();

            Array.ForEach<PropertyInfo>(pInfos, p =>
            {
                string nickName = p.GetNickname<NameAttribute>();
                if (!string.IsNullOrEmpty(nickName))
                    table.Columns.Add(nickName);
            });

            foreach (var item in list)
            {
                var row = table.NewRow();
                Array.ForEach<PropertyInfo>(pInfos, p =>
                {
                    string nickName = p.GetNickname<NameAttribute>();
                    if (!string.IsNullOrEmpty(nickName))
                        row[nickName] = p.GetValue(item, null);
                });

                table.Rows.Add(row);
            }

            return table;
        }

        public static List<T> ToList<T>(this DataTable table) where T : class, new()
        {
            var tp = typeof(T);
            var lst = new List<T>();
            var pInfos = tp.GetProperties();

            foreach (DataRow row in table.Rows)
            {
                T obj = new T();
                Array.ForEach<PropertyInfo>(pInfos, p =>
                {
                    string nickName = p.GetNickname<NameAttribute>();
                    if (table.Columns.Contains(nickName) && row[nickName] != DBNull.Value)
                        p.SetValue(obj, row[nickName], null);
                });

                lst.Add(obj);
            }

            return lst;
        }

        private static bool Validate(DataTable table, string nickName)
        {
            if (string.IsNullOrEmpty(nickName)) return false;
            if (table.Columns.Contains(nickName)) return false;

            return true;
        }

        public static string GetNickname<T>(this PropertyInfo pInfo) where T : NameAttribute
        {
            var attribute = pInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();

            if (attribute == null) return string.Empty;
            return ((T)attribute).Name;
        }

        public static int ToInt(this string str)
        {
            int value = -1;
            if (string.IsNullOrEmpty(str)) return value;
            if (int.TryParse(str, out value)) return value;
            return value;
        }

        public static IEnumerable<string> ToParserGroupList<T>(this IEnumerable<T> list) where T : ParserTypeInfo
        {
            var ls = list.Select<T, string>(p => p.ParseGroupName);

            return ls.Distinct<string>();
        }

        public static IEnumerable<string> ToParserTypeList<T>(this IEnumerable<T> list, string item) where T : ParserTypeInfo
        {
            return list.Where<T>(p => p.ParseGroupName == item).Select(s => s.ParseTypeName);
        }

        public static T GetParserInfo<T>(this IEnumerable<T> list, string group, string type) where T : ParserTypeInfo
        {
            return list.Where<T>(p => p.ParseGroupName == group && p.ParseTypeName == type).FirstOrDefault();
        }

        public static DateTime ToDatetime(this string stDate, string pattern)
        {
            return DateTime.ParseExact(stDate, pattern, System.Globalization.CultureInfo.CurrentCulture);
        }

        public static long ToLong(this string value)
        {
            long lv = -1;

            if (!string.IsNullOrEmpty(value))
            {
                if (long.TryParse(value, out lv))
                    return lv;
            }

            return lv;
        }
    }
}
