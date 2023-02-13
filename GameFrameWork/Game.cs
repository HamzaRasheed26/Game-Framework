using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using GameFrameWork.Movements;
using GameFrameWork.Enums;
using GameFrameWork.Fire;
using GameFrameWork.Collision;

namespace GameFrameWork
{
    public class Game : IGame
    {
        private List<GameObject> gameObjectList;
        private List<CollisionClass> collisions;

        private EventHandler onGameObjectAdded;
        private EventHandler onGameObjectRemoved;
        private EventHandler onPictureBoxRemoved;
        private EventHandler onProgressBarAdded;
        private EventHandler onLeftMove;
        private EventHandler onRightMove;
        private EventHandler onPlayerDie;
        private EventHandler onEnemyDie;
        private EventHandler onWinGame;

        public Game()
        {
            gameObjectList = new List<GameObject>();
            collisions = new List<CollisionClass>();
        }

        public List<GameObject> GameObjectList { get => gameObjectList; set => gameObjectList = value; }
        public EventHandler OnGameObjectAdded { get => onGameObjectAdded; set => onGameObjectAdded = value; }
        public EventHandler OnLeftMove { get => onLeftMove; set => onLeftMove = value; }
        public EventHandler OnRightMove { get => onRightMove; set => onRightMove = value; }
        public EventHandler OnGameObjectRemoved { get => onGameObjectRemoved; set => onGameObjectRemoved = value; }
        public EventHandler OnPlayerDie { get => onPlayerDie; set => onPlayerDie = value; }
        public EventHandler OnEnemyDie { get => onEnemyDie; set => onEnemyDie = value; }
        public EventHandler OnPictureBoxRemoved { get => onPictureBoxRemoved; set => onPictureBoxRemoved = value; }
        public EventHandler OnProgressBarAdded { get => onProgressBarAdded; set => onProgressBarAdded = value; }
        public EventHandler OnWinGame { get => onWinGame; set => onWinGame = value; }

        public void addGameObject(Image img, string name, int left, int top, ObjectTypes tag, IMovement objectMovement)
        {
            GameObject gameObject = new GameObject(img, name, left, top, tag, objectMovement);
            gameObject.OnLeftMove += player_OnLeftMove;
            gameObject.OnRightMove += player_OnRightMove;
            gameObjectList.Add(gameObject);
            OnGameObjectAdded?.Invoke(gameObject.Pb, EventArgs.Empty);
        }

        public void addGameObjectPlayer(Image img, string name, int left, int top, ObjectTypes tag, IMovement objectMovement, int healthValue, IFire fire )
        {
            GameObjectPlayer gameObject = new GameObjectPlayer(img, name, left, top, tag, objectMovement, healthValue, fire);
            gameObject.OnLeftMove += player_OnLeftMove;
            gameObject.OnRightMove += player_OnRightMove;
            gameObject.OnProgressBarAdd += player_OnProgressBarAdd;
            gameObject.OnFireAdded += onFireAddedFromGameObject;
            gameObject.OnFireRemove += onFireRemoveFormGameObject;
            gameObject.makeHealthBar();
            gameObjectList.Add(gameObject);
            OnGameObjectAdded?.Invoke(gameObject.Pb, EventArgs.Empty);
        }

        private void player_OnProgressBarAdd(object sender, EventArgs e)
        {
            onProgressBarAdded?.Invoke(sender, EventArgs.Empty);
        }

        public void RaisePlayerDieEvent(GameObjectPlayer plyerGameObject)
        {
            OnPlayerDie?.Invoke(plyerGameObject, EventArgs.Empty);

            this.gameObjectList.Remove(plyerGameObject);
            
        }

        public void RaiseEnemyDieEvent(GameObjectPlayer plyerGameObject)
        {
            OnEnemyDie?.Invoke(plyerGameObject, EventArgs.Empty);
            
                this.gameObjectList.Remove(plyerGameObject);
            
        }

        public void RemoveImage(PictureBox pb)
        {
            onPictureBoxRemoved?.Invoke(pb, EventArgs.Empty);
        }

        private void onFireRemoveFormGameObject(object sender, EventArgs e)
        {
            OnGameObjectRemoved?.Invoke(sender, e);
        }

        private void onFireAddedFromGameObject(object sender, EventArgs e)
        {
            OnGameObjectAdded?.Invoke(sender, e);
        }


        public void addGameObject(Image img, string name, int left, int top, Point size, PictureBoxSizeMode sizeMode, ObjectTypes tag  , IMovement objectMovement)
        {
            GameObject gameObject = new GameObject(img, name, left, top , size, sizeMode, tag, objectMovement);
            gameObject.OnLeftMove += player_OnLeftMove;
            gameObject.OnRightMove += player_OnRightMove;
            gameObjectList.Add(gameObject);
            OnGameObjectAdded?.Invoke(gameObject.Pb, EventArgs.Empty);
        }

        public void update()
        {
            foreach (GameObject obj in gameObjectList)
            {
                obj.update();
            }
        }

        public GameObject FindGameObject(string name)
        {
            foreach(GameObject obj in gameObjectList)
            {
                if(obj.Pb.Name == name)
                {
                    return obj;
                }
            }
            return null;
        }

        public void createFire()
        {
            foreach (GameObject obj in gameObjectList)
            {
                if(obj.ObjType == ObjectTypes.Player || obj.ObjType == ObjectTypes.Enemy)
                {
                    obj.createFire( );
                }
            }
        }

        private void player_OnLeftMove(object sender, EventArgs e)
        {
            OnLeftMove?.Invoke(sender, EventArgs.Empty);
        }

        private void player_OnRightMove(object sender, EventArgs e)
        {
            OnRightMove?.Invoke(sender, EventArgs.Empty);
        }

        public void bringToFrontGameObject(ObjectTypes tag)
        {
            GameObject player = GameObjectList.Find(i => (string)i.Pb.Tag == tag.ToString());
            player.Pb.BringToFront();
        }

        public void gravityForPlayer(string name, int speed)
        {
            int count = 0;
            GameObject player = GameObjectList.Find(i => i.Pb.Name == name);

            foreach (GameObject obj in this.GameObjectList )
            {
                if (obj.ObjType == ObjectTypes.Platform)
                {
                    if (player.Pb.Bounds.IntersectsWith(obj.Pb.Bounds))
                    {
                        count++;
                    }
                }
            }

            if (count == 0)
            {
                player.Pb.Top += speed;
            }
        }

        public void addCollision(CollisionClass collision)
        {
            collisions.Add(collision);
        }

        public void detectCollison()
        {
            for(int x = 0; x < GameObjectList.Count; x++)
            {
                for(int i = 0; i < GameObjectList.Count; i++)
                {

                    foreach (CollisionClass c in collisions)
                    {
                        if (gameObjectList[x].ObjType == c.G1 && gameObjectList[i].ObjType == c.G2)
                        {
                            c.Behaviour.performAction(this, gameObjectList[x], gameObjectList[i]);
                        }
                    }
                    
                }
            }
        }

        public void WinGame()
        {
            OnWinGame?.Invoke(this, EventArgs.Empty);
        }
    }
}
