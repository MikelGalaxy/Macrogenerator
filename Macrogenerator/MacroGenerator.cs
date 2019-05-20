using System;
using System.Collections.Generic;
using System.Text;

namespace Macrogenerator
{
    public class MacroGenerator
    {
        private MacroLibrary macroLibrary;
        private int currentLevel;
        public static int currentLineOfCode = 0;
        //public void RunTests()
        //{

        //}

        public void DisplayHelp()
        {

        }

        public void ReadFromFile(string filepath)
        {

        }

        private bool CheckFile(string filepath)
        {

            return true;
        }



        public static void PrintError(ErrorCode code, int lineOfFile)
        {


            Console.WriteLine($"ERROR - {code}");
            if (lineOfFile >= 0)
            {
                Console.WriteLine($"In line number {lineOfFile}");
            }
        }
    }
}
