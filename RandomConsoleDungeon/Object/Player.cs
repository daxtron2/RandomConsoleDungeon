﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Player : MovableObject
    {
        private const int REVEAL_RADIUS = 3;
        public Player(Vector2 startPos = null) : base()
        {
            DisplayChar = '@';            
            if (startPos == null) Position = Vector2.Zero;
            Position = startPos;

            Screen.RevealTiles(Position, 10);
            DisplayObject();

        }

        internal override void MoveObject(Vector2 dir)
        {
            base.MoveObject(dir);

            Screen.RevealTiles(Position, REVEAL_RADIUS);
            DisplayObject();
        }
    }
}
