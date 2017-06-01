using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : MonoBehaviour, ICommand
{
    private float jumpSiderun = 0.0F;
    Unit unit;
    Rigidbody2D rb2d;
    public void TakeUnit(Unit unit)
    {
        if (this.unit != null && !this.unit.CompareTag("Player"))
            this.unit.TargetVelosity = Vector2.zero;
        this.unit = unit;
        rb2d = unit.Rb2D;
    }
    void Start()
    {
        Controller.Instance.AddCommand(this);
    }

    void Update()
    {
        unit.TargetVelosity = new Vector2(Input.GetAxis("Horizontal"), 0);

        if (Input.GetButtonDown("Jump") && unit.Grounded)
        {
            rb2d.velocity = Vector2.up*unit.GravityDirection * unit.JumpVelosity;
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (rb2d.velocity.y > 0)
            {
                rb2d.velocity = rb2d.velocity * 0.5f;
            }
        }
        DoNotJumpBackwards();
    }
    /// <summary>
    /// Не дает юниту во время прыжка менять направление полета
    /// </summary>
    private void DoNotJumpBackwards()
    {
        if (!unit.Grounded)
        {
            if (jumpSiderun > 0f && unit.TargetVelosity.x < 0f)
                unit.TargetVelosity = new Vector2(jumpSiderun / 4, 0);
            else if (jumpSiderun < 0f && unit.TargetVelosity.x > 0f)
                unit.TargetVelosity = new Vector2(jumpSiderun / 4, 0);
            else if (jumpSiderun == 0f)
                jumpSiderun = unit.TargetVelosity.x;
        }
        else jumpSiderun = 0;
    }
}
