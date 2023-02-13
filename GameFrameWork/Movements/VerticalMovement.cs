using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GameFrameWork.Movements
{
    public class VerticalMovement : IMovement
    {
        private int speed;
        private Point boundary;
        private string direction;
        private int offset = 90;

        public VerticalMovement(int speed, Point boundary, string direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }

        public Point Move(Point location)
        {
            if ((location.X + offset) >= boundary.X)
            {
                direction = "top";
            }
            else if (location.X + speed <= 0)
            {
                direction = "right";
            }

            if (direction == "top")
            {
                location.Y -= speed;
            }
            else
            {
                location.Y += +speed;
            }

            return location;
        }
    }
}
