using System;

namespace Fluidity.Core
{
    public class MotionEngine
    {
        static void Main(String[] args) 
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public void run()
        {
            MotionEngine fluidty = new MotionEngine();
            float[,] from = new float[6, 6];
            int i = 0;
            int j = 0;
            Random random = new Random();
            for (j = 0; j < y; j++)
            {
                for (i = 0; i < x; i++)
                {
                    from[i, j] = random.Next(0, 9);
                    Console.Write(from[i, j]);
                }
                Console.WriteLine();
            }
            fluidty.threeBy3avg(1, from);
            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        private void threeBy3avg(int passes, float[,] from)
        {
            if (passes == 0)
            {
                return;
            }
            int[,] to = new int[from.GetLength(0), from.GetLength(1)];

            int k = 0;
            int i = 0;
            int j = 0;
            for (k = 0; k < passes; k++)
            {
                for (j = 0; j < y; j++)
                {
                    for (i = 0; i < x; i++)
                    {
                        to[i, j] = avgNeighbour(from, i, j);
                        Console.Write(to[i, j]);
                    }
                    Console.WriteLine();
                }
            }
            threeBy3avg(passes--, to);
        }

        private float avgNeighbour(int[,] from, int x, int y)
        {
            int readonly lengthX = from.GetLength(0) - 1;
            int lengthY = from.GetLength(1) - 1;

            int denominator = 0;
            int numerator = 0;

            switch (x)
            {
                case 0:
                    numerator += from[x + 1, y];
                    denominator++;
                    break;
                case (lengthX):
                    numerator += from[x - 1, y];
                    denominator++;
                    break;
                default:
                    numerator += from[x + 1, y];
                    numerator += from[x - 1, y];
                    denominator += 2;
                    break;
            }
            switch (y)
            {
                case 0:
                    numerator += from[x, y + 1];
                    denominator++;
                    break;
                case (lengthY - 1):
                    numerator += from[x, y - 1];
                    denominator++;
                    break;
                default:
                    numerator += from[x, y + 1];
                    numerator += from[x, y - 1];
                    denominator += 2;
                    break;
            }

            if (denominator != 0)
            {
                return (numerator / denominator);
            }
            return -1;
        }

        public void Run()
        {
            //Manage ticks, setup game world etc.
        }

        private float Update()
        {
            //Tick once
        }

        public void init() {

            //read materials list
        }
    }
}