using UnityEngine;
using System;

namespace BreakRules
{
    class ChangeGravity : IRule
    {
        public float changeFactor = -1F;

        public void Change()
        {
            Unit.globalRules.gravityScale= changeFactor;
            Debug.Log(Unit.globalRules.gravityScale);
        }

        public void Renew(Rules rules)
        {
            rules.gravityScale *= Unit.globalRules.gravityScale;
            Debug.Log("up gravity");
        }
    }
}
