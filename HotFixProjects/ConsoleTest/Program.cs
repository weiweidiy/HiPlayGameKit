using System;
using System.Threading.Tasks;

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

    public class TestReadFileProcessor : ReadTextFileProcessor
    {
        public override string GetReport()
        {
            return "report";
        }

        protected override string OnProcessLine(string file, string line, int lineNumber)
        {
            //Console.WriteLine(line);
            if (line.Contains("19588202b69df2c43a8254e6e10b8553"))
                Console.WriteLine(file);
            return line;
        }

        protected override void OnStartProcessFile(string file)
        {
            //Console.WriteLine(file);
        }
    }


    public class TestRepeatFileProcessor : RepeatFileProcessor
    {
        protected override void OnRepeatFile(string fileName, string path1, string path2)
        {
            Console.WriteLine(path1);
            Console.WriteLine(path2);
            Console.WriteLine("");
        }
    }

    class Program
    {
        static FileProcessor process = new TestRepeatFileProcessor();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Test();

            Console.ReadLine();
            process.Cancel();
            Console.WriteLine("done");
            Console.ReadLine();
        }

        static async void Test()
        {
            string path = "C:/Unity/Assets/Art/UI";
            var task = process.ProcessAsync(path, new UnityHinter());
            Console.WriteLine("continue");
            var report = await task;
            Console.WriteLine("complete " + report);
        }
    }
}
