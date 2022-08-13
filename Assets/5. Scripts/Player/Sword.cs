using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override void Awake()
    {
        base.Awake();
        rechargeTimer = 1f;
        rechargeOnEnd = true;
    }

    public override void Update()
    {
        base.Update();
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack")) playerStats.canMove = false;
        else playerStats.canMove = true;
    }

    public override void Attack()
    {
        base.Attack();
    }
}
