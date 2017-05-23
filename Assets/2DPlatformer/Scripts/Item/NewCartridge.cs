using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCartridge : MonoBehaviour
{
     [SerializeField]
     private int number = 15;

     [SerializeField]
     private BulletType type = BulletType.Bomb;

      private void OnTriggerEnter2D(Collider2D collider)
      {
         Player player = collider.GetComponent<Player>();
         if (player&&Inventory.cartridge.ContainsKey(type))
         {
             Inventory.cartridge[type] += number;
             Destroy(gameObject);
         }
      }
}
