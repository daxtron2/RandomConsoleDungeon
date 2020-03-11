using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Game : IDisposable
    {
        public bool Running { get; internal set; }


        private Screen Screen;
        private Player player;

        internal void Init()
        {
            Running = true;
            Console.CursorVisible = false;
            ConsoleHelpers.SetConsoleSize();
            Screen = new Screen(ConsoleHelpers.GetSize());
            player = new Player(Screen);
            //player.Position = Screen.FirstRoomPos;

        }


        public void Dispose()
        {
            //Console.WriteLine("Dispose anything necessary");
        }


        //non-game logic stuff
        internal void PreUpdate()
        {
            ConsoleHelpers.ResizeCheck();
        }

        //game logic
        internal void Update()
        {
            Input();
            
        }

        private void Input()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey(true).KeyChar)
                {
                    case 'w':
                        player.MoveObject(Vector2.Up);
                        break;
                    case 'a':
                        player.MoveObject(Vector2.Left);
                        break;
                    case 's':
                        player.MoveObject(Vector2.Down);
                        break;
                    case 'd':
                        player.MoveObject(Vector2.Right);
                        break;
                }
            }
        }

        //post game logic stuff, display probably
        internal void PostUpdate()
        {
            
        }
    }
}
