using System;

namespace Macrogenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Macrogenerator starts\n");
            var macroGenerator = new MacroGenerator();

            if (args.Length > 0 && !string.IsNullOrEmpty(args[0]))
            {
                if (args[0] == "-h")
                {
                    macroGenerator.DisplayHelp();
                }
                else
                {

                    macroGenerator.ReadFromFile(args[0]);
                }
                
            }
            else
            {
                MacroGenerator.PrintError(ErrorCode.NoInputFilePointed, -1);
            }

            Console.WriteLine("\n\nMacrogenerator stoped");

            Console.WriteLine("Press Enter to end...");
            Console.ReadLine();

        }
    }
}
