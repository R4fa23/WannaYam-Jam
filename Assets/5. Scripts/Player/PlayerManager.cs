using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
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

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.DashTrigger();
        
    }

    public void GetDamage(int damage)
    {
        playerStats.currentLife -= damage;
        Debug.Log("Vida Restante: "+ playerStats.currentLife + " de " + playerStats.maxlife);
        playerStats.LifeBarTrigger();
    }
    
}
