using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameFrameWork
{
    public interface IGame
    {
        void RaisePlayerDieEvent(GameObjectPlayer player);

        void RaiseEnemyDieEvent(GameObjectPlayer pb);

        void RemoveImage(PictureBox pb);

        void WinGame();
    }
}
