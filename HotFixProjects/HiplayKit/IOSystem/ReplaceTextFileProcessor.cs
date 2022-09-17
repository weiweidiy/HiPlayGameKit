using System.IO;
using System.Threading.Tasks;

namespace FileReaderWriter
{
    public abstract class ReplaceTextFileProcessor : FileProcessor
    {
        protected override void OnProcessFile(string file)
        {
            var tempOutput = "tempOutput";

            if (File.Exists(tempOutput))
                File.Delete(tempOutput);

            using (var sw = new StreamWriter(tempOutput))
            {
                using (var fs = File.OpenRead(file))
                using (var sr = new StreamReader(fs))
                {
                    int lineCount = 0;
                    string line, newLine;
                    while (!sr.EndOfStream)
                    {
                        lineCount++;
                        line = sr.ReadLine();
                        newLine = OnProcessLine(file, line, lineCount);
                        sw.WriteLine(newLine);
                    }
                }
            }

            File.Replace(tempOutput, file, null);
        }

        protected abstract string OnProcessLine(string file, string line, int lineNumber);

    }
}
