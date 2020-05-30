using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TowerManager : MonoBehaviour
{
    public GameObject window;
    public GameObject tableCells;
    public GameObject CanvasButtons;

    public GameObject CardsCanvas;
    public GameObject background;
    public GameObject doorCanvas;

    public GameObject money;
    public GameObject level;
    public GameObject firstName;
    public GameObject secondName;
    public GameObject thirdName;
    public GameObject firstTotalExp;
    public GameObject secondTotalExp;
    public GameObject ThirdTotalExp;
    private int CardName = 0;
    public void GoToMission()
    {
        SceneManager.LoadScene("mapScene");
    }
    public void BuyCards()
    {
        foreach (Transform a in doorCanvas.transform)
        {
            a.gameObject.SetActive(false);
            if (a.gameObject.name == "return")
                a.gameObject.SetActive(true);
        }
        background.SetActive(false);
        CardsCanvas.SetActive(true);
        money.GetComponent<Text>().text = PlayerData.money + " coins";
        level.GetComponent<Text>().text = "level "+PlayerData.Level;
        for (int i=2; i<10; i++)
        {
            if(PlayerData.Level >= i+1)
            {
                if(PlayerData.IsCardBought[i] == true)
                {
                    foreach (Transform card in CardsCanvas.transform)
                    {
                        card.gameObject.SetActive(true);
                        if(card.gameObject.name.Contains((i+1).ToString()))
                        {
                            foreach (Transform component in card.transform)
                            {
                                if (component.gameObject.name == "Canvas")
                                {
                                    component.gameObject.SetActive(true);
                                    foreach (Transform CanvasComponent in component.transform)
                                    {
                                        if (CanvasComponent.gameObject.name == "LevelRequiredText")
                                        {
                                            CanvasComponent.gameObject.SetActive(true);
                                            CanvasComponent.GetComponent<Text>().text = "Sold";
                                        }
                                        if (CanvasComponent.gameObject.name == "BuyCard")
                                        {
                                            CanvasComponent.gameObject.SetActive(false);
                                        }
                                    }
                                }
                                if (component.gameObject.name == "LevelRequired")
                                {
                                    component.gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (Transform card in CardsCanvas.transform)
                    {
                        card.gameObject.SetActive(true);
                        if (card.gameObject.name.Contains((i + 1).ToString()))
                        {
                            foreach (Transform component in card.transform)
                            {
                                if (component.gameObject.name == "Canvas")
                                {
                                    component.gameObject.SetActive(true);
                                    foreach (Transform CanvasComponent in component.transform)
                                    {
                                        if (CanvasComponent.gameObject.name == "LevelRequiredText")
                                        {
                                            CanvasComponent.gameObject.SetActive(false);
                                        }
                                        if (CanvasComponent.gameObject.name == "BuyCard")
                                        {
                                            CanvasComponent.gameObject.SetActive(true);
                                        }
                                    }
                                }
                                if (component.gameObject.name == "LevelRequired")
                                {
                                    component.gameObject.SetActive(false);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (Transform card in CardsCanvas.transform)
                {
                    card.gameObject.SetActive(true);
                    if (card.gameObject.name.Contains((i + 1).ToString()))
                    {
                        foreach (Transform component in card.transform)
                        {
                            component.gameObject.SetActive(false);
                            if (component.gameObject.name == "Canvas")
                            {
                                component.gameObject.SetActive(true);
                                foreach (Transform CanvasComponent in component.transform)
                                {
                                    CanvasComponent.gameObject.SetActive(false);
                                    if (CanvasComponent.gameObject.name == "LevelRequiredText")
                                    {
                                        CanvasComponent.gameObject.SetActive(true);
                                    }
                                }
                            }
                            if (component.gameObject.name == "LevelRequired")
                            {
                                component.gameObject.SetActive(true);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Return()
    {
        foreach (Transform a in doorCanvas.transform)
        {
            a.gameObject.SetActive(true);
            if (a.gameObject.name == "return")
                a.gameObject.SetActive(false);
        }
        background.SetActive(true);
        CardsCanvas.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasButtons.gameObject.SetActive(false);
            window.gameObject.SetActive(true);
            tableCells.gameObject.SetActive(true);
            CardsCanvas.SetActive(false);

            firstName.GetComponent<Text>().text = PlayerData.firstPlaceName;
            secondName.GetComponent<Text>().text = PlayerData.secondPlaceName;
            thirdName.GetComponent<Text>().text = PlayerData.thirdPlaceName;

            firstTotalExp.GetComponent<Text>().text = PlayerData.firstPlaceTotalExp.ToString();
            secondTotalExp.GetComponent<Text>().text = PlayerData.secondPlaceTotalExp.ToString();
            ThirdTotalExp.GetComponent<Text>().text = PlayerData.thirdPlaceTotalExp.ToString();
        }
    }

    public void Continue()
    {
        CanvasButtons.gameObject.SetActive(true);
        window.gameObject.SetActive(false);
        tableCells.gameObject.SetActive(false);
        background.SetActive(true);
        foreach (Transform card in CanvasButtons.transform)
        {
            card.gameObject.SetActive(true);
            if (card.gameObject.name == "return")
                card.gameObject.SetActive(false);
        }
    }

    public void Restart()
    { Login.Restartt(); }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
    public void Pay(float payment)
    {
        CardName = (int)(payment/500+2);
        if(PlayerData.money >= payment)
        {
            PlayerData.money -= payment;
            PlayerData.IsCardBought[CardName - 1] = true;
            Login.UpdatePurchaseCardAndMoney(CardName - 1, PlayerData.money);
            BuyCards();
        }
        else
        {
            foreach (Transform card in CardsCanvas.transform)
            {
                card.gameObject.SetActive(true);
                if (card.gameObject.name.Contains(CardName.ToString()))
                {
                    foreach (Transform component in card.transform)
                    {
                        if (component.gameObject.name == "Canvas")
                        {
                            component.gameObject.SetActive(true);
                            foreach (Transform CanvasComponent in component.transform)
                            {
                                if (CanvasComponent.gameObject.name == "LevelRequiredText")
                                {
                                    CanvasComponent.gameObject.SetActive(true);
                                    CanvasComponent.GetComponent<Text>().text = "not enough money";
                                }
                                if (CanvasComponent.gameObject.name == "BuyCard")
                                {
                                    CanvasComponent.gameObject.SetActive(false);
                                }
                            }
                        }
                        if (component.gameObject.name == "LevelRequired")
                        {
                            component.gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
    }
}