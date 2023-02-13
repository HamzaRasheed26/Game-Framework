using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork.Collision
{
    public class EnemyFireCollision : ICollisionAction
    {
        private int minusHealth;

        public EnemyFireCollision(int minusHealth)
        {
            this.minusHealth = minusHealth;
        }

        private void takeAction(IGame game, GameObjectPlayer player,GameObjectPlayer enemy, int i)
        {
            if (player.HealthBar.Value - minusHealth >= 0)
            {
                player.HealthBar.Value -= minusHealth;
                game.RemoveImage(enemy.Fires[i].getPb());
                enemy.Fires.Remove(enemy.Fires[i]);
            }
            else
            {
               game.RaisePlayerDieEvent(player);
                
            }

        }

        public void performAction(IGame game, GameObject source1, GameObject source2)
        {
            GameObjectPlayer player;
            GameObjectPlayer enemy;
            if (source1.ObjType == Enums.ObjectTypes.Player)
            {
                player = source1 as GameObjectPlayer;
                enemy = source2 as GameObjectPlayer;
            }
            else
            {
                player = source2 as GameObjectPlayer;
                enemy = source1 as GameObjectPlayer;
            }

            for (int i = 0; i < enemy.Fires.Count; i++)
            {
                if (enemy.Fires[i].getPb() != null)
                {
                    if (enemy.Fires[i].getPb().Bounds.IntersectsWith(player.Pb.Bounds))
                    {
                        for (int x = 0; x < player.Fires.Count; x++)
                        {
                            game.RemoveImage(player.Fires[x].getPb());
                        }
                        takeAction(game, player, enemy, i);
                    }
                }
            }
        }
    }
}
