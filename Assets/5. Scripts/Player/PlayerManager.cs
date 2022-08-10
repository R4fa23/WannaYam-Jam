using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats playerStats;
    [HideInInspector] public enum FacindDirection { Frente, Costas, Esquerda, Direita, FrenteEsq, FrentDir, CostasEsq, CostasDir }
    [HideInInspector] public FacindDirection facingDirection;

    [SerializeField] GameObject spritesGroup;
    [SerializeField] GameObject dashGroup;
    [SerializeField] GameObject[] sprites;
    [SerializeField] GameObject[] spritesDash;
    private float RemainingLife;

    PlayerInput playerInput;
    int wichSprite;
    int wichSpriteDash;
    public bool dashing;
    
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        RemainingLife = playerStats.maxlife;
    }

    void Start()
    {
        
    }

    void Update()
    {
        playerStats.inputValue = playerInput.actions["Move"].ReadValue<Vector2>();
        if(playerStats.canMove) ChangeSprites();
    }

    void ChangeSprites()
    {
        Vector2 inputValue = playerStats.inputValue;
        
        //mds que bagunça que vergonha
        if (inputValue.y < 0 && inputValue.x < 0.7 && inputValue.x > -0.7 )
        {
            wichSprite = 0;
            wichSpriteDash = 0;
            facingDirection = FacindDirection.Frente;
        }   
        if (inputValue.y > 0 && inputValue.x < 0.7 && inputValue.x > -0.7)
        {
            wichSprite = 1;
            wichSpriteDash = 1;
            facingDirection = FacindDirection.Costas;
        }
        if(inputValue.x < 0 && inputValue.y < 0.7 && inputValue.y > -0.7)
        {
            wichSprite = 2;
            wichSpriteDash = 2;
            facingDirection = FacindDirection.Esquerda;
        }
        if (inputValue.x > 0 && inputValue.y < 0.7 && inputValue.y > -0.7)
        {
            wichSprite = 3;
            wichSpriteDash = 3;
            facingDirection = FacindDirection.Direita;
        }
        if (inputValue.y < 0 && inputValue.x < -0.7)
        {
            wichSprite = 4;
            wichSpriteDash = 4;
            facingDirection = FacindDirection.FrenteEsq;
        }
        if (inputValue.y < 0 && inputValue.x > 0.7)
        {
            wichSprite = 5;
            wichSpriteDash = 5;
            facingDirection = FacindDirection.FrentDir;
        }
        if (inputValue.y > 0 && inputValue.x < -0.7)
        {
            wichSprite = 6;
            wichSpriteDash = 6;
            facingDirection = FacindDirection.CostasEsq;
        }
        if (inputValue.y > 0 && inputValue.x > 0.7)
        {
            wichSprite = 7;
            wichSpriteDash = 7;
            facingDirection = FacindDirection.CostasDir;
        }             
        
        for (int i = 0; i < sprites.Length; i++)
        {
            if (wichSprite == i) sprites[i].SetActive(true);
            else sprites[i].SetActive(false);
        }

        for (int i = 0; i < spritesDash.Length; i++)
        {
            if (wichSprite == i) spritesDash[i].SetActive(true);
            else spritesDash[i].SetActive(false);
        }
    }
    public void SwitchSprites(bool dash)
    {
        dashGroup.SetActive(dash);
        spritesGroup.SetActive(!dash);
    }

    public void DashInput(InputAction.CallbackContext context)
    {
        if (context.started) playerStats.DashTrigger();
    }

    public void GetDamage(int damage)
    {
        RemainingLife -= damage;
        Debug.Log("Vida Restante: "+ RemainingLife + " de " + playerStats.maxlife);
    }
    
}
