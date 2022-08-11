using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Animator animator;
    public PlayerStats playerStats;
    public float attackSpeed;
    public bool canAttack;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerStats.attackSpeed = attackSpeed;
        canAttack = true;
    }

    void Update()
    {
        
    }

    public void Attack()
    {
        if (canAttack && !playerStats.isDashing)
        {
            StartCoroutine(AttackTimer());
        }
    }

    IEnumerator AttackTimer()
    {
        canAttack = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(playerStats.attackSpeed);
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
