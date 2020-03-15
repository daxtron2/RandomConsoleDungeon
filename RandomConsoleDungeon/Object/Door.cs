using System;

namespace RandomConsoleDungeon
{
    internal class Door : Path
    {
        private bool NeedsToCheckPaths = true;
        public Door(Vector2 pos) : base()
        {
            Position = pos;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            if (NeedsToCheckPaths)
            {
                NeedsToCheckPaths = false;
                CheckHasValidPaths();
            }
        }

        private void CheckHasValidPaths()
        {
            int numOfPaths = 0;
            Vector2 above = Position + Vector2.Up;
            Vector2 below = Position + Vector2.Down;
            Vector2 left  = Position + Vector2.Left;
            Vector2 right = Position + Vector2.Right;

            if (Screen.IsValidPosition(above) && Screen.AccessTile(above.x, above.y)?.GameObject is Path)
            {
                numOfPaths++;
            }
            if (Screen.IsValidPosition(below) && Screen.AccessTile(below.x, below.y)?.GameObject is Path)
            {
                numOfPaths++;
            }
            if (Screen.IsValidPosition(left) && Screen.AccessTile(left.x, left.y)?.GameObject is Path)
            {
                numOfPaths++;
            }
            if (Screen.IsValidPosition(right) && Screen.AccessTile(right.x, right.y)?.GameObject is Path)
            {
                numOfPaths++;
            }

            if (numOfPaths >= 2) IsWalkable = true;
            else IsWalkable = false;
        }
    }
}