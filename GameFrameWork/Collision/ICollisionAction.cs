using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork.Collision
{
    public interface ICollisionAction
    {
        void performAction(IGame game, GameObject source1, GameObject source2);
    }
}
