namespace FileReaderWriter
{
    public abstract class FileProcessor
    {
        public void Process(string path, FileFilter filter)
        {
            var files = FileUtils.GetFiles(path);

            foreach (var file in files)
            {
                if (filter != null && filter.Hint(file))
                {
                    continue;
                }

                OnProcessFile(file);
            }
        }

        protected abstract void OnProcessFile(string file);
    }

}
