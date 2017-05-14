 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchUnitRule : MonoBehaviour
{
    [SerializeField]
    private float switchRadius = 3;

    private void Update()
    {
        if (Input.GetButtonDown("SwitchUnitRule"))
        {
            Change();
        }
    }

    public void Change()
    {
        EventManager.Instance.PostNotification(EVENT_TYPE.CHENGE_UNIT,this, switchRadius);
    }
}
