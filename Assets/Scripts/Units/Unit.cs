using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class Unit : MonoBehaviour
    {

        public bool canShoot = true;
        public SpriteRenderer sprite;
        public static GlobalRules globalRules = new GlobalRules();
        public Rules rules;
        public List<IRule> ruleController = new List<IRule>();

        public virtual void ReceiveDamage()
        {
            Die();
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        public void Unfroze()
        {
            rules.frozen = false;
        }

        public void CanShoot()
        {
            canShoot = true;
        }
    }
}
