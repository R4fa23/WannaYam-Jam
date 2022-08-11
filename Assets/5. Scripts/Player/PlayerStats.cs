using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public enum Weapon {Gun, Sword, Lantern, Kart};
    public Weapon weapon;

    [Header("Status Basicos")]
    public float maxlife;
    public float currentLife;
    public float damage;
    public float maxstamina;
    public float currentStamina;
    public float staminaGain;
    public float dashUses;
    public float attackSpeed;
    public float moveSpeed;

    [Header("Movimentação")]
    public float dashForce;
    public bool isDashing;
    public float smoothTime;
    public int maxDashUses;
    [HideInInspector] public Vector2 inputValue;
    [HideInInspector] public bool canMove;

    [Header("Events")]
    [System.NonSerialized] public UnityEvent DashEvent;
    [System.NonSerialized] public UnityEvent LifeBarEvent;
    [System.NonSerialized] public UnityEvent AttackEvent;

    private void OnEnable()
    {
        currentLife = maxlife;
        if (DashEvent == null) DashEvent = new UnityEvent();
        if (LifeBarEvent == null) LifeBarEvent = new UnityEvent();
        if (AttackEvent == null) AttackEvent = new UnityEvent();
    }

    public void DashTrigger()
    {
        DashEvent.Invoke();
    }
    public void LifeBarTrigger()
    {
        LifeBarEvent.Invoke();
    }

    public void AttackTrigger()
    {
        AttackEvent.Invoke();
    }

}
