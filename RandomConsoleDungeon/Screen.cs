using System;

namespace RandomConsoleDungeon
{
    internal class Screen
    {
        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Vector2 FirstRoomPos { get; private set; }

        static public Tile[,] tiles;

        private DungeonGenerator dg;
        public Screen(Tuple<int, int> ScreenSizeWH)
        {
            dg = new DungeonGenerator();

            ScreenWidth = ConsoleHelpers.Width;
            ScreenHeight = ConsoleHelpers.Height;

            tiles = new Tile[ScreenWidth, ScreenHeight];

            for (int x = 0; x < ScreenWidth; x++)
            {
                for (int y = 0; y < ScreenHeight; y++)
                {
                    //string def = (y).ToString();
                    tiles[x, y] = new Tile(x, y, ' ');
                }
            }

            FirstRoomPos = dg.GenerateRooms();

            ////debug testing out to file
            //string toWrite = "";
            //for (int x = 0; x < ScreenWidth; x++)
            //{
            //    toWrite += x + ": ";
            //    for (int y = 0; y < ScreenHeight; y++)
            //    {
            //        toWrite += tiles[x, y];
            //    }
            //    toWrite += "\n";
            //}

            //using (System.IO.StreamWriter writer = new System.IO.StreamWriter("Dungeon.txt"))
            //{
            //    writer.WriteLine(toWrite);
            //}

            DisplayScreen();
        }

        internal bool CheckWall(Vector2 expectedPos)
        {
            return tiles[expectedPos.x, expectedPos.y].GameObject is Wall;
        }

        public void DisplayScreen()
        {
            string display = "";
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    display += (tiles[x, y].DisplayCharacter);
                }
            }

            ClearScreen();
            Console.Write(display);
        }

        internal void MoveCharacter(int x, int y, Vector2 dir)
        {
            var original = tiles[x, y];
            var next = tiles[x + dir.x, y + dir.y];

            next.DisplayCharacter = original.DisplayCharacter;
            original.ResetCharacter();

            original.Display();
            next.Display();

        }

        internal void SetCharacter(int x, int y, char character)
        {
            tiles[x, y].DisplayCharacter = character;
            tiles[x, y].Display();
        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}