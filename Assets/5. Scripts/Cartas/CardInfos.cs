using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardInfos : MonoBehaviour
{
    public PlayerStats playerStats;
    public CardSO cardSO;
    public TextMeshProUGUI nameCard;
    public TextMeshProUGUI history;
    public TextMeshProUGUI effect;
    public TextMeshProUGUI genre;
    public TextMeshProUGUI type;

    public Image icon;
    public Image splash;
    public Image card;

    public Color color;

    private void OnValidate()
    {
      UpdateInfos();  
    }

    private void Start()
    {

    }

    public void UpdateInfos()
    {
        nameCard.text = cardSO.nameCard;
        history.text = cardSO.history;
        effect.text = cardSO.effect;
        genre.text = cardSO.genre;
        type.text = cardSO.cardType.ToString();

        splash.sprite = cardSO.splash;
        icon.sprite = cardSO.icon;
        if (cardSO.cardType == CardSO.Type.normal) card.color = Color.white;
        else card.color = color;
    }

    public void CardSelected()
    {
        playerStats.damageMultiplier += cardSO.damagePercent / 100; 
        playerStats.moveSpeed += cardSO.moveSpeedPercent / 100;
        playerStats.staminaGain += cardSO.staminaGainPercent / 100;
        playerStats.attackSpeedMultiplier += cardSO.attackSpeedPercent / 100;
        playerStats.dashUses += cardSO.dashUses;
        playerStats.currentLife += cardSO.heal;
        playerStats.LifeBarTrigger();
    }
}
