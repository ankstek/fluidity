using System;

namespace Fluidity.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            new Core.MotionEngine().run();
        }
    }
}
