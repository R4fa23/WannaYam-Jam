using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] Rigidbody[] bullets;
    [SerializeField] GameObject aimPosition;
    [SerializeField] float velocity;

    public override void Awake()
    {
        base.Awake();

    }

    void Update()
    {
    }

    public override void Attack()
    {
        if (canAttack)
        {
            for (int i = 0; i < bullets.Length; i++)
            {
                if (i == 0)
                {
                    bullets[i].gameObject.SetActive(true);
                    bullets[i].transform.position = aimPosition.transform.position;
                    bullets[i].transform.eulerAngles = aimPosition.transform.forward;
                    bullets[i].velocity = bullets[i].transform.forward * velocity;
                }
            }
        }
        base.Attack();           
    }

    IEnumerator checkCanShoot()
    {
        canAttack = false;
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Sword Attack"))
        {
            canAttack = false;
        }
        yield return canAttack = true;
        
    }
}
