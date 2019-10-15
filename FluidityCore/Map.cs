using List;
namespace Fluidity.Core
{
    public class Map
    {

        int SIZE_X;
        int SIZE_Y;
        int[,] mapTiles;

        public Map(int x, int y, List<Tile> tiles){
            SIZE_X = x;
            SIZE_Y = y;
            mapTiles = new mapTiles[x, y];
            foreach (Tile tile in tiles){
                mapTiles[tile.x, tile.y] = tile;
            }
        }

        public static getTile(int x, int y)
        {
            return mapTiles[x, y];
        }
    }
}