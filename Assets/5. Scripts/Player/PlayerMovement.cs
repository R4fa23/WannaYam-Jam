using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerManager playerManager;
    PlayerStats playerStats;
    Rigidbody rig;

    Vector3 currentVelocity;
    Vector3 currentInput;
    Vector3 input;
    Vector3 dashVector;

    float dashForce;
    bool canDash;
    float dashUses;

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

    private void Update()
    {
        dashUses = playerStats.maxstamina / playerStats.dashUses;

        playerStats.currentStamina += playerStats.staminaGain * Time.deltaTime;

        if (playerStats.currentStamina > 1) playerStats.currentStamina = 1;
        if (playerStats.currentStamina < 0) playerStats.currentStamina = 0;
    }

    void FixedUpdate()
    {
        if (playerStats.canMove) Movement();
        else rig.velocity = Vector3.zero;
        if (playerStats.isDashing) Dash();
        rig.velocity = new Vector3(rig.velocity.x, -10, rig.velocity.z);
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
        playerStats.canMove = false;
        canDash = false;
        playerStats.isDashing = true;
        playerStats.currentStamina -= dashUses;

        Vector2 input = playerStats.inputValue;

        dashVector = new Vector3(input.x, 0, input.y).normalized;

        yield return new WaitForSeconds(.2f);
        playerStats.canMove = true;
        canDash = true;
        playerStats.isDashing = false;
    }

    void Dash()
    {
        dashForce = playerManager.playerStats.dashForce;
        rig.velocity = dashVector * dashForce;
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
