using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BreakRules
{
    public class OpenableItems : MonoBehaviour  
    {
        [SerializeField]
        public GameObject item;
        public virtual void Disappear()
        {
            item.GetComponent<BoxCollider2D>().isTrigger = false;
            item.GetComponent<Door>().Disappear();
        }
        public virtual void Appear()
        {
            item.GetComponent<BoxCollider2D>().isTrigger = true;
            item.GetComponent<Door>().Appear();
        }
       
    }
}
