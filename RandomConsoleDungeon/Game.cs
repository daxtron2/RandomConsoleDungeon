using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Game : IDisposable
    {
        private static Game instance = null;
        public static Game Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Game();
                }
                return instance;
            }
        }


        private Game() { }
        public bool Running { get; internal set; }


        private Screen Screen;
        private Player player;

        List<Updatable> Updatables;

        internal void Init()
        {
            Updatables = new List<Updatable>();
            Running = true;
            Console.CursorVisible = false;
            ConsoleHelpers.SetConsoleSize();
            Screen = Screen.Instance;
            Screen.Init();
            player = new Player(Screen.FirstRoomPos);

        }


        public void Dispose()
        {
            //Console.WriteLine("Dispose anything necessary");
            Updatables.Clear();
            Updatables = null;
            Screen = null;
            player = null;

            Running = false;
            instance = null;
        }
        //non-game logic stuff
        internal void PreUpdate()
        {
            ConsoleHelpers.ResizeCheck();
            foreach (Updatable up in Updatables)
            {
                up.PreUpdate();
            }
        }
        //game logic
        internal void Update()
        {
            Input();
            foreach(Updatable up in Updatables)
            {                
                up.Update();
            }            
        }
        //post game logic stuff, display probably
        internal void PostUpdate()
        {
            foreach (Updatable up in Updatables)
            {
                up.PostUpdate();
            }
        }
        internal void AddUpdatable(Updatable updatable)
        {
            Updatables.Add(updatable);
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
    }
}
