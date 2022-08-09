using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    [Header("Status Basicos")]
    public float maxlife;
    public float damage;
    public float maxstamina;
    public float staminaGain;
    public float attackSpeed;
    public float moveSpeed;

    [Header("Movimentação")]
    public float dashForce;
    public Vector2 inputValue;
    public float smoothTime;

    [Header("Inputs")]
    [System.NonSerialized] public UnityEvent DashEvent;

    private void OnEnable()
    {
        if (DashEvent == null) DashEvent = new UnityEvent();
    }

    public void DashTrigger()
    {
        DashEvent.Invoke();
    }
}
