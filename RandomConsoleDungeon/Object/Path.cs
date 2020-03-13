namespace RandomConsoleDungeon
{
    internal class Path : GameObject
    {
        public Path() : base()
        {
        }

        public bool IsWalkable { get; protected set; } = true;

    }
}