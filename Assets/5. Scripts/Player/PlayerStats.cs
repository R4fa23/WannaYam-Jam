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

    [Header("Movimentação")]
    public float dashForce;
    public bool isDashing;
    public float smoothTime;
    public int maxDashUses;
    public bool dontUpdateSprites;
    [HideInInspector] public Vector2 inputValue;
    [HideInInspector] public bool canMove;

    [Header("Balas")]
    public Rigidbody[] balas;

    [Header("Events")]
    [System.NonSerialized] public UnityEvent DashEvent;
    [System.NonSerialized] public UnityEvent LifeBarEvent;
    [System.NonSerialized] public UnityEvent AttackEvent;
    [System.NonSerialized] public UnityEvent ChooseWeaponEvent;

    [Header("Inputs")]
    public bool attackPressing;

    private void OnEnable()
    {
        currentLife = maxlife;
        if (DashEvent == null) DashEvent = new UnityEvent();
        if (LifeBarEvent == null) LifeBarEvent = new UnityEvent();
        if (AttackEvent == null) AttackEvent = new UnityEvent();
        if (ChooseWeaponEvent == null) ChooseWeaponEvent = new UnityEvent();
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

}
