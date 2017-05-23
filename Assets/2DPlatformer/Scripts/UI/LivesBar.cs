using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesBar : MonoBehaviour
    {
        private Transform[] lives = new Transform[Player.MaxLives];
        private Player player;

        private void Awake()
        {
            player = FindObjectOfType<Player>();

            for (int i = 0; i < lives.Length; i++)
            {
                lives[i] = transform.GetChild(i);
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < lives.Length; i++)
            {
                if (i < player.Lives)
                    lives[i].gameObject.SetActive(true);
                else
                    lives[i].gameObject.SetActive(false);
            }
        }
    }
