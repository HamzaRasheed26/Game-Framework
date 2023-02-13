using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameFrameWork.Collision
{
    public class CollisionWithChest : ICollisionAction
    {
        Image img;

        public CollisionWithChest(Image img)
        {
            this.img = img;
        }

        public void performAction(IGame game, GameObject source1, GameObject source2)
        {
            if (source1.Pb.Bounds.IntersectsWith(source2.Pb.Bounds))
            {
                if(source1.ObjType == Enums.ObjectTypes.Chest)
                {
                    source1.Pb.Image = img;
                    source1.Pb.Height = img.Height;
                    source1.Pb.Width = img.Width;
                }
              /*  else
                {
                    source2.Pb.Image = img;
                    source2.Pb.Height = img.Height;
                    source2.Pb.Width = img.Width;
                }*/

                game.WinGame();
            }
        }
    }
}
