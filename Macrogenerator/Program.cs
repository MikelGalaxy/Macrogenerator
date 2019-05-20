using System;

namespace Macrogenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Macrogenerator starts");
            var macroGenerator = new MacroGenerator();
            if(args.Length > 0 &&string.IsNullOrEmpty(args[0]))
            {

            }
            else
            {
                MacroGenerator.PrintError(ErrorCode.NoInputFilePointed, -1);
            }


            Console.WriteLine("Macrogenerator stoped");
            Console.ReadLine();

            //Console.WriteLine($"{args[0]}");
        }
    }
}
