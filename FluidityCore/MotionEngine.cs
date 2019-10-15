using System;

namespace Fluidity.Core
{
    public class MotionEngine
    {
        int y = 6;
        int x = 6;

        static void Main(String[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        public void run()
        {
            MotionEngine fluidty = new MotionEngine();

            double[,] from = new double[x, y];
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

        private void threeBy3avg(int passes, double[,] from)
        {
            if (passes == 0)
            {
                return;
            }
            double[,] to = new double[from.GetLength(0), from.GetLength(1)];

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

        private double avgNeighbour(double[,] from, int x, int y)
        {
            int lengthX = from.GetLength(0) - 1;
            int lengthY = from.GetLength(1) - 1;

            double denominator = 0;
            double numerator = 0;

            if (x == 0)
            {
                numerator += from[x + 1, y];
                denominator++;
            }
            else if (x == lengthX - 1)
            {
                numerator += from[x - 1, y];
                denominator++;
            }
            else
            {
                numerator += from[x + 1, y];
                numerator += from[x - 1, y];
                denominator += 2;
            }

            if (y == 0)
            {
                numerator += from[x, y + 1];
                denominator++;
            }
            else if (y == lengthY - 1)
            {
                numerator += from[x, y - 1];
                denominator++;
            }
            else
            {
                numerator += from[x, y + 1];
                numerator += from[x, y - 1];
                denominator += 2;
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

        private void Update()
        {
            //Tick once
        }

        public void init()
        {

            //read materials list
        }
    }
}