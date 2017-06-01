using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BoxCommand : MonoBehaviour, ICommand
{
    private Unit box = null;
    private Unit unit = null;

    private void Start()
    {
        Controller.Instance.AddCommand(this);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            Box(unit.transform.position);
        }
        else if (Input.GetButton("Use"))
        {
            PushBox(unit.transform.position, unit.TargetVelosity);
        }
        else if (Input.GetButtonUp("Use")) DropBox();
    }

    public void TakeUnit(Unit unit)
    {
        this.unit = unit;
    }

    /// <summary>
    /// Захватить коробки
    /// </summary>
    /// <param name="position">позиция героя</param>
    public void Box(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + Vector3.up * 0.4F, 0.4F);
        for (int i = 0; i < colliders.Length; i++)
        {
            Unit tempBox = colliders[i].GetComponent<Unit>();
            if (tempBox != null)
                if(tempBox.CompareTag("Box"))
                    box = tempBox;
        }
    }

    /// <summary>
    /// Толкать коробку
    /// </summary>
    /// <param name="position">позиция героя</param>
    /// <param name="move">направление героя</param>
    public void PushBox(Vector3 position, Vector3 move)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + Vector3.up * 0.4F, 0.4F);
        if (colliders.Length > 0 && InColliders(typeof(Unit), colliders) && box != null)
            box.TargetVelosity = move;
        else
        {
            DropBox();
        }
    }

    /// <summary>
    /// Отпустить коробку
    /// </summary>
    public void DropBox()
    {
        if (box != null)
        {
            box.TargetVelosity = Vector2.zero;
            box = null;
        }
    }

    /// <summary>
    /// Проверяет есть ли среди обьектов нужый тип
    /// </summary>
    /// <param name="type">искомый тип</param>
    /// <param name="colliders">массив коллайдеров</param>
    /// <returns></returns>
    private bool InColliders(Type type, Collider2D[] colliders)
    {
        bool thereIs = false;
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Box"))
            thereIs = true;
        }
        return thereIs;
    }
}
