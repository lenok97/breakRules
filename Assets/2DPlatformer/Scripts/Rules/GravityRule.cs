using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityRule : MonoBehaviour
{
	void Update ()
    {

        if (Input.GetButtonDown("GravityRule"))
        {
            Unit unitCollection = Unit.FirstCreated;
            foreach (Unit unit in unitCollection)
            {
                unit.ChengeGravity();
            }
        }
    }
}
