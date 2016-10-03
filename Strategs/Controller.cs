using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Strategs
{
    class Controller
    {
        public List<Team> teams = new List<Team>();        
        public Dictionary<int,Unit> units = new Dictionary<int,Unit>();
        public Dictionary<int, List<int>> sortedUnits = new Dictionary<int, List<int>>();
        public List<int> selectedUnits = new List<int>();

        public float stepX = 10;        
        public readonly int mapWidth;
        private int unitsCount = 0;

        public Controller(int mapWidth)
        {
            this.mapWidth = mapWidth;
            for (float i = 0; i < mapWidth; i += stepX)
                sortedUnits.Add((int)(i / stepX), new List<int>());
        }

        public Team CreateTeam(Color c)
        {          
            Team t = new Team(c, this);
            teams.Add(t);
            return t;
        }

        public void CreateUnit(Team t, Unit u)
        {
            u.Selected += Unit_Selected;

            units[unitsCount] = u;
            u.id = unitsCount;            
            t.units.Add(unitsCount);
            unitsCount++;
            
            AddToSortedDictionary(u);
        }

        private void AddToSortedDictionary(Unit u)
        {
            int indexX = (int)Math.Floor(u.position.x / stepX);
            List<int> column = sortedUnits[indexX];
            int indexY = 0;            
            foreach (int i in column)
            {
                if (u.position.y < units[i].position.y) break;
                indexY++;
            }
            column.Insert(indexY, u.id);
            u.sortedPos = new PointI(indexX, indexY);
        }

        public void SelectColumn(int index)
        {
            ClearSelectedList();
            foreach (int i in sortedUnits[index])
                units[i].Select();
        }

        public void SelectedMoveTo(PointF p)
        {
            foreach (int index in selectedUnits)
                units[index].MoveTo(p);
        }

        public void GetColumn(int index)
        {
            List<float> ycoords = new List<float>();

            foreach (int i in sortedUnits[index])
                ycoords.Add(units[i].position.y);
        }        

        public void ClearSelectedList()
        {
            foreach (int index in selectedUnits)
                units[index].DeSelect();
            selectedUnits.Clear();
        }

        public void SelectEverybody()
        {
            foreach (KeyValuePair<int, Unit> pair in units)
            {
                pair.Value.Select();
            }                            
        }

        public void SelectRect(PointF p1, PointF p2)
        {
            if (p1.x > p2.x)
            {
                PointF temp = p1;
                p1 = p2;
                p2 = temp;
            }
            if (p1.y > p2.y)
            {
                float temp = p1.y;
                p1.y = p2.y;
                p2.y = temp;
            }

            int startColumnIndex = (int)Math.Floor(p1.x / stepX);
            int endColumnIndex = (int)Math.Floor(p2.x / stepX);            
            for (int i = startColumnIndex; i < endColumnIndex; i++)
            {
                //List<int> selected = new List<int>();
                List<int> column = sortedUnits[i];
                int startI = 0;
                int endI = column.Count - 1;
                foreach (int c in column)
                {
                    if (p1.y <= units[c].position.y) break;
                    startI++;
                }
                for (int ii = column.Count-1; ii > 0; ii--)
                {
                    int c = column[ii];
                    if (p2.y >= units[c].position.y) break;
                    endI--;
                }
                for (int ii = startI; ii < endI; ii++)
                    units[column[ii]].Select();
            }
        }

        private void Unit_Selected(object sender, EventArgs e)
        {
            selectedUnits.Add(((Unit)sender).id);
        }

        public void EverybodyFromMoveTo(Team t, PointF pos)
        {
            foreach (int index in t.units)
                units[index].MoveTo(pos);
        }

        public void EverybodyFromMove(Team t)
        {
            foreach (int index in t.units)
            {
                units[index].MoveByPath();
                //UpdateSortedPos(units[index]);
            }
        }

        //NOT WORKS
        private void UpdateSortedPos(Unit u)
        {            
            int newIndexX = GetColumnIndex(u.position.x);                                    
            //delete from old column            
            sortedUnits[u.sortedPos.x].RemoveAt(u.sortedPos.y);
            //add to new column
            List<int> newColumn = sortedUnits[newIndexX];
            int newIndexY = 0;
            foreach (int i in newColumn)
            {
                if (units[i].position.y < u.position.y) break;
                newIndexY++;
            }
            u.sortedPos = new PointI(newIndexX, newIndexY);
            newColumn.Insert(newIndexY, u.id);
        }

        private int GetColumnIndex(float x)
        {
            return (int)Math.Floor(x / stepX);
        }
    }
}
