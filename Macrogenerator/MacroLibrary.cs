using System;
using System.Collections.Generic;
using System.Linq;

namespace Macrogenerator
{
    public class MacroLibrary
    {
        public List<Macro> MacrosList { get; set; }
        public MacroLibrary()
        {
            MacrosList = new List<Macro>();
        }

        public void AddMacro(Macro nMacro)
        {
            if (nMacro==null)
            {               
                return;
            }

            if(string.IsNullOrEmpty(nMacro.Name) || string.IsNullOrWhiteSpace(nMacro.Body))
            {
                MacroGenerator.PrintError(ErrorCode.AddedMacroIsEmpty, MacroGenerator.currentLineOfCode);
                return;
            }

            // Check if macro with given name and level already exists
            var lookForMacro = MacrosList.FirstOrDefault(m => m.Name.Equals(nMacro.Name) && m.Level.Equals(nMacro.Level));

            if(lookForMacro!=null)
            {
                // Print information about overwritting macro
                Console.WriteLine($"\nMacro \"{lookForMacro.Name}\" with level {lookForMacro.Level} has been overwritten");

                // Remove it from list;
                MacrosList.Remove(lookForMacro);
            }

            MacrosList.Add(nMacro);

            // Sort after insert(by level)
            MacrosList = MacrosList.OrderBy(d => d.Level).ToList();
        }

        public void RemoveMacro(string name, int level)
        {
            var macro = FindMacro(name, level);

            if(macro!=null)
            {
                MacrosList.Remove(macro);
            }
        }

        public Macro FindMacro(string name, int level = 0)
        {
            // find macro with given name and level equal or lower than given then take one
            var macro = MacrosList.OrderByDescending(n=>n.Level).FirstOrDefault(m => m.Name.Equals(name) && m.Level <= level);

            if (macro == null)
            {
                MacroGenerator.PrintError(ErrorCode.MacroNotFound, MacroGenerator.currentLineOfCode);
            }

            return macro;
        }
    }
}
