using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public CardSO[] cardsRefsComun;
    public CardSO[] cardsRefsSpecial;
    public CardInfos[] cardsToSelect;

    public int stage = 0;
    public PlayerStats playerStats;

    public GameObject selectScreen;
    public CardSO selectedSO;

    int random1;
    int random2;


    private void OnEnable()
    {
        playerStats.SelectCard.AddListener(OpenSelection);
    }
    private void OnDisable()
    {
        playerStats.SelectCard.RemoveListener(OpenSelection);
    }

    public void OpenSelection()
    {
        Debug.Log("OpenSelection");
        playerStats.canMove = false;
        selectScreen.SetActive(true);
        StartCoroutine(TurnCardUpWhenSelectedCards());
    }


    IEnumerator TurnCardUpWhenSelectedCards()
    {
        CardSO[] deckToChoose;
        stage++;
        if (stage <= 2) deckToChoose = cardsRefsComun;
        else deckToChoose = cardsRefsSpecial;
        
        yield return new WaitForSeconds(2f);
        int index = 0;

        if (stage <= 3)
        {           
            while (index <= 1)
            {
                int random = Random.Range(0, deckToChoose.Length);
                for (int i = 0; i < deckToChoose.Length; i++)
                {
                    if (i == random) selectedSO = deckToChoose[i];
                }

                for (int i = 0; i < cardsToSelect.Length; i++)
                {
                    if (i == index)
                    {
                        cardsToSelect[i].cardSO = selectedSO;
                        cardsToSelect[i].UpdateInfos();
                    }
                }
                index++;
                Debug.Log("Alow");
                Debug.Log(index);
            }
        }
        else
        {
            playerStats.canMove = true;
            playerStats.inChest = false;
            selectScreen.SetActive(false);
            playerStats.OpenDoorLevel();
            StopCoroutine(TurnCardUpWhenSelectedCards());
        }

        yield return new WaitUntil(()=> index >= 1);
        Debug.Log("Alow");
        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardUp");
        }
    }    

    public void ClickedCard()
    {
        Debug.Log("cliked");
        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardDown");
        }      
        StartCoroutine(TurnCardUpWhenSelectedCards());
    }
}
