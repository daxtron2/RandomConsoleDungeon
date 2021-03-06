﻿
using System;
using System.Collections.Generic;
using System.Linq;

namespace RandomConsoleDungeon
{
    class Room
    {
        public Vector2 Position;
        public int roomSize;

        public List<Tile> doors;
        private Tile LDoor, RDoor, TDoor, BDoor;
        public Tile TL { get; private set; }
        public Tile TR { get; private set; }
        public Tile BL { get; private set; }
        public Tile BR { get; private set; }

        private List<int> xValues, yValues;
        static private Screen Screen;

        public Room()
        {
            if (Screen == null) Screen = Screen.Instance;
            doors = new List<Tile>();
        }

        public bool Connected { get; internal set; } = false;
        private void InitLists()
        {
            
            if (xValues == null || xValues.Count != 4)
            {
                xValues = new List<int>();
                xValues.Add(TL.Position.x);
                xValues.Add(TR.Position.x);
                xValues.Add(BL.Position.x);
                xValues.Add(BR.Position.x);
            }
            if(yValues == null || yValues.Count != 4)
            {
                yValues = new List<int>();
                yValues.Add(TL.Position.y);
                yValues.Add(TR.Position.y);
                yValues.Add(BL.Position.y);
                yValues.Add(BR.Position.y);
            }
        }
        internal bool IsInRoom(Vector2 pos)
        {
            InitLists();

            int Xmax = xValues.Max();
            int Xmin = xValues.Min();
            int Ymax = yValues.Max();
            int Ymin = yValues.Min();

            if ((pos.x <= Xmax) && (pos.x >= Xmin) && (pos.y <= Ymax) && (pos.y >= Ymin))
            {
                //is in room
                return true;
            }
            else
            {
                //not in room
                return false;
            }
        }

        internal void SetCorners(Tile tl, Tile tr, Tile bl, Tile br)
        {
            TL = tl;
            TR = tr;
            BL = bl;
            BR = br;

            TDoor = GetMidpoint(TL, TR);
            BDoor = GetMidpoint(BL, BR);
            LDoor = GetMidpoint(TL, BL);
            RDoor = GetMidpoint(TR, BR);

            if (TDoor is null || BDoor is null || LDoor is null || RDoor is null) return;

            TDoor.SetDoor(this);
            BDoor.SetDoor(this);
            LDoor.SetDoor(this);
            RDoor.SetDoor(this);

            SetFloor();
        }

        private void SetFloor()
        {
            for(int x = TL.Position.x+1; x < TR.Position.x; x++)
            {
                for(int y = TL.Position.y+1; y < BL.Position.y; y++)
                {
                    Screen.AccessTile(x, y)?.SetPath('\0');
                }
            }
        }

        private Tile GetMidpoint(Tile pt1, Tile pt2)
        {
            int xMidpoint = (pt1.Position.x + pt2.Position.x) / 2;
            int yMidpoint = (pt1.Position.y + pt2.Position.y) / 2;

            return Screen.AccessTile(xMidpoint, yMidpoint);
        }
    }
}