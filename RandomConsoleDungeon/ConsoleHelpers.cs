using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    static class ConsoleHelpers
    {
        public static readonly int Height = 50, Width = 80;

        internal static void SetConsoleSize()
        {

            //Console.WriteLine($"{Console.LargestWindowWidth}:{Console.LargestWindowHeight}");
            Console.WindowHeight = Height+1;
            Console.WindowWidth = Width;

            Console.BufferHeight = Height+1;
            Console.BufferWidth = Width;

        }

        internal static void ResizeCheck()
        {
            if(Console.WindowHeight != Height || Console.WindowWidth != Width)
            {
                SetConsoleSize();
            }
        }

        internal static Tuple<int,int> GetSize()
        {
            return new Tuple<int, int>(Width, Height);
        }
    }
}
