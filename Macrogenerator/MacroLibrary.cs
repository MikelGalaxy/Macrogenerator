using System;
using System.Collections.Generic;
using System.Text;

namespace Macrogenerator
{
    public class MacroLibrary
    {
        public List<Macro> MacrosList { get; set; }

        public void AddMacro(Macro nMacro)
        {

        }

        public void RemoveMacro(string name,int level)
        {

        }

        public Macro FindMacro(string name, int level = 0)
        {
            return new Macro();
        }
    }
}
