using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : Weapon
{

    public override void Awake()
    {
        base.Awake();
        continiousAttack = true;
        ranged = true;
        rechargeTimer = 3;
    }

    public override void Update()
    {
        base.Update();
        if (playerStats.attackPressing) playerStats.dontUpdateSprites = true;
        else playerStats.dontUpdateSprites = false;
    }
}
