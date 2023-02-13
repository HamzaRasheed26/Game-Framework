using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameFrameWork.Enums;
using GameFrameWork.Movements;

namespace GameFrameWork.Fire
{
    public class PLayerFire : IFire
    {
        private PictureBox pbFire;
        private Image fireImgLeft;
        private Image fireImageRight;
        private MoveNames direction;
        private ObjectTypes tag;
        private int speed;
        private Keys leftFire;
        private Keys rightFire;
        private Point boundary;

        public PLayerFire(Image fireImgLeft,Image fireImageRight, ObjectTypes tag,int speed, Keys leftFire, Keys rightFire, Point boundary)
        {
            this.fireImgLeft = fireImgLeft;
            this.fireImageRight = fireImageRight;
            this.tag = tag;
            this.speed = speed;
            this.leftFire = leftFire;
            this.rightFire = rightFire;
            this.boundary = boundary;
        }

        public PLayerFire(PLayerFire fire)
        {
            this.pbFire = fire.PbFire;
            this.fireImgLeft = fire.fireImgLeft;
            this.fireImageRight = fire.fireImageRight;
            this.tag = fire.tag;
            this.speed = fire.speed;
            this.leftFire = fire.leftFire;
            this.rightFire = fire.rightFire;
            this.direction = fire.direction;
            this.boundary = fire.boundary;
        }

        public PictureBox PbFire { get => pbFire; set => pbFire = value; }
        public Image FireImgLeft { get => fireImgLeft; set => fireImgLeft = value; }
        public Image FireImageRight { get => fireImageRight; set => fireImageRight = value; }
        public MoveNames Direction { get => direction; set => direction = value; }
        public ObjectTypes Tag { get => tag; set => tag = value; }
        public int Speed { get => speed; set => speed = value; }
        public Keys LeftFire { get => leftFire; set => leftFire = value; }
        public Keys RightFire { get => rightFire; set => rightFire = value; }
        
        public PictureBox getPb()
        {
            return pbFire;
        }

        public IFire createFire(Point location)
        {
            if (EZInput.Keyboard.IsKeyPressed((EZInput.Key)leftFire))
            {
                makeFire(fireImgLeft, MoveNames.Left, location);
                return new PLayerFire(this);
            }
            else if (EZInput.Keyboard.IsKeyPressed((EZInput.Key)rightFire))
            {
                makeFire(fireImageRight, MoveNames.Right, location);
                return new PLayerFire(this);
            }
            return null;
        }

        private void makeFire(Image img, MoveNames direction, Point location)
        {
            pbFire = new PictureBox();
            pbFire.Left = location.X;
            pbFire.Top = location.Y + 30;
            pbFire.Image = img;
            pbFire.Height = img.Height;
            pbFire.Width = img.Width;
            pbFire.Tag = tag.ToString();
            pbFire.BackColor = Color.Transparent;
            pbFire.BringToFront();
            this.direction = direction;
        }

        public Point Move( )
        {
            if (direction == MoveNames.Left )
            {
                pbFire.Left -= speed;
            }
            else if (direction == MoveNames.Right)
            {
                pbFire.Left += speed;
            }

            return new Point(pbFire.Left, pbFire.Top);
        }

        public bool RemoveFire()
        {
            if (pbFire.Left <= 0)
            {
                return true;
            }
            else if ((pbFire.Left + pbFire.Width) > boundary.X)
            {
                return true;
            }
            else if (pbFire.Top <= 0)
            {
                return true;
            }
            else if ((pbFire.Top + pbFire.Height) > boundary.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
