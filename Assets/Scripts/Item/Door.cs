using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BreakRules
{
    public class Door : OpenableItems 
    {
        [SerializeField]
        public Sprite op_door;
        [SerializeField]
        public Sprite cl_door;
        public override void Disappear()
        {
            item.gameObject.GetComponent<SpriteRenderer>().sprite = cl_door;
        }
        public override void Appear()
        {
            item.gameObject.GetComponent<SpriteRenderer>().sprite = op_door;
        }

    }
}
