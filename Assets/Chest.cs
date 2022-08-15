using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject chest;   

    private void OnEnable()
    {
        playerStats.LevelCompleted.AddListener(OpenChest);
    }
    private void OnDisable()
    {
        playerStats.LevelCompleted.RemoveListener(OpenChest);
    }

    private void Start()
    {
        
    }

    public void OpenChest()
    {
        chest.SetActive(true);
        //playerStats.canMove = false;
        //playerStats.inChest = true;
        gameObject.GetComponent<BoxCollider>().isTrigger = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == ("Player"))
        {
            playerStats.canMove = false;
            playerStats.SelectCardTrigger();
            chest.SetActive(false);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }

}
