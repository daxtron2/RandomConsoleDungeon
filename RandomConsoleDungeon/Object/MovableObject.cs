namespace RandomConsoleDungeon
{
    internal class MovableObject : GameObject
    {
        public MovableObject() : base()
        {

        }

        internal virtual void MoveObject(Vector2 dir)
        {
            if (CheckMove(dir))
            {
                Screen.MoveCharacter(Position.x, Position.y, dir);
                Position += dir;
            }
        }

        private bool CheckMove(Vector2 dir)
        {
            var expectedPos = Position + dir;

            if (expectedPos.x < 0 || expectedPos.x >= Screen.ScreenWidth) return false;
            if (expectedPos.y < 0 || expectedPos.y >= Screen.ScreenHeight) return false;
            if (!Screen.CheckWalkable(expectedPos)) return false;            

            return true;
        }
    }
}