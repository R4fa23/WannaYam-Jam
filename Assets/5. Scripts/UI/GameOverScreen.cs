using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] GameObject DeathScreen;
    [SerializeField] PlayerStats playerStats;
    
    public void TurnOnPanel()
    {
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(2f);
        DeathScreen.SetActive(true);
    }

    private void OnEnable()
    {
        playerStats.DeathEvent.AddListener(TurnOnPanel);
    }

    private void OnDisable()
    {
        playerStats.DeathEvent.RemoveListener(TurnOnPanel);
    }

    public void Restart()
    {
        playerStats.ResetValues();
        SceneManager.LoadScene("Level_00");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
