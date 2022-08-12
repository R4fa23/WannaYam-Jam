using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    protected Animator animator;
    public PlayerStats playerStats;
    protected float attackSpeed;
    public bool canAttack;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        canAttack = true;
    }

    void Update()
    {
    }

    public virtual void Attack()
    {
        if (canAttack && !playerStats.isDashing)
        {
            StartCoroutine(AttackTimer());
        }
        else animator.speed = 1;
    }

    IEnumerator AttackTimer()
    {
        animator.speed = playerStats.attackSpeed;
        canAttack = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(1/playerStats.attackSpeed);
        canAttack = true;
    }

    private void OnEnable()
    {
        playerStats.AttackEvent.AddListener(Attack);
    }

    private void OnDisable()
    {
        playerStats.AttackEvent.RemoveListener(Attack);
    }
}
