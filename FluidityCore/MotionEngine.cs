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

            Random random = new Random();
            for (int j = 0; j < y; j++)
                for (int i = 0; i < x; i++)
                    from[i, j] = random.Next(0, 9);

            Console.WriteLine("Before:");
            PrintArrayGrid(from);
            double[,] to = fluidty.MeanL1Filter(1, from);
            Console.WriteLine("After:");
            PrintArrayGrid(to);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        ///   Filter the given array <paramref name="passes"/> number of times
        ///   with an L1 norm arithmetic mean filter.
        /// </summary>
        private double[,] MeanL1Filter(int passes, double[,] from)
        {
            double[,] to = new double[from.GetLength(0), from.GetLength(1)];

            for (int k = 0; k < passes; k++)
                for (int j = 0; j < y; j++)
                    for (int i = 0; i < x; i++)
                        to[i, j] = MeanL1Neighbour(from, i, j);

            return to;
        }

        /// <summary>
        ///   Compute the arithmetic mean of the neighbours in the L1 norm
        ///   (Manhattan distance = 1).
        /// </summary>
        private static double MeanL1Neighbour(double[,] from, int x, int y)
        {
            int xmin = Math.Max(0, x-1);
            int xmax = Math.Min(from.GetLength(0) - 1, x+1);
            int ymin = Math.Max(0, y-1);
            int ymax = Math.Min(from.GetLength(1) - 1, y+1);

            double numerator = 0;
            for (int i = xmin; i <= xmax; i++)
                numerator += from[i, y];
            for (int i = ymin; i <= ymax; i++)
                numerator += from[x, i];

            // Subtract (x, y) twice because it is being counted by the
            // iterations above and it shouldn't be.
            numerator -= 2 * from[x, y];
            double denominator = xmax - xmin + ymax - ymin;
            return numerator / denominator;
        }

        private static void PrintArrayGrid(double[,] arr)
        {
            for (int y = 0; y < arr.GetLength(1); y++)
            {
                for (int x = 0; x < arr.GetLength(0); x++)
                    Console.Write($"{arr[x, y],8:F3}");
                Console.WriteLine("");
            }
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
