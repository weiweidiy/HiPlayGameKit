using System;
using System.IO;
using System.Threading.Tasks;

namespace FileReaderWriter
{
    /// <summary>
    /// 文件处理类
    /// </summary>
    public abstract class ReadTextFileProcessor : TextFileProcessor
    {
        protected override void OnProcessFile(string file)
        {
            using (var sr = new StreamReader(file))
            {
                int lineNumber = 0;
                while (!sr.EndOfStream/*sr.Peek() >= 0*/)
                {
                    lineNumber++;
                    string line = sr.ReadLine();
                    OnProcessLine(file, line, lineNumber);
                }
            }
        }

        protected abstract string OnProcessLine(string file, string line, int lineNumber);
    }
}
