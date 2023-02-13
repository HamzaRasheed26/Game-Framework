using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GameFrameWork.Fire
{
    public interface IFire
    {

        PictureBox getPb();

        IFire createFire(Point location);

        Point Move();

        bool RemoveFire();

    }
}
