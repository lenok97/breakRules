using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRule : MonoBehaviour
{
    private float[] runSpeeds = { 0, 0.5F, 1, 2 };
    private int count = 3;
    void Update()
    {
        if (Input.GetButtonDown("SpeedRule"))
            Change();
    }
    public void Change(int i)
    {
        if (i >= runSpeeds.Length)
        {
            Unit unitCollection = Unit.FirstCreated;
            foreach (Unit unit in unitCollection)
            {
                unit.MaxSpeed = unit.CoefficientSpeed * runSpeeds[i];
            }
        }
    }
    public void Change()
    {
        if (count >= runSpeeds.Length) count = 0;
        Unit unitCollection = Unit.FirstCreated;
        foreach (Unit unit in unitCollection)
        {
            unit.MaxSpeed = unit.CoefficientSpeed * runSpeeds[count];
        }
        count++;
    }
}
