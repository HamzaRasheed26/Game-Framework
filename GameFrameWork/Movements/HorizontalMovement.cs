using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using GameFrameWork.Enums;

namespace GameFrameWork.Movements
{
    public class HorizontalMovement : IMovement
    {
        private int speed;
        private Point boundary;
        private MoveNames direction;

        public HorizontalMovement(int speed, Point boundary, MoveNames direction)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.direction = direction;
        }

        public Point Move(Point location)
        {

            if(location.X + speed <= boundary.Y && direction == MoveNames.Right)
            {
                location.X += speed;
            }
            else
            {
                direction = MoveNames.Left;
            }

            if (location.X - speed >= boundary.X && direction == MoveNames.Left)
            {
                location.X -= speed;
            }
            else
            {
                direction = MoveNames.Right;
            }

            return location;
        }
    }
}
