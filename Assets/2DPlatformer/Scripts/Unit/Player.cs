using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : Unit
{
    private Animator animator;
    //public Inventory inventory;

    [SerializeField]
    private InventoryBar inventoryBar;

    void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        //inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        Animator();
    }

    public override void ReceiveDamage()
    {
        Lives--;
        Rb2D.velocity = Vector3.zero;
        Rb2D.AddForce(transform.up * 3.0F, ForceMode2D.Impulse);
        if (Lives<=0)
            EventManager.Instance.PostNotification(EVENT_TYPE.UNIT_DIE);
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
