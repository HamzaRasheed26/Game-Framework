using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using GameFrameWork.Fire;
using GameFrameWork.Enums;
using GameFrameWork.Movements;

namespace GameFrameWork
{
    public class GameObjectPlayer : GameObject
    {
        private IFire fire;
        private List<IFire> fires;
        private ProgressBar healthBar;
        private EventHandler onFireAdded;
        private EventHandler onProgressBarAdd;
        private EventHandler onFireRemove;

        public EventHandler OnFireAdded { get => onFireAdded; set => onFireAdded = value; }
        public EventHandler OnProgressBarAdd { get => onProgressBarAdd; set => onProgressBarAdd = value; }
        public EventHandler OnFireRemove { get => onFireRemove; set => onFireRemove = value; }
        public List<IFire> Fires { get => fires; set => fires = value; }
        public ProgressBar HealthBar { get => healthBar; set => healthBar = value; }

        public GameObjectPlayer( Image img, string name, int left, int top, ObjectTypes tag, IMovement objectMovement, int healthValue, IFire fire) : base(img, name, left, top, tag, objectMovement)
        {
            this.fire = fire;
            fires = new List<IFire>();
        }

        public void makeHealthBar()
        {
            healthBar = new ProgressBar();
            this.healthBar.Location = new Point(Pb.Left, Pb.Top - 20);
            this.healthBar.Name = "progressBar1";
            this.healthBar.Size = new System.Drawing.Size(89, 16);
            this.healthBar.Value = 100;
            this.healthBar.BringToFront();
            OnProgressBarAdd?.Invoke(healthBar, new EventArgs());
        }

        public override void createFire( )
        {
             
            IFire f = fire.createFire(new Point(Pb.Left, Pb.Top)); 
            if (f != null)
            {
                fires.Add(f);
                
                OnFireAdded?.Invoke(f.getPb(), new EventArgs());
            }
        }

      
        public void moveFires()
        {
            foreach(IFire f in fires)
            {
                f.getPb().Location = f.Move();

            }
        }

        public void removeFires()
        {
            for (int i = 0; i < fires.Count; i++)
            {
                if (fires[i].RemoveFire())
                {
                    onFireRemove?.Invoke(fires[i].getPb(), new EventArgs());
                    fires.Remove(fires[i]);
                }

            }
        }

        public void moveHealthBar()
        {
            healthBar.Left = Pb.Left;
            healthBar.Top = Pb.Top - 20;
        }

        public override void update()
        {
            base.update();
            moveFires();
            removeFires();
            moveHealthBar();
        }
    }

}
