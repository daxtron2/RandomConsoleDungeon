namespace RandomConsoleDungeon
{
    internal class MovableObject : GameObject
    {
        internal void MoveObject(Vector2 dir)
        {
            if (CheckMove(dir))
            {
                screen.MoveCharacter(Position.x, Position.y, dir);
                Position += dir;
            }
        }

        private bool CheckMove(Vector2 dir)
        {
            var expectedPos = Position + dir;

            if (expectedPos.x < 0 || expectedPos.x >= screen.ScreenWidth) return false;
            if (expectedPos.y < 0 || expectedPos.y >= screen.ScreenHeight) return false;

            return true;
        }
    }
}