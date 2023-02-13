using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GameFrameWork.Movements
{
    public class UsingKeyboard : IMovement
    {
        private int speed;
        private System.Drawing.Point boundary;
        private Keys leftKey;
        private Keys rightKey;

        public UsingKeyboard(int speed, System.Drawing.Point boundary, Keys leftKey, Keys rightKey)
        {
            this.speed = speed;
            this.boundary = boundary;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
        }

        public int Speed { get => speed; set => speed = value; }

        public System.Drawing.Point Move(System.Drawing.Point location)
        {
            if (EZInput.Keyboard.IsKeyPressed((EZInput.Key)leftKey))
            {
                if ((location.X - speed) >= 0)
                {
                    location.X -= Speed;
                }
            }

            if (EZInput.Keyboard.IsKeyPressed((EZInput.Key)rightKey))
            {
                if ((location.X + speed) <= boundary.X)
                {
                    location.X += Speed;
                }
            }

            return location;
        }
    }
}
