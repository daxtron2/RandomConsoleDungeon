using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Player : MovableObject
    {
        public Player(Screen _screen, Vector2 startPos = null)
        {
            DisplayChar = '@';
            screen = _screen;
            if (startPos == null) Position = Vector2.Zero;
            Position = startPos;
            DisplayObject();
        }
    }
}
