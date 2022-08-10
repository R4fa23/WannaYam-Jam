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

    float forceDashVelocity;
    float currentDash;
    float dashForce;
    bool dashing;
    bool canDash;

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
        if (canDash) StartCoroutine(DashTimer());
    }
    IEnumerator DashTimer()
    {
        playerManager.SwitchSprites(true);
        playerStats.canMove = false;
        canDash = false;
        dashing = true;

        Vector2 input = playerStats.inputValue;

        dashVector = new Vector3(input.x, 0, input.y).normalized;

        Debug.Log(dashVector);

        yield return new WaitForSeconds(.2f);
        playerStats.canMove = true;
        canDash = true;
        dashing = false;
        playerManager.SwitchSprites(false);
    }

    void Dash()
    {
        dashForce = playerManager.playerStats.dashForce;
        //currentDash = Mathf.SmoothDamp(currentDash, dashForce, ref forceDashVelocity, 0.1f);
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
