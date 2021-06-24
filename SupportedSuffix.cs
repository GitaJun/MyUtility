using System;
using System.Collections.Generic;

namespace JunUtility
{
    /// <summary>
    /// 支持的文件后缀类
    /// </summary>
    public class SupportedFileSuffix : IDisposable
    {
        private readonly SortedSet<string> supportedSuffix;

        /// <summary>
        /// 创建一个支持的文件后缀类对象
        /// </summary>
        /// <param name="lowercaseSuffixes">带点号小写的后缀名</param>
        public SupportedFileSuffix(params string[] lowercaseSuffixes)
        {
            supportedSuffix = new(lowercaseSuffixes);
            List<string> tempUpper = new();
            foreach (var item in supportedSuffix)
            {
                tempUpper.Add(item.ToUpper());
            }
            foreach (var item in tempUpper)
            {
                supportedSuffix.Add(item);
            }
        }

        /// <summary>
        /// 校验文件是否是支持的类型
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns>是否支持该类型的文件</returns>
        public bool CheckFileSuffix(string fileName)
        {
            int dotIndex = fileName.LastIndexOf('.');
            string suffix = fileName.Substring(dotIndex);
            return supportedSuffix.Contains(suffix);
        }

        public void Dispose()
        {
            supportedSuffix.Clear();
        }

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var item in supportedSuffix)
            {
                result += $"'{item}'";
            }
            return result;
        }
    }
}
