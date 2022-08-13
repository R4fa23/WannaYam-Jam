using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIStaminaManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    public Image image;


    void Update()
    {
        float currentStamina = playerStats.currentStamina;

        image.fillAmount = currentStamina;

        if (currentStamina >= playerStats.maxstamina / playerStats.dashUses) image.color = Color.cyan;
        else image.color = Color.grey;

    }

}
