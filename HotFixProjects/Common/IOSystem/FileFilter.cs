namespace FileReaderWriter
{
    public abstract class FileFilter
    {
        public abstract bool Filter(string file);
    }

    public abstract class FileHinter
    {
        public abstract bool Hint(string file);
    }
}
