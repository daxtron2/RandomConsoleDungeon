using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class DungeonGenerator
    {
        private const int minRooms = 3, maxRooms = 10;
        private const int minSize = 3, maxSize = 5;
        private Random rng;

        public DungeonGenerator()
        {
            rng = new Random();
        }

        /// <summary>
        /// generates suware rooms
        /// </summary>
        /// <param name="tiles">tile array</param>
        /// <returns>The first room's center for player spawning</returns>
        public Vector2 GenerateRooms(ref Tile[,] tiles)
        {
            int width = ConsoleHelpers.Width;
            int height = ConsoleHelpers.Height;

            List<Room> rooms = new List<Room>();
            int numRooms = rng.Next(minRooms, maxRooms + 1);
            for(int i = 0; i < numRooms; i++)
            {
                Room room = new Room();
                room.roomSize = rng.Next(minSize, maxSize + 1);

                room.Position = GetRandomPosition(width, height);

                bool addRoomToList = true;
                foreach(Room rm in rooms)
                {
                    if(Vector2.Distance(room.Position, rm.Position) < rm.roomSize + room.roomSize)
                    {
                        addRoomToList = false;
                        break;
                    }
                }

                if(room.Position.x - room.roomSize < 0 || 
                    room.Position.x + room.roomSize > width)
                {
                    addRoomToList = false;
                }
                if(room.Position.y - room.roomSize < 0 ||
                    room.Position.y + room.roomSize > height)
                {
                    addRoomToList = false;
                }

                if (addRoomToList)
                {
                    rooms.Add(room);
                }
            }

            if(rooms.Count > 0)
            {
                foreach(Room rm in rooms)
                {
                    PlaceRoom(ref tiles, rm.Position, rm.roomSize);
                }
                return rooms[0].Position;
            }
            else
            {
                var roomPos = new Vector2(width / 2, height / 2);
                PlaceRoom(ref tiles, roomPos, 2);
                return roomPos;
            }
        }

        private void PlaceRoom(ref Tile[,] tiles, Vector2 pos, int size)
        {
            int w = ConsoleHelpers.Width;
            int h = ConsoleHelpers.Height;

            tiles[pos.x - size, pos.y - size].DisplayCharacter = '1';
            tiles[pos.x - size, pos.y + size].DisplayCharacter = '2';
            tiles[pos.x + size, pos.y - size].DisplayCharacter = '3';
            tiles[pos.x + size, pos.y + size].DisplayCharacter = '4';
        }

        private Vector2 GetRandomPosition(int width, int height)
        {
            int randX = rng.Next(0, width);
            int randY = rng.Next(0, height);
            return new Vector2(randX, randY);
        }
    }

    struct Room
    {
        public Vector2 Position;
        public int roomSize;
    }
}
