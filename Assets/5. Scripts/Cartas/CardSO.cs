using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards")]

public class CardSO : ScriptableObject
{
    public string nameCard;
    public string effect;
    public string history;
    public string genre;

    public enum Type { normal, special}
    public Type cardType;

    public Sprite icon;
    public Sprite splash;
    public Color color;

    public float damagePercent;
    public float moveSpeedPercent;
    public float staminaGainPercent;
    public float attackSpeedPercent;
    public float dashUses;
    public float heal;
}
