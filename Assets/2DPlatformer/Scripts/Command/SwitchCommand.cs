using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCommand : MonoBehaviour, ICommand
{
    Transform unitTransform;

    private void Start()
    {
        Controller.Instance.AddCommand(this);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Use"))
        {
            Switch(unitTransform.position);
        }
    }

    public void TakeUnit(Unit unit)
    {
        unitTransform = unit.transform;
    }

    private void Switch(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(position + Vector3.up * 0.4F, 0.4F);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Switch"))
                // Послать уведомление об использование переключателя
                EventManager.Instance.PostNotification(EVENT_TYPE.DOOR_OPEN);
        }
    }
}
