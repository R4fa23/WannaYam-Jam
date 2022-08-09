using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    public PlayerStats PlayerStats;
    PlayerInput playerInput;

    [SerializeField] GameObject[] sprites;
    int wichSprite;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();    
    }

    void Start()
    {
        
    }

    void Update()
    {
        PlayerStats.inputValue = playerInput.actions["Move"].ReadValue<Vector2>();
        ChangeSprites();
    }

    void ChangeSprites()
    {
        Vector2 inputValue = PlayerStats.inputValue;

        if (inputValue.y < 0) wichSprite = 0;
        if (inputValue.y > 0) wichSprite = 1;
        if (inputValue.x < 0) wichSprite = 2;
        if (inputValue.x > 0) wichSprite = 3;

        for (int i = 0; i < sprites.Length; i++)
        {
            if (wichSprite == i) sprites[i].SetActive(true);
            else sprites[i].SetActive(false);
        }
    }
    
}
