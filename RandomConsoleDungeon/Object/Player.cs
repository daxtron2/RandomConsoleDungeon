using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomConsoleDungeon
{
    class Player : AttackableObject
    {
        private const int REVEAL_RADIUS = 3;

        private int baseAttackValue = 1;
        private List<int> itemAttackValues; //for testing purposes, will be own classes
        public Player(Vector2 startPos = null) : base()
        {
            if (startPos == null) Position = Vector2.Zero;
            Position = startPos;

            Screen.AccessTile(Position.x, Position.y).SetGameObject(this);
            

            DisplayChar = '@';            

            itemAttackValues = new List<int>();

            Screen.RevealTiles(Position, 10);
            DisplayObject();

        }

        internal override void MoveObject(Vector2 dir)
        {
            ConsoleHelpers.WriteString($"Player moved in direction: {dir}");
            if (CheckAttackable(dir, out Enemy enemy))
            {
                Attack(enemy);
            }
            base.MoveObject(dir);
            Screen.RevealTiles(Position, REVEAL_RADIUS);
            DisplayObject();
        }

        private void Attack(Enemy enemy)
        {
            enemy.TakeDamage(GetAttackValue());
        }

        private int GetAttackValue()
        {
            int attValue = baseAttackValue;
            foreach(int i in itemAttackValues)
            {
                attValue += i;
            }
            return attValue;
        }

        private bool CheckAttackable(Vector2 dir, out Enemy enemy)
        {
            GameObject go = Screen.AccessTile(Position.x + dir.x, Position.y + dir.y)?.GameObject;
            if (go is Enemy)
            {
                enemy = go as Enemy;
                return true;
            }
            else
            {
                enemy = null;
                return false;
            }
        }
    }
}
