using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            using(Game game = new Game())
            {
                game.Init();
                while (game.Running)
                {
                    game.PreUpdate();
                    game.Update();
                    game.PostUpdate();
                }
            }
        }        
    }
}
