using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Enemy : AttackableObject
    {
        public Enemy(int hp, Vector2 startPos = null) : base()
        {
            if (startPos == null) Position = Vector2.Zero;
            Position = startPos;

            Screen.AccessTile(Position.x, Position.y).SetGameObject(this);

            DisplayChar = '$';
            Health = hp;

            DisplayObject();
        }

        private float movementTimer = 0f;
        private int movementTimerInc = 0;
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            movementTimer += deltaTime;
            if(movementTimer >= 1f)
            {
                movementTimerInc++;
                ConsoleHelpers.WriteString($"MVMT: {movementTimerInc}");
                movementTimer = 0f;
            }
        }

        public override void PostUpdate()
        {
            if (IsAlive)
            {
                base.PostUpdate();
                //DisplayChar++;
                DisplayObject();                
            }
        }

        internal override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
            ConsoleHelpers.WriteString($"Enemy Took {dmg} damage");
        }
    }
}
