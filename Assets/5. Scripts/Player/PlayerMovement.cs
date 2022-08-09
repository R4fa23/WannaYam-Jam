using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    CharacterController characterController;

    Vector3 currentVelocity;
    Vector3 currentInput;
    Vector3 input;
    Vector3 dashVector;

    float dashForce;
    bool dashing;
    bool canDash;

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = playerManager.playerStats;
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        playerStats.inputValue = Vector2.zero;
        canDash = true;
    }
   
    void Update()
    {
        Movement();
        if(dashing) Dash();
    }

    void Movement()
    {        
        input = new Vector3(playerStats.inputValue.x, 0, playerStats.inputValue.y);
        currentInput = Vector3.SmoothDamp(currentInput, input, ref currentVelocity, playerStats.smoothTime);
        characterController.Move(currentInput * playerStats.moveSpeed / 10);
    }

    void StartDash()
    {
        if (canDash) StartCoroutine(DashTimer());
    }
    IEnumerator DashTimer()
    {
        canDash = false;
        dashing = true;
        yield return new WaitForSeconds(.2f);
        canDash = true;
        dashing = false;
    }

    void Dash()
    {        
        dashForce = playerManager.playerStats.dashForce;       

        switch (playerManager.facingDirection)
        {
            case PlayerManager.FacindDirection.Frente:
                dashVector = -transform.forward;
                break;
            case PlayerManager.FacindDirection.Costas:
                dashVector = transform.forward;
                break;
            case PlayerManager.FacindDirection.Esquerda:
                dashVector = -transform.right;
                break;
            case PlayerManager.FacindDirection.Direita:
                dashVector = transform.right;
                break;
            default:
                break;
        }

        transform.position +=  dashVector * dashForce * Time.deltaTime;
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
