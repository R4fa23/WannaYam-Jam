using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class HealthManager : MonoBehaviour
{
    [SerializeField] PlayerStats playerStats;
    [SerializeField] Image lifeFill;
    [SerializeField] TextMeshProUGUI maxLifeText;
    [SerializeField] TextMeshProUGUI currentLifeText;

    float maxLife;
    float curretLife;

    private void Start()
    {
        UpdateLifeBar();
    }

    public void UpdateLifeBar()
    {
        maxLife = playerStats.maxlife;
        curretLife = playerStats.currentLife;

        lifeFill.fillAmount = curretLife / maxLife;

        maxLifeText.text = Mathf.RoundToInt(maxLife).ToString();
        currentLifeText.text = Mathf.RoundToInt(curretLife).ToString();
    }

    private void OnEnable()
    {
        playerStats.LifeBarEvent.AddListener(UpdateLifeBar);
    }

    private void OnDisable()
    {
        playerStats.LifeBarEvent.RemoveListener(UpdateLifeBar);
    }
}
