using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class RandomWalker : Enemy
    {
        public RandomWalker(int _hp, Vector2 pos) : base(_hp, pos){}

        protected override void DoMovementType()
        {
            var dir = Vector2.GetRandomDirection();
            MoveObject(dir);
        }
    }
}
