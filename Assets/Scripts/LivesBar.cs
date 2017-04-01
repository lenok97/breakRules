using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BreakRules
{
    public class LivesBar : MonoBehaviour
    {
        private Transform[] lives = new Transform[Character.MaxLives];
        private Character character;

        private void Awake()
        {
            character = FindObjectOfType<Character>();

            for (int i = 0; i < lives.Length; i++)
            {
                lives[i] = transform.GetChild(i);
            }
        }

        public void Refresh()
        {
            for (int i = 0; i < lives.Length; i++)
            {
                if (i < character.Lives)
                    lives[i].gameObject.SetActive(true);
                else
                    lives[i].gameObject.SetActive(false);
            }
        }
    }
}
