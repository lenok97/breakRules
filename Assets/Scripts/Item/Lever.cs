using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class Lever : MonoBehaviour
    {
        [SerializeField]
        GameObject lever;
        [SerializeField]
        Sprite lev_on;
        [SerializeField]
        Sprite lev_off;
        bool leverState;
        [SerializeField]
        GameObject item;
        private void OnTriggerEnter2D(Collider2D collider)
        {

            if (collider.gameObject.name == "Character")
            {
                if (leverState)
                {
                    leverState = false;
                    lever.gameObject.GetComponent<SpriteRenderer>().sprite = lev_off;
                    item.GetComponent<OpenableItems>().Disappear();
                }
                else
                {
                    leverState = true;
                    lever.gameObject.GetComponent<SpriteRenderer>().sprite = lev_on;
                    item.GetComponent<OpenableItems>().Appear();
                }
            }
        }
    }
}
      
