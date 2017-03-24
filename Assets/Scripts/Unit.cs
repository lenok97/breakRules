using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour 
{
    public bool frozen=false;
    public bool canShoot=true;

    public virtual void ReceiveDamage()
    {
        Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public void Unfroze()
    {
        frozen = false;
    }

    public void CanShoot()
    {
        canShoot = true;
    }
}
