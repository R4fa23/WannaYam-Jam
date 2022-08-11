using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaRecharger : MonoBehaviour
{
    public float staminaGain = 0.2f;
    UIStaminaManager staminaManager;
    public bool recharging;
    public Image image;
    public int point;
    public bool recharged;
    public bool canRecharge;

    private void Awake()
    {
        staminaManager = FindObjectOfType<UIStaminaManager>();
        image = GetComponent<Image>();
    }

    void Update()
    {      
       
    }
}
