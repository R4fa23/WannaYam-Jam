using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack")) playerStats.canMove = false;
        else playerStats.canMove = true;
    }
}
