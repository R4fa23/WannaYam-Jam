using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    int index;
    [SerializeField] GameObject [] weapons;
    public PlayerStats playerStats;   
    PlayerInput playerInput;
    public bool dashing;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        playerStats.inputValue = playerInput.actions["Move"].ReadValue<Vector2>();
        ChooseWeapon();
    }    

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.DashTrigger();
    }

    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.AttackTrigger();
    }

    void ChooseWeapon()
    {        
        switch (playerStats.weapon)
        {
            case PlayerStats.Weapon.Gun:
                index = 0;
                break;
            case PlayerStats.Weapon.Sword:
                index = 1;
                break;
            case PlayerStats.Weapon.Lantern:
                index = 2;
                break;
            case PlayerStats.Weapon.Kart:
                index = 3;
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
        playerStats.currentLife -= damage;
        Debug.Log("Vida Restante: "+ playerStats.currentLife + " de " + playerStats.maxlife);
        playerStats.LifeBarTrigger();
    }    
}
