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
        StartCoroutine(OpeningSelectionTimer());
    }

    IEnumerator OpeningSelectionTimer()
    {
        WichCardType();

        yield return new WaitForSeconds(2f);

        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardUp");
        }
    }

    IEnumerator ClosingSelection()
    {
        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardDown");
        }

        yield return new WaitForSeconds(3f);

        playerStats.canMove = true;
        selectScreen.SetActive(false);
        //Door
    }

    public void ClickedCard()
    {
        Debug.Log("cliked");
        StartCoroutine(SelectAndFlipCard());
    }

    IEnumerator SelectAndFlipCard()
    {
        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardDown");
        }
        WichCardType();

        yield return new WaitForSeconds(1f);

        foreach (var card in cardsToSelect)
        {
            card.GetComponent<Animator>().SetTrigger("TurnCardUp");
        }
    }

    public void WichCardType()
    {
        if (stage < 3)
        {            
            if (stage <= 1) ChooseAComunCard();
            else ChooseASpecialCard();
        }
        else StartCoroutine(ClosingSelection());        
    }

    public void ChooseAComunCard()
    {
        int randomCard = Random.Range(0, cardsRefsComun.Length + 1);
        Debug.Log(randomCard);

        for (int i = 0; i < cardsRefsComun.Length; i++)
        {
            if(i == randomCard) selectedSO = cardsRefsComun[i];            
        }

        for (int i = 0; i < cardsToSelect.Length; i++)
        {
            if (i == 0) 
            {
                cardsToSelect[i].cardSO = selectedSO;
                //cardsToSelect[i].UpdateInfos();
            }
        }

        randomCard = Random.Range(0, cardsRefsComun.Length + 1);
        Debug.Log(randomCard);

        for (int i = 0; i < cardsRefsComun.Length; i++)
        {
            if (i == randomCard) selectedSO = cardsRefsComun[i];            
        }

        for (int i = 0; i < cardsToSelect.Length; i++)
        {
            if (i == 1)
            {
                cardsToSelect[i].cardSO = selectedSO;
                //cardsToSelect[i].UpdateInfos();
            }
        }
        stage++;
    }

    public void ChooseASpecialCard()
    {
        int randomCard = Random.Range(0, cardsRefsSpecial.Length + 1);

        for (int i = 0; i < cardsRefsSpecial.Length; i++)
        {
            if (i == randomCard) selectedSO = cardsRefsSpecial[i];
            
        }

        for (int i = 0; i < cardsRefsSpecial.Length; i++)
        {
            if (i == 0)
            {
                cardsToSelect[i].cardSO = selectedSO;
                cardsToSelect[i].UpdateInfos();
            }
        }

        randomCard = Random.Range(0, cardsRefsSpecial.Length + 1);

        for (int i = 0; i < cardsRefsSpecial.Length; i++)
        {
            if (i == randomCard) selectedSO = cardsRefsSpecial[i];
            
        }

        for (int i = 0; i < cardsToSelect.Length; i++)
        {
            if (i == 1)
            {
                cardsToSelect[i].cardSO = selectedSO;
                cardsToSelect[i].UpdateInfos();
            }
        }
        stage++;
    }

}
