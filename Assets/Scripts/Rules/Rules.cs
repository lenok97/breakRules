using UnityEngine;
using System;

namespace BreakRules
{
    public class Rules
    {
        public bool visibly = true;
        public bool shell = false;
        public Vector3 gravityRotate = new Vector3(0, 180, 180);
        public bool frozen = false;
        [SerializeField]
        public float speed = 3.0F;
        [SerializeField]
        public float jumpForce = 15.0F;
        public bool canJump = true;
        public float gravityScale =3F;
    }
}