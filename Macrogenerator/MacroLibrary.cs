using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Macrogenerator
{
    public class MacroLibrary
    {
        public List<Macro> MacrosList { get; set; }

        public void AddMacro(Macro nMacro)
        {
            if(nMacro==null)
            {
                MacroGenerator.PrintError(ErrorCode.AddedMacroIsNull, MacroGenerator.currentLineOfCode);
                return;
            }

            // Check if macro with given name and level already exists
            var lookForMacro = MacrosList.First(m => m.Name.Equals(nMacro.Name) && m.Level.Equals(nMacro.Level));

            if(lookForMacro!=null)
            {
                // Print information about overwritting macro


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
            var macro = MacrosList.First(m => m.Name.Equals(name) && m.Level.Equals(level));

            if (macro == null)
            {
                MacroGenerator.PrintError(ErrorCode.MacroNotFound, MacroGenerator.currentLineOfCode);
            }

            return macro;
        }
    }
}
