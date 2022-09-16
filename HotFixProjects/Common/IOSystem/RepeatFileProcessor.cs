
using System;
using System.Collections.Generic;
using System.IO;

namespace FileReaderWriter
{
    public abstract class RepeatFileProcessor : FileProcessor
    {
        Dictionary<string, string> container = new Dictionary<string, string>();

        protected override void OnProcessFile(string file)
        {
            string fileName = Path.GetFileName(file);
            if (container.ContainsKey(fileName))
            {
                OnRepeatFile(fileName, container[fileName], file);
            }
            else
            {
                container.Add(fileName, file);
            }
        }

        protected abstract void OnRepeatFile(string fileName, string path1, string path2);
    }

}
