namespace RandomConsoleDungeon
{
    internal class GameObject
    {
        internal Vector2 Position;

        protected char DisplayChar;

        protected Screen screen;

        protected virtual void DisplayObject()
        {
            screen.SetCharacter(Position.x, Position.y, DisplayChar);
        }
    }
}