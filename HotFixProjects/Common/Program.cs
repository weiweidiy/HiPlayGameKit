using System;

namespace FileReaderWriter
{

    public class TestReadFileProcessor : ReadTextFileProcessor
    {
        protected override string OnProcessLine(string file, string line, int lineNumber)
        {
            if (line.Contains("0cbf0f96efed0c141a0e992814b9c71d"))
                Console.WriteLine(file);
            return line;
        }
    }
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string path = "C:/Unity/Assets";
            var process = new TestReadFileProcessor();
            process.Process(path, null);

            Console.WriteLine("done");
        }
    }
}
