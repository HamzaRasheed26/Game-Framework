using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameWork.Enums;

namespace GameFrameWork.Collision
{
    public class CollisionClass
    {
        private ObjectTypes g1;
        private ObjectTypes g2;
        private ICollisionAction behaviour;

        public CollisionClass(ObjectTypes g1, ObjectTypes g2, ICollisionAction behaviour)
        {
            this.g1 = g1;
            this.g2 = g2;
            this.behaviour = behaviour;
        }

        public ObjectTypes G1 { get => g1; set => g1 = value; }
        public ObjectTypes G2 { get => g2; set => g2 = value; }
        public ICollisionAction Behaviour { get => behaviour; set => behaviour = value; }
    }
}
