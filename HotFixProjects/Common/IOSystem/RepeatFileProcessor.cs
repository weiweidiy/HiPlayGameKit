
using System;
using System.Collections.Generic;
using System.IO;

namespace FileReaderWriter
{
    public class RepeatFileProcessor : FileProcessor
    {
        Dictionary<string, string> container = new Dictionary<string, string>();

        protected override void OnProcessFile(string file)
        {
            string fileName = Path.GetFileName(file);
            if (container.ContainsKey(fileName))
            {
                Console.WriteLine(file);
                Console.WriteLine(container[fileName]);
            }
            else
            {
                container.Add(fileName, file);
            }
        }
    }

}
