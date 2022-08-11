using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject spritesGroup;
    [SerializeField] GameObject dashGroup;
    [SerializeField] GameObject[] sprites;
    [SerializeField] GameObject[] spritesDash;
    [HideInInspector] public enum FacindDirection { Frente, Costas, Esquerda, Direita, FrenteEsq, FrentDir, CostasEsq, CostasDir }
    [HideInInspector] public FacindDirection facingDirection;

    PlayerManager playerManager;
    PlayerStats playerStats;
    Rigidbody rig;

    Vector3 currentVelocity;
    Vector3 currentInput;
    Vector3 input;
    Vector3 dashVector;

    float dashForce;
    bool dashing;
    bool canDash;
    float dashUses;

    int wichSprite;
    int wichSpriteDash;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = playerManager.playerStats;
        rig = GetComponent<Rigidbody>();
    }

    void Start()
    {
        playerStats.inputValue = Vector2.zero;
        canDash = true;
        playerStats.canMove = true;
    }
   
    void FixedUpdate()
    {
        dashUses = playerStats.maxstamina/playerStats.dashUses;

        playerStats.currentStamina += playerStats.staminaGain * Time.deltaTime;

        if (playerStats.currentStamina > 1) playerStats.currentStamina = 1;
        if (playerStats.currentStamina < 0) playerStats.currentStamina = 0;

        if (playerStats.canMove) ChangeSprites();
        if (playerStats.canMove) Movement();
        if (dashing) Dash(); 
        

    }

    void Movement()
    {        
        input = new Vector3(playerStats.inputValue.x, 0, playerStats.inputValue.y);
        currentInput = Vector3.SmoothDamp(currentInput, input, ref currentVelocity, playerStats.smoothTime);
        rig.velocity = currentInput * playerStats.moveSpeed;
    }

    void StartDash()
    {
        if (canDash && playerStats.currentStamina >= dashUses) StartCoroutine(DashTimer());
    }
    IEnumerator DashTimer()
    {
        SwitchSprites(true);
        playerStats.canMove = false;
        canDash = false;
        dashing = true;
        playerStats.currentStamina -= dashUses;

        Vector2 input = playerStats.inputValue;

        dashVector = new Vector3(input.x, 0, input.y).normalized;

        yield return new WaitForSeconds(.2f);
        playerStats.canMove = true;
        canDash = true;
        dashing = false;
        SwitchSprites(false);
    }

    void Dash()
    {
        dashForce = playerManager.playerStats.dashForce;
        rig.velocity = dashVector * dashForce;
    }

    void ChangeSprites()
    {
        Vector2 inputValue = playerStats.inputValue;

        //mds que bagunça que vergonha
        if (inputValue.y < 0 && inputValue.x < 0.65 && inputValue.x > -0.65)
        {
            wichSprite = 0;
            wichSpriteDash = 0;
            facingDirection = FacindDirection.Frente;
        }
        if (inputValue.y > 0 && inputValue.x < 0.65 && inputValue.x > -0.65)
        {
            wichSprite = 1;
            wichSpriteDash = 1;
            facingDirection = FacindDirection.Costas;
        }
        if (inputValue.x < 0 && inputValue.y < 0.65 && inputValue.y > -0.65)
        {
            wichSprite = 2;
            wichSpriteDash = 2;
            facingDirection = FacindDirection.Esquerda;
        }
        if (inputValue.x > 0 && inputValue.y < 0.65 && inputValue.y > -0.65)
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

    private void OnEnable()
    {
        playerStats.DashEvent.AddListener(StartDash);
    }

    private void OnDisable()
    {
        playerStats.DashEvent.RemoveListener(StartDash);
    }
}
