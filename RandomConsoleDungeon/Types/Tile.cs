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
            DefaultCharacter = defaultCharacter;
            DisplayCharacter = '\0';

        }

        public void Display()
        {
            Console.SetCursorPosition(Position.x, Position.y);
            Console.Write(DisplayCharacter);
        }

        internal void ObjectDied()
        {
            if (GameObject is AttackableObject)
                SetPath(DefaultCharacter);
        }

        internal void ResetCharacter()
        {
            DisplayCharacter = DefaultCharacter;
        }

        private void SetCharacter(char toSet)
        {
            DefaultCharacter = toSet;
            DisplayCharacter = '\0';
        }
        internal Tile SetWall(char toSet ='#')
        {
            SetCharacter(toSet);
            GameObject = new Wall();
            return this;
        }


        internal Tile SetPath(char toSet = '-')
        {
            SetCharacter(toSet);
            if(!(GameObject is Wall))            
            {
                GameObject = new Path();
            }
            
            return this;
        }

        internal Tile SetDoor(char toSet = '+')
        {
            SetCharacter(toSet);
            GameObject = new Door(Position);
            return this;
        }

        public override string ToString()
        {
            return DisplayCharacter.ToString();
        }

        internal void Reveal()
        {
            DisplayCharacter = DefaultCharacter;
            Display();
        }

        internal void SwapObject(ref Tile other)
        {
            var thisOrigObj = GameObject;
            GameObject = other.GameObject;
            other.GameObject = thisOrigObj;
        }
        internal void SetGameObject(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}