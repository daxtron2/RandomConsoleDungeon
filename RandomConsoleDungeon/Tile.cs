using System;

namespace RandomConsoleDungeon
{
    internal class Tile
    {
        private Vector2 Position;
        public char DisplayCharacter { get; internal set; }
        public char DefaultCharacter;

        private GameObject obj;
        public Tile(int x, int y, char defaultCharacter)
        {
            Position = new Vector2(x, y);

            //Testing
            DefaultCharacter = DisplayCharacter = defaultCharacter;

        }

        public void Display()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(DisplayCharacter);
        }

        internal void ResetCharacter()
        {
            DisplayCharacter = DefaultCharacter;
        }

        internal void SetWall()
        {
            DisplayCharacter = DefaultCharacter = '#';
        }
    }
}