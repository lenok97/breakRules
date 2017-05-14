using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D doorCollider=null;
    private Animator animator=null;
    private void OnEnable()
    {
        doorCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        //Добавить себя как получателя события использования переключателя
        EventManager.Instance.AddListener(EVENT_TYPE.DOOR_OPEN, this.DoorOpen);
        EventManager.Instance.AddListener(EVENT_TYPE.DORR_CLOSE, this.DoorClose);
    }

    private void DoorOpen(EVENT_TYPE Event_Type, Component sender, object param)
    {
        doorCollider.enabled = false;
        animator.SetBool("IsOpen", true);
    }
    private void DoorClose(EVENT_TYPE Event_Type, Component sender, object param)
    {
        doorCollider.enabled = true;
        animator.SetBool("IsOpen", false);
    }
}
