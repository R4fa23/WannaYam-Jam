using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSelector : MonoBehaviour
{
    public CardSO[] cardsRefsComun;
    public CardSO[] cardsRefsSpecial;
    public CardInfos cardSelect01;
    public CardInfos cardSelect02;
    public int stage = 0;
    public PlayerStats playerStats;

    public GameObject selectScreen;



    public void OpenSelection()
    {
        playerStats.canMove = false;
        selectScreen.SetActive(true);
    }
    public void CloseSelection()
    {
        playerStats.canMove = true;
        selectScreen.SetActive(false);
        //Door
    }

    public void WichCardType()
    {
        if (stage > 1) ChooseAComunCard();
        else ChooseASpecialCard();
    }

    public void ChooseAComunCard()
    {
        int randomCard = Random.Range(0, cardsRefsComun.Length + 1);

        for (int i = 0; i < cardsRefsComun.Length; i++)
        {
            if(i == randomCard)
            {
                cardSelect01.cardSO = cardsRefsComun[i];
            }
        }

        randomCard = Random.Range(0, cardsRefsComun.Length + 1);

        for (int i = 0; i < cardsRefsComun.Length; i++)
        {
            if (i == randomCard)
            {
                cardSelect02.cardSO = cardsRefsComun[i];
            }
        }

        stage++;
    }

    public void ChooseASpecialCard()
    {
        int randomCard = Random.Range(0, cardsRefsSpecial.Length + 1);

        for (int i = 0; i < cardsRefsSpecial.Length; i++)
        {
            if (i == randomCard)
            {
                cardSelect01.cardSO = cardsRefsSpecial[i];
            }
        }

        randomCard = Random.Range(0, cardsRefsSpecial.Length + 1);

        for (int i = 0; i < cardsRefsSpecial.Length; i++)
        {
            if (i == randomCard)
            {
                cardSelect02.cardSO = cardsRefsSpecial[i];
            }
        }
        CloseSelection();
    }

}
