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

    private void Awake()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        playerStats = playerManager.PlayerStats;
        characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        playerStats.inputValue = Vector2.zero;
    }
   
    void Update()
    {
        Vector3 input = new Vector3(playerStats.inputValue.x, 0, playerStats.inputValue.y);
        currentInput = Vector3.SmoothDamp(currentInput, input, ref currentVelocity, playerStats.smoothTime);
        characterController.Move(currentInput * playerStats.moveSpeed/10);
    }

    

}
