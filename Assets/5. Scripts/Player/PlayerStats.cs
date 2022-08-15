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
    [HideInInspector] public float currentLife;
    public float damage;
    public float damageMultiplier;
    public float maxstamina;
    [HideInInspector] public float currentStamina;
    public float staminaGain;
    public float dashUses;
    [HideInInspector] public float attackSpeed;
    [HideInInspector] public bool dead;
    public float attackSpeedMultiplier;
    public float moveSpeed;
    [HideInInspector] public bool canTakeDamage;


    [Header("Movimentação")]
    public float dashForce;
    public bool isDashing;
    public float smoothTime;
    public int maxDashUses;
    public bool dontUpdateSprites;
    [HideInInspector] public Vector2 inputValue;
    public bool canMove;

    [Header("Events")]
    [System.NonSerialized] public UnityEvent DashEvent;
    [System.NonSerialized] public UnityEvent LifeBarEvent;
    [System.NonSerialized] public UnityEvent AttackEvent;
    [System.NonSerialized] public UnityEvent ChooseWeaponEvent;
    [System.NonSerialized] public UnityEvent DamageSpriteEvent;
    [System.NonSerialized] public UnityEvent DeathEvent;
    [System.NonSerialized] public UnityEvent LevelCompleted;
    [System.NonSerialized] public UnityEvent SelectCard;
    [System.NonSerialized] public UnityEvent OpenDoorLevelEvent;

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

    [Header("Dano das Armas")]
    public float armaLaser;
    public float armaLaserSpeed;
    public float espada;
    public float espadaSpeed;

    [Header("Scenes")]

    public float wichLevel = 0;    

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
        if (DeathEvent == null) DeathEvent = new UnityEvent();
        if (LevelCompleted == null) LevelCompleted = new UnityEvent();
        if (SelectCard == null) SelectCard = new UnityEvent();
        if (OpenDoorLevelEvent == null) OpenDoorLevelEvent = new UnityEvent();
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

    public void DeathTrigger()
    {
        DeathEvent.Invoke();
    }
    public void LevelCompletedTrigger()
    {
        LevelCompleted.Invoke();
    }

    public void OpenDoorLevel()
    {
        OpenDoorLevelEvent.Invoke();
    }

    public void SelectCardTrigger()
    {
        SelectCard.Invoke();
    }

    public void ResetValues()
    {
        weapon = Weapon.None;
        maxlife = maxlifeReset;
        currentLife = maxlifeReset;
        damage = damageReset;
        maxstamina = maxStaminaReset;
        staminaGain = staminaGainReset;
        dashUses = dashUsesReset;
        attackSpeed = attackSpeedReset;
        moveSpeed = moveSpeedReset;
        attackSpeedMultiplier = 1;
        canMove = true;
        canTakeDamage = true;
        dontUpdateSprites = false;
        dead = false;
        wichLevel = 0;
        damageMultiplier = 1;
    }
}
