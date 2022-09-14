using System;

namespace FileReaderWriter
{
    public class UnityFilter : FileFilter
    {
        public override bool Hint(string file)
        {
            return file.EndsWith(".meta", StringComparison.Ordinal);
        }
    }
}
