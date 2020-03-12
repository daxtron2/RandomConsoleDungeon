using System;

namespace RandomConsoleDungeon
{
    internal class Vector2
    {
        internal int x, y;
        public Vector2 Normalized { get { return Vector2.Normalize(this); } }
        public int Magnitude { get { return Vector2.GetMagnitude(this); } }


        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private static int GetMagnitude(Vector2 A)
        {
            return (int)Math.Floor(Math.Sqrt(A.x * A.x + A.y * A.y));
        }
        public static Vector2 Normalize(Vector2 a)
        {
            return new Vector2(a.x / a.Magnitude, a.y / a.Magnitude);
        }
        public static int Distance(Vector2 a, Vector2 b)
        {
            return (int)Math.Round(Math.Sqrt(Math.Pow(b.x - a.x, 2) + Math.Pow(b.y - a.y, 2)));
        }
        public static Vector2 Zero { get { return new Vector2(0, 0); } }
        public static Vector2 Up { get { return new Vector2(0, -1); } }
        public static Vector2 Down { get { return new Vector2(0, 1); } }
        public static Vector2 Left { get { return new Vector2(-1, 0); } }
        public static Vector2 Right { get { return new Vector2(1, 0); } }
        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x + b.x, a.y + b.y);
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.x - b.x, a.y - b.y);
        }
        public static Vector2 operator *(Vector2 a, int b)
        {
            return new Vector2(a.x * b, a.y * b);
        }

    }
}