﻿using System;
using System.IO;

namespace Macrogenerator
{
    /*
          
    Project task no. 11:
    “Write a macrogenerator with no parameters but definitions can be nested. 
    The discriminant of definition is &.
    &CC means start of definition of macro CC.
    &CC TEXT - possible definitions of macro &
    Macrocall: $CC“

     */

    public class MacroGenerator
    {
        private MacroLibrary macroLibrary;
        private int currentLevel = 0;
        public static int currentLineOfCode = 1;
        string filepath;
        string outputFilepath;

        public MacroGenerator()
        {
            macroLibrary = new MacroLibrary();
        }

        public void ReadFromFile(string filename)
        {
            string outputName = $"output_{filename}";
            filepath = Path.Combine(Environment.CurrentDirectory, filename);
            outputFilepath = Path.Combine(Environment.CurrentDirectory, outputName);

            Console.WriteLine($"Trying to read from {filepath}\n");

            if (CheckFile(filepath))
            {
                // Delete old output file
                File.Delete(outputFilepath);

                using (StreamReader sr = new StreamReader(filepath))
                {
                    while (sr.Peek() >= 0)
                    {
                        string line = sr.ReadLine();

                        if (isComment(line) == false)
                        {
                            InterpretLine(line);
                        }

                        currentLineOfCode++;
                    }
                }
            }
        }

        /// <summary>
        /// Check if line started with // meaning commented line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool isComment(string line)
        {
            if (line.Length >= 2 && line[0] == '/')
            {
                if (line[1] == '/')
                {
                    return true;
                }
            }

            return false;
        }

        private void InterpretLine(string line)
        {
            bool callStarted = false;
            string tempCallName = string.Empty;
            string output = string.Empty;

            for (int i = 0; i < line.Length; i++)
            {
                if (callStarted == false)
                {
                    if (line[i] == '$')
                    {
                        callStarted = true;
                        WriteToFile(output);
                        output = string.Empty;
                        currentLevel++;
                    }
                    else if (line[i] == '&')
                    {
                        i += VerifyAndAddMacro(line.Substring(i + 1)) + 1;
                    }
                    else
                    {
                        if (line[i] == ';')
                        {
                            // in that case it isn't treated as free text - line is cleared out;
                            PrintError(ErrorCode.InvalidSemiCollonPosition, currentLineOfCode);
                            output = string.Empty;
                        }
                        else
                        {
                            output += line[i];
                        }

                    }
                }
                else if (callStarted == true)   // reading macrocall;
                {
                    if (line[i] == ';' && string.IsNullOrEmpty(tempCallName))
                    {
                        PrintError(ErrorCode.NoNameInMacrocall, currentLineOfCode);
                        callStarted = false;
                        currentLevel--;
                    }
                    else if (line[i] == ';')
                    {
                        ExecuteMacrocall(tempCallName, currentLevel);
                        tempCallName = string.Empty;
                        callStarted = false;
                        currentLevel--;
                    }
                    else
                    {
                        tempCallName += line[i];
                    }
                }
            }

            if (callStarted == true)
            {
                PrintError(ErrorCode.MacrocallNotFinished, currentLineOfCode);
            }

            WriteToFile(output);
            //Console.Write(output);        //stream that goes to writter
        }

        /// <summary>
        /// Function used when & occur
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private int VerifyAndAddMacro(string line)
        {
            bool macroBodyStarted = false;
            string tempMacroName = string.Empty;
            string tempMacroBody = string.Empty;

            int macrosCount = 1;

            int i = 0;

            for (; i < line.Length; i++)
            {
                if (line[i] == '&' || line[i] == '$')
                {
                    macrosCount++;
                }
                if (line[i] == ';')
                {
                    macrosCount--;

                    if (macrosCount < 0)
                    {
                        PrintError(ErrorCode.IncorectAmmountOfSemiCollons, currentLineOfCode);
                    }
                }
                if (line[i] == ';' && macrosCount == 0 && string.IsNullOrEmpty(tempMacroName))
                {
                    PrintError(ErrorCode.NoNameInMacrocall, currentLineOfCode);
                    macroBodyStarted = false;
                }
                else if (line[i] != ' ' && macroBodyStarted == false)
                {
                    tempMacroName += line[i];
                }
                else if (line[i] == ' ' && macroBodyStarted == false)
                {
                    macroBodyStarted = true;
                }
                else if (line[i] == ';' && string.IsNullOrEmpty(tempMacroBody))
                {
                    PrintError(ErrorCode.NoNameInMacrocall, currentLineOfCode);
                    macroBodyStarted = false;
                }
                else if (line[i] == ';' && macrosCount == 0 && macroBodyStarted == true)
                {
                    macroBodyStarted = false;

                    var nMacro = new Macro
                    {
                        Name = tempMacroName,
                        Level = currentLevel,
                        Body = tempMacroBody
                    };

                    macroLibrary.AddMacro(nMacro);
                    break;

                }
                else if (macroBodyStarted == true)
                {
                    tempMacroBody += line[i];
                }
            }

            return i;
        }

        private void ExecuteMacrocall(string macroName, int level)
        {
            var macro = macroLibrary.FindMacro(macroName, level);
            if (macro != null)
            {
                InterpretLine(macro.Body);
            }
        }

        private bool CheckFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                try
                {
                    var z = File.OpenRead(filepath);
                    z.Close();
                }
                catch
                {
                    PrintError(ErrorCode.CantReadFile, -1);
                    return false;

                }
                return true;
            }

            PrintError(ErrorCode.CantFindFile, -1);
            return false;
        }

        public static void PrintError(ErrorCode code, int lineOfFile)
        {
            Console.WriteLine($"\nERROR - {code}");
            if (lineOfFile >= 0)
            {
                Console.WriteLine($"In line number {lineOfFile}");
            }
        }

        private void WriteToFile(string output)
        {
            using (StreamWriter sw = new StreamWriter(outputFilepath, true))
            {
                if (!string.IsNullOrWhiteSpace(output))
                {
                    sw.Write(output);
                }
            }
        }

        // basic information about macrogenerator (after using -h as parameter)
        public void DisplayHelp()
        {
            Console.WriteLine("Help");
            Console.WriteLine("Language syntax:");
            Console.WriteLine("Special delimiters: &, $, ' ', ;");
            Console.WriteLine("Macro definitions will begin with a '&' character followed by a space character and closed with ';' character.");
            Console.WriteLine("Macrocall starts with '$' ending with a ';' character.\n");
            Console.WriteLine("\tBODY : <CALL> | <DEF>");
            Console.WriteLine("\tDEF: &<MARCO_NAME>' '<BODY>;\n");
            Console.WriteLine("\tCALL: $<MACRO_NAME>; No parameters allowed");
            Console.WriteLine("Lines starting with // are treated by program as comments and therefore neglected\n");
            Console.WriteLine("Tests can be find in file EOPSY_TESTS.txt");
        }
    }
}
