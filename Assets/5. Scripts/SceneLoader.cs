using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string[] level01;
    public string[] level02;
    public string[] level03;
    public string[] level04;
    public string[] level05;

    string[] levelToLoad;

    [SerializeField] PlayerStats playerStats;

    public void LoadLevel()
    {
        playerStats.wichLevel++;

        switch (playerStats.wichLevel)
        {
            case 1:
                levelToLoad = level01;
                break;
            case 2:
                levelToLoad = level02;
                break;
            case 3:
                levelToLoad = level03;
                break;
            case 4:
                levelToLoad = level04;
                break;
            case 5:
                levelToLoad = level05;
                break;
            default:
                break;
        }

        int randomLevel = Random.Range(0, levelToLoad.Length+1);

        for (int i = 0; i < levelToLoad.Length; i++)
        {
            if(randomLevel == i)
            {                
                SceneManager.LoadScene(levelToLoad[i].ToString());
            }
        }
    }

}
