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
        GameObject door;
        [SerializeField]
        Sprite op_door;
        [SerializeField]
        Sprite cl_door;
        [SerializeField]
        Sprite lev_on;
        [SerializeField]
        Sprite lev_off;
        bool doorState;
        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.name == "Character")
            {
                if(doorState)
                {
                    doorState = false;
                    door.gameObject.GetComponent<SpriteRenderer>().sprite = cl_door;
                    lever.gameObject.GetComponent<SpriteRenderer>().sprite = lev_off;
                }
                else
                { 
                    doorState = true;
                    door.gameObject.GetComponent<SpriteRenderer>().sprite = op_door;
                    lever.gameObject.GetComponent<SpriteRenderer>().sprite = lev_on;
                }
            }        
        }
    }
}
