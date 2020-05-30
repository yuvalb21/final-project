using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class screenManager
{
    public GameObject continueButton;
    public GameObject screen;
    public GameObject player;
    public GameObject BlocksArray;
    public GameObject enemy;
    public GameObject courtPositionsArray;

    private bool a;
    public screenManager(GameObject _player, GameObject bArray, GameObject canvass, GameObject background, GameObject _enemy, GameObject _courtPositionsArray)
    {
        player = _player;
        BlocksArray = bArray;
        continueButton = canvass;
        screen = background;
        enemy = _enemy;
        courtPositionsArray = _courtPositionsArray;
    }

    public void isScreen()
    {
        screen.SetActive(!screen.activeSelf);
        player.SetActive(!player.activeSelf);
        enemy.SetActive(!enemy.activeSelf);
        BlocksArray.SetActive(!BlocksArray.activeSelf);
        courtPositionsArray.SetActive(!courtPositionsArray.activeSelf);
        foreach (Transform ChosenButton in continueButton.transform)
        {
            if (ChosenButton.gameObject.name == "Disappear")
            {
                ChosenButton.gameObject.SetActive(!ChosenButton.gameObject.activeSelf);
                SortCards();
                a = ChosenButton.gameObject.activeSelf;
            }
        }

        foreach (Transform ChosenButton in continueButton.transform)
        {
            if (ChosenButton.gameObject.name == "ContinueButton")
                ChosenButton.gameObject.SetActive(a);
        }
    }

    public void isContinueB()
    {
        foreach (Transform ChosenButton in continueButton.transform)
        {
            if (ChosenButton.gameObject.name == "ContinueButton")
                ChosenButton.gameObject.SetActive(!ChosenButton.gameObject.activeSelf);
        }
    }

    private void SortCards()
    {
        foreach (Transform ChosenButton in continueButton.transform)
        {
            if (ChosenButton.gameObject.name == "Disappear")
            {
                foreach (Transform x in ChosenButton.gameObject.transform)
                {
                    for(int i=0; i<PlayerData.IsCardBought.Length; i++)
                    {
                        if(x.gameObject.name == PlayerData.GetCardName(i))
                        {
                            x.gameObject.SetActive(PlayerData.IsCardBought[i]);
                        }
                    }
                }
            }
        }
    }
}