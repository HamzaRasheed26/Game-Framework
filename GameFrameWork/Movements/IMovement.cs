using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameFrameWork.Movements
{
    public interface IMovement
    {
        Point Move(Point location);
    }
}
