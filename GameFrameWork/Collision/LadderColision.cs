using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork.Collision
{
    public class LadderColision : ICollisionAction
    {
        public void performAction(IGame game, GameObject source1, GameObject source2)
        {
            if (source1.Pb.Bounds.IntersectsWith(source2.Pb.Bounds))
            {
                if (EZInput.Keyboard.IsKeyPressed(EZInput.Key.UpArrow))
                {
                    if (source1.ObjType == Enums.ObjectTypes.Player)
                    {
                        source1.Pb.Top -= 10;
                    }
                    else
                    {
                        source2.Pb.Top -= 10;
                    }
                }
            }

        }
    }
}
