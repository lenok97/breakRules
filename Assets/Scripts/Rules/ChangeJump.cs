using UnityEngine;
using System;

namespace BreakRules
{
    class ChangeJump : IRule
    {
        public float changeFactor = -1F;

        public void Change()
        {
            Unit.globalRules.jumpFactor = changeFactor;
            Debug.Log(Unit.globalRules.jumpFactor);
        }

        public void Renew(Rules rules)
        {
            rules.jumpForce *= Unit.globalRules.jumpFactor;
            Debug.Log("up jump");
        }
    }
}