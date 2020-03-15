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
        private List<Room> rooms;

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
                    SetTile(x, y, new Tile(x, y, '\0'));
                }
            }


            rooms = dg.GenerateRooms();
            FirstRoomPos = rooms[0].Position;

            DisplayScreen();
        }

        private void SetTile(int x, int y, Tile tile)
        {
            if (!IsValidPosition(new Vector2(x, y))) return;
            tiles[x, y] = tile;
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
            var goToTest = AccessTile(expectedPos.x, expectedPos.y)?.GameObject;
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
                    display += (AccessTile(x, y)?.DisplayCharacter);
                }
            }

            ClearScreen();
            Console.Write(display);
        }

        internal void MoveCharacter(int x, int y, Vector2 dir)
        {
            var original = AccessTile(x, y);
            var next = AccessTile(x + dir.x, y + dir.y);
            if (original is null || next is null) return;

            next.DisplayCharacter = original.DisplayCharacter;
            original.ResetCharacter();

            original.Display();
            next.Display();

        }

        internal void SetCharacter(int x, int y, char character)
        {
            Tile t = AccessTile(x, y);
            if (t is null) return;
            t.DisplayCharacter = character;
            t.Display();
        }

        internal void RevealTiles(Vector2 position, int halfDist)
        {
            for (int x = position.x - halfDist; x < position.x + halfDist; x++)
            {
                for (int y = position.y - halfDist; y < position.y + halfDist; y++)
                {
                    AccessTile(x, y)?.Reveal();
                }
            }
        }

        internal Tile AccessTile(int x, int y)
        {
            if(!IsValidPosition(new Vector2(x,y))) return null;
            return tiles[x, y];
        }

        private void ClearScreen()
        {
            Console.Clear();
        }
    }
}