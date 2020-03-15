using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class AttackableObject : MovableObject
    {
        public int Health { get; protected set; }
        public bool IsAlive { get; protected set; } = true;

        internal virtual void TakeDamage(int dmg)
        {
            if (!IsAlive) return;
            if (Health - dmg <= 0)
                Die();
            else
                Health -= dmg;
        }

        protected virtual void Die()
        {
            Health = 0;
            IsAlive = false;
            Screen.AccessTile(Position.x, Position.y).ObjectDied();
        }
    }
}
