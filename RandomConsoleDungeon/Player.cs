using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Player : MovableObject
    {
        public Player(Screen _screen)
        {
            DisplayChar = '@';
            screen = _screen;
            Position = new Vector2(0,0);
            DisplayObject();
        }
    }
}
