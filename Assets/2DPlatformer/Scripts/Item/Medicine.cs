using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private int number = 5;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Player player = collider.GetComponent<Player>();
        if (player)
        {
            for (int i=0; i<number; i++)
                player.Lives ++;
            Destroy(gameObject);
        }
     }
}

