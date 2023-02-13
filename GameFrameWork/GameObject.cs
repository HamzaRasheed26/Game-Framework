using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using GameFrameWork.Movements;
using GameFrameWork.Enums;

namespace GameFrameWork
{
    public class GameObject
    {
        private PictureBox pb;
        private IMovement objectMovement;
        private int value;
        private ObjectTypes objType;

        private EventHandler onLeftMove;
        private EventHandler onRightMove;

        public GameObject(Image img, string name, int left, int top, ObjectTypes tag, IMovement objectMovement)
        {
            this.objectMovement = objectMovement;

            Pb = new PictureBox();
            Pb.Name = name;
            Pb.Image = img;
            Pb.Width = img.Width;
            Pb.Height = img.Height;
            Pb.BackColor = Color.Transparent;
            Pb.Left = left;
            Pb.Top = top;
            Enum t = tag;
            Pb.Tag = t.ToString();
            objType = tag;
            value = 0;
        }

        public GameObject(Image img, string name, int left, int top, Point size, PictureBoxSizeMode sizeMode, ObjectTypes tag, IMovement objectMovement)
        {
            this.objectMovement = objectMovement;

            Pb = new PictureBox();
            Pb.Name = name;
            Pb.Image = img;
            Pb.Width = img.Width;
            Pb.Height = img.Height;
            Pb.BackColor = Color.Transparent;
            Pb.Left = left;
            Pb.Top = top;
            Enum t = tag;
            Pb.Tag = t.ToString();
            Pb.Size = new Size(size.X, size.Y);
            Pb.SizeMode = sizeMode;
            objType = tag;
            value = 0;
        }

        public PictureBox Pb { get => pb; set => pb = value; }
        public EventHandler OnLeftMove { get => onLeftMove; set => onLeftMove = value; }
        public EventHandler OnRightMove { get => onRightMove; set => onRightMove = value; }
        public int Value { get => value; set => this.value = value; }
        public ObjectTypes ObjType { get => objType; set => objType = value; }

        public virtual void changeImage(Image img)
        {
            Pb.Image = img;
            Pb.Width = img.Width;
            Pb.Height = img.Height;
        }

        public virtual void createFire( )
        {
            
        }

        public virtual void update()
        {
            Point p = pb.Location;
            pb.Location  = objectMovement.Move(pb.Location);

            if (p.X > pb.Location.X ) 
            {
                OnLeftMove?.Invoke(this, EventArgs.Empty);

            }
            else if(p.X < pb.Location.X )
            {
                OnRightMove?.Invoke(this, EventArgs.Empty);
            }
        }

    }
}
