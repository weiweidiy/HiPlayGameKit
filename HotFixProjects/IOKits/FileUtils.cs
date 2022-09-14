using System.Collections.Generic;
using System.IO;

namespace FileReaderWriter
{
    public class FileUtils
    {
        /// <summary>
        /// 获取指定目录以及子目录下所有文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string[] GetFiles(string path)
        {

            List<string> result = new List<string>();
            if (File.Exists(path))
            {
                result.Add(path);
                return result.ToArray();
            }

            DirectoryInfo info = new DirectoryInfo(path);
            FileSystemInfo[] sys = info.GetFileSystemInfos();
            foreach (var i in sys)
            {
                if (i is DirectoryInfo)
                {
                    result.AddRange(GetFiles(i.FullName));
                }
                else
                {
                    result.Add(i.FullName);
                }
            }

            return result.ToArray();
        }

        /// <summary>
        /// 获取指定文件文本内容
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static string GetText(string file)
        {
            string text = "";
            using (var sr = new StreamReader(file))
            {
                int lineNumber = 0;
                while (!sr.EndOfStream/*sr.Peek() >= 0*/)
                {
                    lineNumber++;
                    string line = sr.ReadLine();
                    text += line + "\n";
                }
            }
            return text;
        }

        /// <summary>
        /// 写入文本内容
        /// </summary>
        /// <param name="file"></param>
        /// <param name="text"></param>
        public static void WriteText(string file, string text)
        {
            string[] lines = text.Split("/n");
            using (var sw = new StreamWriter(file))
            {
                foreach (var line in lines)
                {
                    sw.WriteLine(line);
                }
            }
        }
    }
}
