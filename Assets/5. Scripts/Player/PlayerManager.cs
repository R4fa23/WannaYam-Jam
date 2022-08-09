using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [SerializeField] GameObject[] sprites;
    
    PlayerInput playerInput;
    int wichSprite;
    [HideInInspector] public enum FacindDirection {Frente, Costas, Esquerda, Direita}
    [HideInInspector] public FacindDirection facingDirection;
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
        ChangeSprites();
    }

    void ChangeSprites()
    {
        Vector2 inputValue = playerStats.inputValue;

        if (inputValue.y < 0)
        {
            wichSprite = 0;
            facingDirection = FacindDirection.Frente;
        }
        if (inputValue.y > 0)
        {
            wichSprite = 1;
            facingDirection = FacindDirection.Costas;
        }
        if (inputValue.x < 0)
        {
            wichSprite = 2;
            facingDirection = FacindDirection.Esquerda;
        }
        if (inputValue.x > 0)
        {
            wichSprite = 3;
            facingDirection = FacindDirection.Direita;
        }

        for (int i = 0; i < sprites.Length; i++)
        {
            if (wichSprite == i) sprites[i].SetActive(true);
            else sprites[i].SetActive(false);
        }
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.DashTrigger();
    }
    
}
