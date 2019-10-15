namespace Fluidity.Core
{

    public enum Direction { UP, DOWN, LEFT, RIGHT };

    struct Action
    {
        int priority; //0-255 low to high
    }

    public struct Subtile
    {
        public double currentMass;
        public double currentPressure;
        public double currentTemperature;
        public int substanceId;
        public State state;
        public int priority;
        public double momentumForceX;
        public Direction momentumDirectionX;
        public double momentumForceY;
        public Direction momentumDirectionY;

        public Subtile(int substanceId, State state, double currentMass, double currentPressure, double currentTemperature)
        {
            this.substanceId = substanceId;
            this.state = state;
            this.currentMass = currentMass;
            this.currentPressure = currentPressure;
            this.currentTemperature = currentTemperature;
            priority = 0;
            momentumDirectionX = Direction.UP;
            momentumDirectionY = Direction.UP;
            momentumForceX = 0;
            momentumForceY = 0;
        }
    }

    public class Tile
    {
        Subtile[,] subtiles;
        public readonly int subtileResolution = 1;
        public int MAPSIZE_X = 0;
        public int MAPSIZE_Y = 0;
        public int TILE_X = 0;
        public int TILE_Y = 0;
        public readonly bool edgeTile = false;

        Map map;

        public Tile(int subtileResolution, int map_x, int map_y, int tile_x, int tile_y, Map map)
        {
            subtiles = new Subtile[subtileResolution, subtileResolution];
            this.subtileResolution = subtileResolution;
            this.map = map;
            MAPSIZE_X = map_x;
            MAPSIZE_Y = map_y;
            TILE_X = tile_x;
            TILE_Y = tile_y;
            if (TILE_X == 0 || TILE_X == map_x || TILE_Y == 0 || TILE_Y == map_y)
            {
                edgeTile = true;
            }
        }

        public Subtile getSubtile(int x, int y)
        {
            return this.subtiles[x, y];
        }

        public static void swapSubTiles(Subtile subtile1, Subtile subtile2)
        {
            Subtile swapTile = subtile1;
            subtile1 = subtile2;
            subtile2 = swapTile;
        }

        public double getSubtileForce(int x, int y, Direction direction)
        {

            switch (subtiles[x, y].state)
            {
                case State.GAS: return 0;
                case State.LIQUID: return 0;
                case State.SOLID: return 0;
                default: return 0;
            }

        }

        /* Merge two gasses and remove the content of the second tile
         * Not for equalizing slowly, this is for rapid compression or gas of low mass merging with gas of larger mass
         */
        public void mergeSubtiles(int x1, int y1, int x2, int y2)
        {
            if ((x1 == x2 && (y1 - 1 == y2 || y1 + 1 == y2)) || (y1 == y2 && (x1 - 1 == x2 || x1 + 1 == x2)))
            {
                equalizePressure(x1, y1, x2, y2);
                equalizeTemperature(x1, y1, x2, y2);
                subtiles[x1, y1].currentMass += subtiles[x2, y2].currentMass;
            }
        }

        public void equalizePressure(int x1, int y1, int x2, int y2)
        {
            double newPressure = Tools.getSubstanceMixPressure(subtiles[x1, y1].substanceId, subtileResolution, subtiles[x1, y1].currentMass, subtiles[x2, y2].currentMass, subtiles[x1, y1].currentTemperature, subtiles[x2, y2].currentTemperature);
            subtiles[x1, y1].currentPressure = newPressure;
            subtiles[x2, y2].currentPressure = newPressure;
        }

        public void equalizeTemperature(int x1, int y1, int x2, int y2)
        {
            double newTemp = Tools.getSubstanceMixTemperature(subtiles[x1, y1].substanceId, subtiles[x1, y1].currentMass, subtiles[x2, y2].currentMass, subtiles[x1, y1].currentTemperature, subtiles[x2, y2].currentTemperature);
            subtiles[x1, y1].currentTemperature = newTemp;
            subtiles[x2, y2].currentTemperature = newTemp;
        }

        public void equalizeMass(int x1, int y1, int x2, int y2)
        {
            double newMass = (subtiles[x1, y1].currentMass + subtiles[x2, y2].currentMass) / 2;
            subtiles[x1, y1].currentMass = newMass;
            subtiles[x2, y2].currentMass = newMass;
        }

        public void splitSubtile()
        {

        }

        public void updateTemperature(int x, int y, double newTemperature)
        {

        }

        public void updatePressure()
        {

        }

        public void moveSubtile(int x, int y, Direction direction)
        {

        }

        public void subtileAction(int x, int y)
        {
            entropyHeat(x, y);
            reachEqulibrium(x, y);
        }

        public void entropyHeat(int x, int y)
        {
            if (subtiles[x, y].currentTemperature > 0.1)
            {
                conductHeat(x, y);
                if (subtiles[x, y].currentTemperature > 325) //at temperatures greater than 50, start trying to radiate heat away
                {
                    radiateHeat(x, y);
                }
            }
        }

        public void conductHeat(int x, int y)
        {
            int length_x = subtiles.GetLength(0);
            int length_y = subtiles.GetLength(1);
            double rightTemp = -1;
            double upTemp = -1;
            double leftTemp = -1;
            double downTemp = -1;



            if (x == 0)
            {
                if (!edgeTile)
                {
                    leftTemp = map.getTile(TILE_X - 1, TILE_Y).getSubtile(subtileResolution - 1, y).currentTemperature;
                }
                rightTemp = subtiles[x + 1, y].currentTemperature;
            }
            else if (x == length_x - 1)
            {
                if (!edgeTile)
                {
                    rightTemp = map.getTile(TILE_X + 1, TILE_Y).getSubtile(0, y).currentTemperature;
                }
                leftTemp = subtiles[x - 1, y].currentTemperature;

            }
            else
            {
                rightTemp = subtiles[x + 1, y].currentTemperature;
                leftTemp = subtiles[x - 1, y].currentTemperature;
            }

            if (y == 0)
            {
                if (!edgeTile)
                {
                    upTemp = map.getTile(TILE_X, TILE_Y - 1).getSubtile(x, subtileResolution - 1).currentTemperature;
                }
                downTemp = subtiles[x + 1, y].currentTemperature;
            }
            else if (y == length_y)
            {
                if (!edgeTile)
                {
                    downTemp = map.getTile(TILE_X, TILE_Y + 1).getSubtile(x, 0).currentTemperature;
                }
                upTemp = subtiles[x - 1, y].currentTemperature;
            }
            else
            {
                upTemp = subtiles[x, y - 1].currentTemperature;
                downTemp = subtiles[x, y + 1].currentTemperature;
            }

        }

        public void radiateHeat(int x, int y)
        {

        }

        public void reachEqulibrium(int x, int y)
        {

        }
    }
}