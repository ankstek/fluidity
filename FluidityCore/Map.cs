using System.Collections.Generic;
namespace Fluidity.Core
{
    public class Map
    {

        int SIZE_X;
        int SIZE_Y;
        public Tile[,] mapTiles;

        public Map(int x, int y, List<Tile> tiles){
            SIZE_X = x;
            SIZE_Y = y;
            mapTiles = new Tile[x, y];
            foreach (Tile tile in tiles){
                mapTiles[tile.TILE_X, tile.TILE_X] = tile;
            }
        }

        public Tile getTile(int x, int y)
        {
            return mapTiles[x, y];
        }
    }
}