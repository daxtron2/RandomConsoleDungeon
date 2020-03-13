namespace RandomConsoleDungeon
{
    internal class GameObject : Updatable
    {
        internal Vector2 Position;

        protected char DisplayChar;

        public GameObject() : base()
        {
        }

        protected virtual void DisplayObject()
        {
            Screen.SetCharacter(Position.x, Position.y, DisplayChar);
        }
    }
}