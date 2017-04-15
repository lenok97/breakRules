using UnityEngine;
using System;

namespace BreakRules
{
    class ChangeShell : IRule
    {

        public void Change()
        {
            Unit.globalRules.shell = !Unit.globalRules.shell;
            Debug.Log(Unit.globalRules.shell);
        }

        public void Renew(Rules rules)
        {
            rules.shell = Unit.globalRules.shell;
            Debug.Log("up shell");
        }
    }
}