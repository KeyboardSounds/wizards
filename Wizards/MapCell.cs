using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards
{
    class MapCell
    {
        public List<int> TileIDs = new List<int>();

        public void AddTile(int tileID)
        {
            TileIDs.Add(tileID);
        }

        public int TileID
        {
            get { return TileIDs[0]; }
        }

        public MapCell(int tileID)
        {
            AddTile(tileID);
        }

    }
}
