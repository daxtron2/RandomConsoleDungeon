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
        private static Random rng;
        public static Random RandomInstance
        {
            get
            {
                if (rng == null)
                {
                    rng = new Random();
                }
                return rng;
            }
        }

        private Game() { }
        public bool Running { get; internal set; }

        private Screen Screen;
        private Player player;

        private List<Updatable> Updatables;
        private List<Enemy> enemies;

        internal void Init()
        {
            Updatables = new List<Updatable>();
            enemies = new List<Enemy>();
            Running = true;
            Console.CursorVisible = false;
            ConsoleHelpers.SetConsoleSize();
            Screen = Screen.Instance;
            Screen.Init();
            player = new Player(Screen.FirstRoomPos);
            enemies.Add(new RandomWalker(3, Screen.FirstRoomPos + Vector2.Up));
        }


        public void Dispose()
        {
            //Console.WriteLine("Dispose anything necessary");
            Updatables.Clear();
            enemies.Clear();
            Updatables = null;
            enemies = null;

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

        private DateTime lastTime = DateTime.Now;
        //game logic
        internal void Update()
        {
            float deltaTime = GetDeltaTime(DateTime.Now);
            Input();
            foreach(Updatable up in Updatables)
            {                
                up.Update(deltaTime);
            }
            lastTime = DateTime.Now;
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
                    case '\\':
                        Screen.RevealTiles(Vector2.Zero, 100);
                        break;
                }
            }
        }

        private float GetDeltaTime(DateTime startTime)
        {
            return (startTime.Ticks - lastTime.Ticks) / 10000000f;            
        }
    }
}