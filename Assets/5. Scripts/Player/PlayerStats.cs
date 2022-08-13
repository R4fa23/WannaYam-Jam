using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Stats", menuName = "Player Stats")]
public class PlayerStats : ScriptableObject
{
    public enum Weapon {Gun, Sword, Lantern, Kart, None};
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
    public bool canTakeDamage;

    [Header("Movimentação")]
    public float dashForce;
    public bool isDashing;
    public float smoothTime;
    public int maxDashUses;
    public bool dontUpdateSprites;
    [HideInInspector] public Vector2 inputValue;
    public bool canMove;

    [Header("Balas")]
    public Rigidbody[] balas;

    [Header("Events")]
    [System.NonSerialized] public UnityEvent DashEvent;
    [System.NonSerialized] public UnityEvent LifeBarEvent;
    [System.NonSerialized] public UnityEvent AttackEvent;
    [System.NonSerialized] public UnityEvent ChooseWeaponEvent;
    [System.NonSerialized] public UnityEvent DamageSpriteEvent;

    [Header("Inputs")]
    public bool attackPressing;

    [Header("Reset Values")]
    public float maxlifeReset;
    public float damageReset;
    public float maxStaminaReset;
    public float staminaGainReset;
    public float dashUsesReset;
    public float attackSpeedReset;
    public float moveSpeedReset;

    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        ResetValues();
        if (DashEvent == null) DashEvent = new UnityEvent();
        if (LifeBarEvent == null) LifeBarEvent = new UnityEvent();
        if (AttackEvent == null) AttackEvent = new UnityEvent();
        if (ChooseWeaponEvent == null) ChooseWeaponEvent = new UnityEvent();
        if (DamageSpriteEvent == null) DamageSpriteEvent = new UnityEvent();
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
    public void ChooseWeaponEventTrigger()
    {
        ChooseWeaponEvent.Invoke();
    }

    public void DamageSpriteTrigger()
    {
        DamageSpriteEvent.Invoke();
    }

    public void ResetValues()
    {
        maxlife = maxlifeReset;
        currentLife = maxlifeReset;
        damage = damageReset;
        maxstamina = maxStaminaReset;
        staminaGain = staminaGainReset;
        dashUses = dashUsesReset;
        attackSpeed = attackSpeedReset;
        moveSpeed = moveSpeedReset;

        canMove = true;
        canTakeDamage = true;
        dontUpdateSprites = false;

    }
}
