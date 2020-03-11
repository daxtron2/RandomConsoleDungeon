using System;

namespace RandomConsoleDungeon
{
    internal class Vector2
    {
        internal int x, y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static int Distance(Vector2 a, Vector2 b)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2)));
        }

        public static Vector2 Up { get { return new Vector2(0, -1); } }
        public static Vector2 Down { get { return new Vector2(0, 1); } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } }
        public static Vector2 Right { get { return new Vector2(1, 0); } }

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }

        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.x * b, a.y * b);
        }
    }
}