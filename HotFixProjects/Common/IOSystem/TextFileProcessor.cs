using System;
using System.Threading.Tasks;

namespace FileReaderWriter
{
    public abstract class TextFileProcessor
    {
        public float Progress { get; private set; }

        public async void Process(string path, FileFilter filter)
        {
            await ProcessAsync(path, filter);
        }


        public async Task ProcessAsync(string path, FileFilter filter)
        {
            var task = Task.Factory.StartNew(() =>
            {
                var files = FileUtils.GetFiles(path);
                var maxCount = files.Length;

                for (int i = 0; i < files.Length; i++)
                {
                    Progress = i / (float)maxCount;
                    var file = files[i];
                    if (filter != null && filter.Hint(file))
                    {
                        continue;
                    }
                    OnStartProcessFile(file);
                    OnProcessFile(file);
                    OnEndProcessFile(file);
                }
            });
            await task;
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="file"></param>
        protected virtual void OnStartProcessFile(string file) { }

        /// <summary>
        /// 处理方法
        /// </summary>
        /// <param name="file"></param>
        protected abstract void OnProcessFile(string file);

        /// <summary>
        /// 处理结束
        /// </summary>
        /// <param name="file"></param>
        protected virtual void OnEndProcessFile(string file) { }
    }

}
