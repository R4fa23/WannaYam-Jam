using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSprites : MonoBehaviour
{
    [SerializeField] GameObject rangedWeaponPosition;
    [SerializeField] GameObject spritesGroup;
    [SerializeField] GameObject dashGroup;
    [SerializeField] GameObject damageSprite;
    [SerializeField] PlayerStats playerStats;
    [SerializeField] GameObject[] sprites;
    [SerializeField] GameObject[] spritesDash;
    [HideInInspector] public enum FacingDirection { Frente, Costas, Esquerda, Direita, FrenteEsq, FrentDir, CostasEsq, CostasDir }
    [HideInInspector] public FacingDirection facingDirection;

    int wichSprite;
    int wichSpriteDash;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStats.canMove && !playerStats.dontUpdateSprites) ChangeSprites();
        DashingSprites(playerStats.isDashing);

    }

    void ChangeSprites()
    {
        Vector2 inputValue = playerStats.inputValue;

        //mds que bagunça que vergonha
        if (inputValue.y < 0 && inputValue.x < 0.65 && inputValue.x > -0.65)           
            facingDirection = FacingDirection.Frente;
        
        if (inputValue.y > 0 && inputValue.x < 0.65 && inputValue.x > -0.65)
           facingDirection = FacingDirection.Costas;
        
        if (inputValue.x < 0 && inputValue.y < 0.65 && inputValue.y > -0.65)
            facingDirection = FacingDirection.Esquerda;
     
        if (inputValue.x > 0 && inputValue.y < 0.65 && inputValue.y > -0.65)  
            facingDirection = FacingDirection.Direita;
        
        if (inputValue.y < 0 && inputValue.x < -0.7)      
            facingDirection = FacingDirection.FrenteEsq;
        
        if (inputValue.y < 0 && inputValue.x > 0.7)
            facingDirection = FacingDirection.FrentDir;
        
        if (inputValue.y > 0 && inputValue.x < -0.7)       
            facingDirection = FacingDirection.CostasEsq;
        
        if (inputValue.y > 0 && inputValue.x > 0.7)
            facingDirection = FacingDirection.CostasDir;

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

        switch (facingDirection)
        {
            case FacingDirection.Frente:
                wichSprite = 0;
                wichSpriteDash = 0;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, -0, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0, 0.2f, 0.25f);

                break;
            case FacingDirection.Costas:
                wichSprite = 1;
                wichSpriteDash = 1;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, 180, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0, 0.2f, 0.4f);

                break;
            case FacingDirection.Esquerda:
                wichSprite = 2;
                wichSpriteDash = 2;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, 90, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(-0.35f, 0.2f, 0.3f);

                break;
            case FacingDirection.Direita:
                wichSprite = 3;
                wichSpriteDash = 3;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, -90, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0.35f, 0.2f, 0.3f);

                break;
            case FacingDirection.FrenteEsq:
                wichSprite = 4;
                wichSpriteDash = 4;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, 45, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0, 0.2f, 0.33f);

                break;
            case FacingDirection.FrentDir:
                wichSprite = 5;
                wichSpriteDash = 5;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, -45, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0f, 0.2f, 0.33f);
                break;
            case FacingDirection.CostasEsq:
                wichSprite = 6;
                wichSpriteDash = 6;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, 135, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(-0.34f, 0.2f, 0.34f);

                break;
            case FacingDirection.CostasDir:
                wichSprite = 7;
                wichSpriteDash = 7;
                rangedWeaponPosition.transform.rotation = Quaternion.Euler(0, -135, 0);
                rangedWeaponPosition.transform.localPosition = new Vector3(0f, 0.2f, 0.34f);

                break;
            default:
                break;
        }
    }


    void DamageSprite()
    {
        playerStats.dontUpdateSprites = true;
        dashGroup.SetActive(false);
        spritesGroup.SetActive(false);
        damageSprite.SetActive(true);
    }

    public void DashingSprites(bool dash)
    {
        dashGroup.SetActive(dash);
        spritesGroup.SetActive(!dash);
    }
}
