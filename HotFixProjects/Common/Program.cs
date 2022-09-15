using System;
using System.Threading.Tasks;

namespace FileReaderWriter
{

    public class TestReadFileProcessor : ReadTextFileProcessor
    {
        protected override string OnProcessLine(string file, string line, int lineNumber)
        {
            //Console.WriteLine(line);
            if (line.Contains("Juese_Jianhun"))
                Console.WriteLine(file);
            return line;
        }

        protected override void OnStartProcessFile(string file)
        {
            Console.WriteLine(file);
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string path = "D:/Test2";
            Test();
            //var process = new TestReadFileProcessor();
            //process.Process(path, null);

            Console.WriteLine("done");
            Console.ReadLine();
        }

        static async void Test()
        {
            string path = "D:/Test2";
            var process = new TestReadFileProcessor();
            var task = process.ProcessAsync(path, null);
            Console.WriteLine("continue");
            await task;
            Console.WriteLine("complete");
        }
    }
}
