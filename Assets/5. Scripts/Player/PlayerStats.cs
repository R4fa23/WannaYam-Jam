using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Status Basicos")]
    public float life;
    public float damage;
    public float moveSpeed;
    public float attackSpeed;

    [Header("Movimentação")]
    public Vector2 inputValue;
    public float smoothTime;
}
