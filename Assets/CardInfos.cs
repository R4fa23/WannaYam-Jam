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

      

    private void Start()
    {
        nameCard.text = cardSO.nameCard;
        history.text = cardSO.history;
        effect.text = cardSO.effect;
        genre.text = cardSO.genre;
        type.text = cardSO.cardType.ToString();

        card.sprite = cardSO.card;
        splash.sprite = cardSO.splash;
        icon.sprite = cardSO.splash;

        color = cardSO.color;
    }

    public void CardSelected()
    {
        playerStats.damage += cardSO.damage / 100;
        playerStats.staminaGain += cardSO.staminaGain / 100;
        playerStats.moveSpeed += cardSO.moveSpeed / 100;
        playerStats.currentLife += cardSO.life;
    }
}
