using System;

namespace RandomConsoleDungeon
{
    internal class Tile
    {
        public Vector2 Position { get; private set; }
        public char DisplayCharacter { get; internal set; }
        public char DefaultCharacter;
        public GameObject GameObject { get; private set; }

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

        internal Tile SetWall(char toSet ='#')
        {
            DisplayCharacter = DefaultCharacter = toSet;
            GameObject = new Wall();
            return this;
        }
        internal Tile SetPath(char toSet = '-')
        {
            DisplayCharacter = DefaultCharacter = toSet;
            if(GameObject is Wall)
            {
                DisplayCharacter = DefaultCharacter = '+';
            }
            GameObject = new Path();
            
            return this;
        }

        public override string ToString()
        {
            return DisplayCharacter.ToString();
        }

    }
}