using System;
using System.Threading.Tasks;

namespace FileReaderWriter
{

    public class TestReadFileProcessor : ReadTextFileProcessor
    {
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
    class Program
    {
        static RepeatFileProcessor process = new RepeatFileProcessor();
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
            await task;
            Console.WriteLine("complete");
        }
    }
}
