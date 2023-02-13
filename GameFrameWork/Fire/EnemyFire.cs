using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameFrameWork.Enums;

namespace GameFrameWork.Fire
{
    public class EnemyFire : IFire
    {
        private GameObject player;
        private EventHandler onPlayerComingNear;

        private PictureBox pbFire;
        private Image fireImgLeft;
        private Image fireImageRight;
        private MoveNames direction;
        private ObjectTypes tag;
        private int speed;
        private Point boundary;

        private int wait;

        public EnemyFire(Image fireImgLeft, Image fireImageRight, ObjectTypes tag, int speed, Point boundary, GameObject player)
        {
            this.fireImgLeft = fireImgLeft;
            this.fireImageRight = fireImageRight;
            this.tag = tag;
            this.speed = speed;
            this.boundary = boundary;
            this.player = player;
            wait = 0;
        }

        public EnemyFire(EnemyFire fire)
        {
            this.pbFire = fire.PbFire;
            this.fireImgLeft = fire.fireImgLeft;
            this.fireImageRight = fire.fireImageRight;
            this.tag = fire.tag;
            this.speed = fire.speed;
            this.direction = fire.direction;
            this.boundary = fire.boundary;
            this.Player = fire.Player;
            wait = 0;
        }

        public PictureBox PbFire { get => pbFire; set => pbFire = value; }
        public Image FireImgLeft { get => fireImgLeft; set => fireImgLeft = value; }
        public Image FireImageRight { get => fireImageRight; set => fireImageRight = value; }
        public MoveNames Direction { get => direction; set => direction = value; }
        public ObjectTypes Tag { get => tag; set => tag = value; }
        public int Speed { get => speed; set => speed = value; }
        public GameObject Player { get => player; set => player = value; }
        public EventHandler OnPlayerComingNear { get => onPlayerComingNear; set => onPlayerComingNear = value; }

        public IFire createFire(Point location)
        {
            if (wait == 0)
            {
                    if (location.X > player.Pb.Left)
                    {
                        makeFire(fireImgLeft, MoveNames.Left, location);
                    }

                    if (location.X < player.Pb.Right)
                    {
                        makeFire(fireImgLeft, MoveNames.Right, location);
                    }

                    wait = 15;
                    return new EnemyFire(this);
                
            }
            wait--;

            return null;
        }

        public PictureBox getPb()
        {
            return pbFire;
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

        public Point Move()
        {
            if (direction == MoveNames.Left)
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
