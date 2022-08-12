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
        playerStats.currentLife -= damage;
        Debug.Log("Vida Restante: "+ playerStats.currentLife + " de " + playerStats.maxlife);
        playerStats.LifeBarTrigger();
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
