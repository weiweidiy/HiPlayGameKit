using System;

namespace FileReaderWriter
{
    public class UnityFilter : FileFilter
    {
        public override bool Filter(string file)
        {
            return file.EndsWith(".meta", StringComparison.Ordinal);
        }
    }

    public class UnityHinter : FileHinter
    {
        public override bool Hint(string file)
        {
            return file.EndsWith(".png", StringComparison.Ordinal);
        }
    }
}
