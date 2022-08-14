using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject chest;

    private void Start()
    {
        playerStats.LevelCompletedTrigger();
    }

    private void OnEnable()
    {
        playerStats.LevelCompleted.AddListener(OpenChest);
    }
    private void OnDisable()
    {
        playerStats.LevelCompleted.RemoveListener(OpenChest);
    }

    public void OpenChest()
    {
        chest.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            playerStats.SelectCardTrigger();
            chest.SetActive(true);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
