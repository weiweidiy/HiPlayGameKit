using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileReaderWriter
{

    public abstract class FileProcessor
    {
        /// <summary>
        /// 处理进度
        /// </summary>
        public float Progress { get; private set; }


        CancellationTokenSource tokenSource = new CancellationTokenSource();

        /// <summary>
        /// 文件筛选器
        /// </summary>
        FileFilter fileFilter = null;

        /// <summary>
        /// 文件选中器
        /// </summary>
        FileHinter fileHinter = null;

        /// <summary>
        /// 取消
        /// </summary>
        public void Cancel()
        {
            tokenSource.Cancel();
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filter"></param>
        public async void Process(string path, FileFilter filter)
        {
            await ProcessAsync(path, filter);
        }

        public async Task<string> ProcessAsync(string path, FileFilter filter)
        {
            fileFilter = filter;
            return await ProcessAsync(path);
        }

        public async void Process(string path, FileHinter hinter)
        {
            await ProcessAsync(path, hinter);
        }

        public async Task<string> ProcessAsync(string path, FileHinter hinter)
        {
            fileHinter = hinter;
            return await ProcessAsync(path);
        }

        /// <summary>
        /// 是否命中
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        bool Hint(string file)
        {
            return (fileHinter != null && fileHinter.Hint(file)) || (fileFilter != null && !fileFilter.Filter(file));
        }

        /// <summary>
        /// 开始处理
        /// </summary>
        /// <param name="path"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<string> ProcessAsync(string path)
        {

            var task = Task<string>.Factory.StartNew(() =>
            {
                var files = FileUtils.GetFiles(path);
                var maxCount = files.Length;

                for (int i = 0; i < files.Length; i++)
                {
                    if (tokenSource.IsCancellationRequested)
                    {
                        Console.WriteLine("task has canceled");
                        break;
                    }

                    Progress = i / (float)maxCount;
                    var file = files[i];

                    if (!Hint(file))
                        continue;

                    OnStartProcessFile(file);
                    OnProcessFile(file);
                    OnEndProcessFile(file);
                }

                return GetReport();

            }, tokenSource.Token);

            return await task;
        }

        /// <summary>
        /// 获取处理报告
        /// </summary>
        /// <returns></returns>
        public virtual string GetReport() { return ""; }

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
