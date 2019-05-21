using System;

namespace Macrogenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Macrogenerator starts\n");
            var macroGenerator = new MacroGenerator();
            string test = "test1.txt";
            //if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            //{
                macroGenerator.ReadFromFile(test);
            //}
            //else
            //{
            //    MacroGenerator.PrintError(ErrorCode.NoInputFilePointed, -1);
            //}


            Console.WriteLine("\n\nMacrogenerator stoped");

            Console.WriteLine("Press Enter to end...");
            Console.ReadLine();

            //Console.WriteLine($"{args[0]}");
        }
    }
}
