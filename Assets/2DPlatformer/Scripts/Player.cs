using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Unit
{
    private Animator animator;
    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Animator();
    }

    private void Animator()
    {
        //при смене направления движения, сменяет направление спрайта
        bool flipSprite = (spriteRenderer.flipX ? (TargetVelosity.x > 0.01f) : (TargetVelosity.x < 0.0f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", Grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x));
    }
}
