namespace Fluidity.Core
{

    enum Direction { UP, DOWN, LEFT, RIGHT };

    struct Action
    {
        int priority = 0; //0-255 low to high
    }

    struct Subtile
    {
        float currentMass = 0;
        float currentPressure = 0;
        float currentTemperature = 0;
        int substanceId;
        State state;
        int priority;
        float momentumForceX = 0;
        Direction momentumDirectionX;
        float momentumForceY = 0;
        Direction momentumDirectionY;

        public Subtile(int substanceId, State state, float currentMass, float currentPressure, float currentTemperature)
        {
            this.substanceId = substanceId;
            this.state = state;
            this.currentMass = currentMass;
            this.currentPressure = currentPressure;
            this.currentTemperature = currentTemperature;
        }
    }

    public class Tile
    {
        Subtile[,] subtiles;
        Subtile swapTile = new Subtile();
        int subtileResolution = 1;
        int MAPSIZE_X = 0;
        int MAPSIZE_Y = 0;
        int TILE_X = 0;
        int TILE_Y = 0;
        readonly bool edgeTile = false;

        public Tile(int subtileResolution, int map_x, int map_y, int tile_x, int tile_y)
        {
            subtiles = new Subtile[subtileResolution, subtileResolution];
            this.subtileResolution = subtileResolution;
            MAPSIZE_X = map_x;
            MAPSIZE_Y = map_y;
            TILE_X = tile_x;
            TILE_Y = tile_y;
            if (TILE_X == 0 || TILE_X == map_x || TILE_Y == 0 || TILE_Y == map_y)
            {
                edgeTile = true;
            }
        }

        public static Subtile getSubtile(int x, int y)
        {
            return subtiles[x, y];
        }

        public static void swapSubTiles(Subtile subtile1, Subtile subtile2)
        {
            swapTile = subtile1;
            subtile1 = subtile2;
            subtile2 = swapTile;
        }

        public float getSubtileForce(int x, int y, Direction direction)
        {

            switch (subtiles[x, y].state)
            {
                case State.GAS: subtile[x, y]; break;
                case State.LIQUID: break;
                case State.SOLID: return 0.0;
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
            float newPressure = Tools.getSubstanceMixPressure(subtiles[x1, y1].substanceId, subtileResolution, subtiles[x1, y1].currentMass, subtiles[x2, y2].currentMass, subtiles[x1, y1].currentTemperature, subtiles[x2, y2].currentTemperature);
            subtiles[x1, y1].currentPressure = newPressure;
            subtiles[x2, y2].currentPressure = newPressure;
        }

        public void equalizeTemperature(int x1, int y1, int x2, int y2)
        {
            float newTemp = Tools.getSubstanceMixTemperature(subtiles[x1, y1].substanceId, subtiles[x1, y1].currentMass, subtiles[x2, y2].currentMass, subtiles[x1, y1].currentTemperature, subtiles[x2, y2].currentTemperature);
            subtiles[x1, y1].currentTemperature = newTemp;
            subtiles[x2, y2].currentTemperature = newTemp;
        }

        public void equalizeMass(int x1, int y1, int x2, int y2)
        {
            float newMass = (subtiles[x1, y1].currentMass + subtiles[x2, y2].currentMass) / 2;
            subtiles[x1, y1].currentMass = newMass;
            subtiles[x2, y2].currentMass = newMass;
        }

        public void splitSubtile()
        {

        }

        public void updateTemperature(int x, int y, float newTemperature)
        {

        }

        public void updatePressure()
        {

        }

        public void moveSubtile(int x, int y, Direction direction)
        {
            subtiles[x, y];
        }

        public void subtileAction(int x, int y)
        {
            entropyHeat(x, y);
            reachEqulibrium(x, y);
        }

        public void entropyHeat(int x, int y)
        {
            if (currentTemperature > 0.1)
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

            float rightTemp = -1;
            float upTemp = -1;
            float leftTemp = -1;
            float downTemp = -1;

            switch (x)
            {
                case 0:
                    if (!edgeTile)
                    {
                        leftTemp = Map.getTile(TILE_X, TILE_Y).getSubtile(subtileResolution - 1, y);
                    }
                    rightTemp = subtiles[x + 1, y].currentTemperature;
                    break;
                case subtileResolution - 1:
                    if (!edgeTile)
                    {
                        rightTemp = Map.getTile(TILE_X, TILE_Y).getSubtile(0, y);
                    }
                    leftTemp = subtiles[x - 1, y].currentTemperature;
                    break;
                default:
                    rightTemp = subtiles[x + 1, y].currentTemperature;
                    leftTemp = subtiles[x - 1, y].currentTemperature;
                    break;
            }
            switch (y)
            {
                case 0:
                    if (!edgeTile)
                    {
                        upTemp = Map.getTile(TILE_X, TILE_Y).getSubtile(x, subtileResolution - 1);
                    }
                    downTemp = subtiles[x, y + 1].currentTemperature;
                    break;
                case subtileResolution - 1:
                    if (!edgeTile)
                    {
                        downTemp = Map.getTile(TILE_X, TILE_Y).getSubtile(x, 0);
                    }
                    upTemp = subtiles[x, y - 1].currentTemperature;
                    break;
                default:
                    upTemp = subtiles[x, y - 1].currentTemperature;
                    downTemp = subtiles[x, y + 1].currentTemperature;
                    break;
            }
           
       
        }

        public void radiateHeat(int x, int y)
        {

        }
    }
}