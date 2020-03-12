using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class DungeonGenerator
    {
        private const int minRooms = 10, maxRooms = 25;
        private const int minSize = 3, maxSize = 5;
        private Random rng;
        

        public DungeonGenerator()
        {
            rng = new Random();
        }

        /// <summary>
        /// generates suware rooms
        /// </summary>
        /// <param name="Screen.tiles">tile array</param>
        /// <returns>The first room's center for player spawning</returns>
        public Vector2 GenerateRooms()
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
                   room.Position.x + room.roomSize >= width)
                {
                    addRoomToList = false;
                }
                if(room.Position.y - room.roomSize < 0 ||
                   room.Position.y + room.roomSize >= height)
                {
                    addRoomToList = false;
                }

                if(addRoomToList)
                {
                    rooms.Add(room);
                }
            }
            
            if(rooms.Count > 0)
            {
                foreach(Room rm in rooms)
                {
                    PlaceRoom(rm.Position, rm.roomSize);
                }
                ConnectAllRooms(rooms);
                return rooms[0].Position;
            }
            else
            {
                var roomPos = new Vector2(width / 2, height / 2);
                PlaceRoom(roomPos, 2);
                return roomPos;
            }
        }

        private void ConnectAllRooms(List<Room> rooms)
        {            
            foreach(Room rm in rooms)
            {                
                var closestUnconnected = GetClosestRoom(rooms, rm, false);
                if(closestUnconnected != null)
                {
                    ConnectRooms(rm, closestUnconnected);
                    rooms[rooms.IndexOf(closestUnconnected)].Connected = true;
                    rm.Connected = true;
                }

                var closestConnected = GetClosestRoom(rooms, rm, true);
                if(closestConnected != null)
                {
                    ConnectRooms(rm, closestConnected);
                    rm.Connected = true;
                }

                if(closestConnected == null && closestUnconnected == null)
                {
                    //Just one room i think?
                    //put stuff necessary in this room i guess idfk
                }
            }
        }

        private void ConnectRooms(Room a, Room b)
        {
            ConnectWalls(Screen.tiles[a.Position.x, a.Position.y], Screen.tiles[b.Position.x, b.Position.y]);
        }

        private Room GetClosestRoom(List<Room> rms, Room thisRm, bool? connected)
        {
            
            Room rMin = null;
            int minDist = int.MaxValue;
            Vector2 currentPos = thisRm.Position;
            foreach (Room rm in rms)
            {
                if (rm == thisRm) continue;
                if (connected.HasValue)
                {
                    if ( rm.Connected && !connected.Value) continue;
                    if (!rm.Connected &&  connected.Value) continue;
                }
                int dist = Vector2.Distance(rm.Position, currentPos);
                if (dist < minDist)
                {
                    rMin = rm;
                    minDist = dist;
                }
            }
            return rMin;
        }

        private void PlaceRoom(Vector2 pos, int size)
        {
            int w = ConsoleHelpers.Width;
            int h = ConsoleHelpers.Height;

            var tl = Screen.tiles[pos.x - size, pos.y - size].SetWall('1');
            var tr = Screen.tiles[pos.x + size, pos.y - size].SetWall('3');
            ConnectWalls(tl, tr);

            var bl = Screen.tiles[pos.x - size, pos.y + size].SetWall('2');
            var br = Screen.tiles[pos.x + size, pos.y + size].SetWall('4');
            ConnectWalls(bl, br);

            ConnectWalls(tl, bl);
            ConnectWalls(tr, br);
        }

        private void ConnectWalls(Tile start, Tile end)
        {
            Vector2 dir = (end.Position - start.Position).Normalized;
            int dist = Vector2.Distance(start.Position, end.Position);
            for(int i = 1; i < dist; i++)
            {
                Screen.tiles[start.Position.x + (dir.x * i), start.Position.y + (dir.y * i)].SetWall();
            }

        }

        private Vector2 GetRandomPosition(int width, int height)
        {
            int randX = rng.Next(0, width);
            int randY = rng.Next(0, height);
            return new Vector2(randX, randY);
        }
    }

    class Room
    {
        public Vector2 Position;
        public int roomSize;

        public bool Connected { get; internal set; } = false;
    }
}
