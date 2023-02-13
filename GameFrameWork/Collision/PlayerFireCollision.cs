using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork.Collision
{
    public class PlayerFireCollision : ICollisionAction
    {
        private int minusHealth;

        public PlayerFireCollision(int minusHealth)
        {
            this.minusHealth = minusHealth;
        }
        private void takeAction(IGame game, GameObjectPlayer player, GameObjectPlayer enemy, int i)
        {
            if (enemy.HealthBar.Value - minusHealth >= 0)
            {
                enemy.HealthBar.Value -= minusHealth;
                game.RemoveImage(player.Fires[i].getPb());
                player.Fires.Remove(player.Fires[i]);
            }
            else
            {
                game.RaiseEnemyDieEvent(enemy);

            }
        }

        public void performAction(IGame game, GameObject source1, GameObject source2)
        {
            GameObjectPlayer player;
            GameObjectPlayer enemy;
            if(source1.ObjType == Enums.ObjectTypes.Player)
            {
                player = source1 as GameObjectPlayer;
                enemy = source2 as GameObjectPlayer;
            }
            else
            {
                player = source2 as GameObjectPlayer;
                enemy = source1 as GameObjectPlayer;
            }

            for(int i = 0; i < player.Fires.Count; i++)
            {
                if (player.Fires[i].getPb().Bounds.IntersectsWith(enemy.Pb.Bounds))
                {
                    for(int x =0; x < enemy.Fires.Count; x++)
                    {
                        game.RemoveImage(enemy.Fires[x].getPb());
                    }
                    takeAction(game, player, enemy, i);
                   /* game.RaiseEnemyDieEvent(enemy);
                    game.RemoveImage(player.Fires[i].getPb());
                    player.Fires.Remove(player.Fires[i]);*/
                }
            }

        }
    }
}
