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
    public Sprite card;
    public Color color;

    public float damage;
    public float moveSpeed;
    public float staminaGain;
    public float life;
}
