using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Strategs
{
    class Team
    {
        public Color color;
        public List<int> units = new List<int>();
        public List<Structure> structures = new List<Structure>();
        public Controller ls;        
        public Team(Color c, Controller ls)
        {
            color = c;
            this.ls = ls;
        }

        public void AddStructure(Structure st)
        {
            structures.Add(st);
        }

        public void AddUnit(int index)
        {
            units.Add(index);
        }        
    }
}
