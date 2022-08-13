using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] GameObject bullestsGroup;
    [SerializeField] Rigidbody[] bullets;
    [SerializeField] GameObject aimPosition;
    [SerializeField] Image rechargerMeter;
    [SerializeField] float velocity;

    protected float rechargeTimer;
    public float attackSpeed;
    public float rechargerMultiplier;

    protected Animator animator;
    protected bool continiousAttack;
    protected bool canAttack;
    protected bool ranged;
    protected int index;
    protected bool recharging;
    protected bool rechargeOnEnd;

    public virtual void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        canAttack = true;

        bullestsGroup = GameObject.Find("Bullets");
        bullets = new Rigidbody[bullestsGroup.transform.childCount];
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i] = bullestsGroup.transform.GetChild(i).GetComponent<Rigidbody>();
        }

        if (rechargerMultiplier == 0) rechargerMultiplier = 1;

    }

    public virtual void Update()
    {
        attackSpeed = playerStats.attackSpeed;

        if (recharging) rechargerMeter.fillAmount += playerStats.attackSpeed * Time.deltaTime * rechargerMultiplier * playerStats.attackSpeedMultiplier;
        if (rechargerMeter.fillAmount >= 1)
        {
            rechargerMeter.fillAmount = 0;
            recharging = false;
        }
    }
    public virtual void Attack()
    {
        if (canAttack && !playerStats.isDashing && !recharging && !playerStats.dead && playerStats.canTakeDamage)
        {
            StartCoroutine(AttackTimer());
        }
    }

    IEnumerator AttackTimer()
    {
        animator.speed = playerStats.attackSpeed;
        canAttack = false;
        animator.SetTrigger("attack");
        if (ranged) RangedAttack();

        yield return new WaitForSeconds(1/playerStats.attackSpeed);

        canAttack = true;
        animator.speed = 1;
        if (rechargeOnEnd) recharging = true;
        if (continiousAttack && playerStats.attackPressing) Attack();
    }

    void RangedAttack()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (i == index)
            {                
                bullets[i].gameObject.SetActive(true);
                bullets[i].transform.position = aimPosition.transform.position;
                bullets[i].transform.rotation = aimPosition.transform.rotation;
                bullets[i].velocity = bullets[i].transform.forward * velocity;
            }
        }
        index++;
        if (index > 5)
        {
            recharging = true;
            Debug.Log("alow");
            index = 0;            
        }
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
