using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpRule : MonoBehaviour
{
    private float[] jumpSpeeds = { 0, 1,1.5F, 1.7F };
    private int count = 2;
    void Update()
    {
        if (Input.GetButtonDown("JumpRule"))
            Change();
    }
    public void Change(int i )
    {
        if (i >= jumpSpeeds.Length)
        {
            Unit unitCollection = Unit.FirstCreated;
            foreach (Unit unit in unitCollection)
            {
                unit.JumpVelosity = unit.CoefficientJump * jumpSpeeds[i];
            }
        }
    }
    public void Change()
    {
        if (count >= jumpSpeeds.Length) count = 0;
        Unit unitCollection = Unit.FirstCreated;
        foreach (Unit unit in unitCollection)
        {
            unit.JumpVelosity = unit.CoefficientJump * jumpSpeeds[count];
        }
        count++;
    }
}
