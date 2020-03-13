using System;
using System.Collections.Generic;

namespace RandomConsoleDungeon
{
    internal class Screen
    {
        private static Screen instance = null;
        public static Screen Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Screen();
                }
                return instance;
            }
        }

        public int ScreenWidth { get; private set; }
        public int ScreenHeight { get; private set; }
        public Vector2 FirstRoomPos { get; private set; }

        public Tile[,] tiles;

        private DungeonGenerator dg;
        private Screen()
        {

            ScreenWidth = ConsoleHelpers.Width;
            ScreenHeight = ConsoleHelpers.Height;

            tiles = new Tile[ScreenWidth, ScreenHeight];            
        }

        public void Init()
        {
            if(dg is null)
                dg = new DungeonGenerator();
            for (int x = 0; x < ScreenWidth; x++)
            {
                for (int y = 0; y < ScreenHeight; y++)
                {
                    tiles[x, y] = new Tile(x, y, '\0');
                }
            }

            FirstRoomPos = dg.GenerateRooms();

            DisplayScreen();
        }

        internal bool IsValidPosition(Vector2 pos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x >= ScreenWidth || pos.y >= ScreenHeight)
                return false;
            else
                return true;
        }

        internal bool CheckWalkable(Vector2 expectedPos)
        {
            var goToTest = tiles[expectedPos.x, expectedPos.y].GameObject;
            if (goToTest is Wall) return false;
            if (goToTest is null) return false;
            if (goToTest is Door)
            {
                var door = goToTest as Door;
                if (!door.IsWalkable) return false;
            }
            return true;
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