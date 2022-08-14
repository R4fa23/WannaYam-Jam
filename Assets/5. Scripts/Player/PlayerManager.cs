using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    
    [SerializeField] GameObject [] weapons;

    public PlayerStats playerStats;
    public bool dashing;

    PlayerInput playerInput;
    Animator animator;
    int index;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        ChooseWeapon();
    }

    void Update()
    {
        playerStats.inputValue = playerInput.actions["Move"].ReadValue<Vector2>();
    }    

    void ChooseWeapon()
    {        
        switch (playerStats.weapon)
        {
            case PlayerStats.Weapon.Gun:
                index = 0;
                playerStats.attackSpeed = playerStats.armaLaserSpeed;
                playerStats.damage = playerStats.armaLaser;
                break;
            case PlayerStats.Weapon.Sword:
                playerStats.attackSpeed = playerStats.espadaSpeed;
                playerStats.damage = playerStats.espada;
                index = 1;
                break;
            case PlayerStats.Weapon.Lantern:
                index = 2;
                break;
            case PlayerStats.Weapon.Kart:
                index = 3;
                break;
            case PlayerStats.Weapon.None:
                foreach (var item in weapons)
                index = 4;
                break;
            default:
                break;
        }

        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == index) weapons[i].SetActive(true);
            else weapons[i].SetActive(false);
        }

    }

    public void GetDamage(int damage)
    {
        if (playerStats.canTakeDamage)
        {
            playerStats.currentLife -= damage;
            if (playerStats.currentLife <= 0) playerStats.currentLife = 0;
            if (playerStats.currentLife >= playerStats.maxlife) playerStats.currentLife = playerStats.maxlife;
            playerStats.LifeBarTrigger();
            playerStats.canTakeDamage = false;

            if (playerStats.currentLife > 0)
            {                
                animator.SetTrigger("take damage");
                playerStats.DamageSpriteTrigger();
                StartCoroutine(CanTakeDamageTimer());
            } 
            else
            {
                playerStats.DeathTrigger();
                animator.SetTrigger("dead");
                playerStats.canMove = false;
                playerStats.dead = true;
                foreach (var item in weapons)
                {
                    item.SetActive(false);
                }
            }
            //Debug.Log("Vida Restante: " + playerStats.currentLife + " de " + playerStats.maxlife);
        }
    }

    IEnumerator CanTakeDamageTimer()
    {
        yield return new WaitForSeconds(0.3f);
        playerStats.canTakeDamage = true;
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.DashTrigger();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerStats.AttackTrigger();
            playerStats.attackPressing = true;
        }
        else playerStats.attackPressing = false;
    }

    private void OnEnable()
    {
        playerStats.ChooseWeaponEvent.AddListener(ChooseWeapon);
    }

    private void OnDisable()
    {
        playerStats.ChooseWeaponEvent.RemoveListener(ChooseWeapon);
    }
}
