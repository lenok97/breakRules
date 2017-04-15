using UnityEngine;
using System;

namespace BreakRules
{
    public class GlobalRules
    {
        public bool visibly = true;
        public bool shell = false;
        public bool frozen = false;
        public bool canJump = true;
        public bool canShoot = true;
        public Vector3 gravityRotate = new Vector3(0, 180, 180);
        public float speedFactor = 1F;
        public float jumpFactor = 1F;
        public float gravityScale = 1F;
    }
}
