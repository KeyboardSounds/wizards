using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wizards
{
    class TileMap //TODO: fix some tiles on layers 1 and up disappearing
    {
        public List<MapRow> Rows = new List<MapRow>();
        public int MapWidth;
        public int MapHeight;
        public Texture2D TileSetTexture;
        public int TileSize;
        private string TileMapPath = "../../../../Content/tilemaps/blergh.json";

        public TileMap()
        {
            

            // Read map data from file
            string json = File.ReadAllText(TileMapPath);
            var rootObj = JsonConvert.DeserializeObject<RootObject>(json);

            MapHeight = rootObj.height;
            MapWidth = rootObj.width;
            TileSize = rootObj.tileheight;
            int TileIDOffset = rootObj.tilesets[0].firstgid; // we will assume the map only uses one tileset
            rootObj.layers.Reverse();
            int dataIdx = 0;

            for (int y = 0; y < MapHeight; y++)
            {
                MapRow thisRow = new MapRow();
                for (int x = 0; x < MapWidth; x++)
                {
                    foreach (Layer layer in rootObj.layers)
                    {
                        if(layer.data[dataIdx] == 0)
                        {
                            continue;
                        }
                        try
                        {
                            thisRow.Columns[x].AddTile(layer.data[dataIdx] - TileIDOffset);
                        }
                        catch (Exception)
                        {
                            thisRow.Columns.Add(new MapCell(layer.data[dataIdx] - TileIDOffset));
                        }
                        
                    }
                            
                    dataIdx++;
                }
                Rows.Add(thisRow);
                    
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int y = 0; y < MapWidth; y++)
            {
                for (int x = 0; x < MapHeight; x++)
                {
                    foreach (int tileID in Rows[y].Columns[x].TileIDs)
                    {
                        spriteBatch.Draw(
                        TileSetTexture,
                        new Rectangle((x * TileSize), (y * TileSize), TileSize, TileSize),
                        GetSourceRectangle(tileID),
                        Color.White);
                    }

                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tileIndex">The 0-indexed position of the tile.</param>
        /// <returns></returns>
        public Rectangle GetSourceRectangle(int tileIndex)
        {
            int tileY = tileIndex / (TileSetTexture.Width / TileSize);
            int tileX = tileIndex % (TileSetTexture.Width / TileSize);

            return new Rectangle(tileX * TileSize, tileY * TileSize, TileSize, TileSize);
        }
    }


}
