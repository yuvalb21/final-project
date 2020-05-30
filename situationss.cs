using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//[System.Serializable]
public class situationss : MonoBehaviour
{
    private int currentLocation;
    public GameObject squarsArray;
    public GameObject windowCanvas;
    public GameObject window;
    public GameObject cube;
    public GameObject bar;
    public GameObject barCanvas;
    public GameObject backToTowerButton;

    //private PlayerData PlayerData;
    //private winOrLose winOrLose;
    //private int number;
    //private float num;
    //private float result;
    private int situation;
    private int exp;
    private int money;

    public situationss(int cl, GameObject SAS, GameObject _windowCanvas, GameObject _window, GameObject _cube, GameObject _bar,
        GameObject canvas, GameObject backtomap)
    {
        currentLocation = cl;
        squarsArray = SAS;
        windowCanvas = _windowCanvas;
        window = _window;
        cube = _cube;
        bar = _bar;
        barCanvas = canvas;
        backToTowerButton = backtomap;
    }

    private GameObject pickSquare(int num)
    {
        foreach (Transform SquareFloor in squarsArray.transform)
        {
            if (SquareFloor.gameObject.name == "sqaure" + num.ToString())
            {
                GameObject block = SquareFloor.gameObject;
                return block;
            }
        }
        Debug.Log("fail");
        return null;
    }
    public void actions()
    {
        GameObject currentBlock = pickSquare(currentLocation);
        foreach (Transform x in currentBlock.transform)
        {
            if (x.gameObject.name == "TreasureBox" && x.gameObject.activeSelf == true)
            {
                enableWindow(0);
            }
            if (x.gameObject.name == "SurpriseBag" && x.gameObject.activeSelf == true)
            {
                int n = (int)(Random.Range(0f, 11f));
                if (n < 4)
                    enableWindow(1);//money
                else
                    enableWindow(2);//transfer
            }
            if (x.gameObject.name == "Enemy" && x.gameObject.activeSelf == true)
            {
                enableWindow(3);
            }
        }
    }

    private void enableWindow(int num)
    {
        situation = num;
        Debug.Log("situation: " + situation);
        window.gameObject.SetActive(true);
        foreach (Transform s in windowCanvas.transform)
        {
            if (situation == 0 && s.gameObject.name == "treasureBoxText")
            {
                exp = (int)(Random.Range(15f, 31f));
                PlayerData.experience += (float)exp;

                money = (int)(Random.Range(15f, 31f));
                PlayerData.money += (float)money;
                Login.UpdateExpAndMoney(money, exp);
                Debug.Log("money: " + money);

                s.gameObject.SetActive(true);
                foreach (Transform a in s.gameObject.transform)
                {
                    if (a.gameObject.name == "expAmount")
                    {
                        a.GetComponent<Text>().text = exp.ToString();
                    }
                    if (a.gameObject.name == "moneyAmount")
                    {
                        a.GetComponent<Text>().text = money.ToString();
                    }
                    if (a.gameObject.name == "youGet")
                    {
                        a.GetComponent<Text>().text = PlayerData.PlayerName.ToString() + " get:";
                    }
                }
            }
            else if (situation == 1 && s.gameObject.name == "surpriseBagMoneyText")
            {
                money = (int)(Random.Range(15f, 31f));
                PlayerData.money += (float)money;
                Login.UpdateMoney(PlayerData.money);

                s.gameObject.SetActive(true);
                foreach (Transform a in s.gameObject.transform)
                {
                    if (a.gameObject.name == "moneyAmount")
                    {
                        a.GetComponent<Text>().text = money.ToString();
                    }
                    if (a.gameObject.name == "youGet")
                    {
                        a.GetComponent<Text>().text = PlayerData.PlayerName.ToString() + " get:";
                    }
                }
            }
            else if (situation == 2 && s.gameObject.name == "surpriseBagMovementText")
            {
                s.gameObject.SetActive(true);
            }
            else if (situation == 3 && s.gameObject.name == "enemyText")
            {
                foreach (Transform a in s.gameObject.transform)
                {
                    if (a.gameObject.name == "name")
                    {
                        a.GetComponent<Text>().text = PlayerData.PlayerName.ToString();
                    }
                }
                s.gameObject.SetActive(true);
            }
        }
    }

    public void AfterContinueButton()
    {
        foreach (Transform s in windowCanvas.transform)
        {
            if(s.gameObject.name != "Button")
                s.gameObject.SetActive(false);
        }
        window.gameObject.SetActive(false);
        deleteCurrentSquarsFiture();
        if (situation == 0 || situation == 1)
            updateExpAndMoney();
        if (situation == 2)
            SurpriseBagTransform();
        if (situation == 3)
            Duel();
        if(situation == 5)
        {
            levelUp();
        }
        if(situation == 7)
        {
            missionAccomplished();
        }

        if(situation == 9)
        {
            if (PlayerData.experience < PlayerData.expGoal)
            {
                bar.transform.localScale = new Vector3(1f, -(PlayerData.experience / PlayerData.expGoal), 1f);
                cube.gameObject.GetComponent<Button>().interactable = true;
                backToTowerButton.gameObject.SetActive(true);
            }
            else
            {
                levelUp();
            }
        }
    }
    private void updateExpAndMoney()
    {
        foreach (Transform a in barCanvas.transform)
        {
            if (a.gameObject.name == "currentExp")
            {
                a.GetComponent<Text>().text = PlayerData.experience.ToString();
            }
            if (a.gameObject.name == "money")
            {
                a.GetComponent<Text>().text = PlayerData.money.ToString();
            }
        }
        if (PlayerData.experience < PlayerData.expGoal)
        {
            bar.transform.localScale = new Vector3(1f, -(PlayerData.experience / PlayerData.expGoal), 1f);
            cube.gameObject.GetComponent<Button>().interactable = true;
        }
        else
        {
            levelUp();
        }
    }

    private void SurpriseBagTransform()
    {
        bool notEmpty = true;
        while (notEmpty == true)
        {
            notEmpty = false;
            Debug.Log("transform situation2");
            int x = (int)(Random.Range(0f, 55f));
            while(x == currentLocation)
                x = (int)(Random.Range(0f, 55f));
            foreach (Transform a in squarsArray.transform)
            {
                if (a.gameObject.name == "sqaure" + x.ToString())
                {
                    GameObject block = pickSquare(x);
                    foreach (Transform r in block.transform)
                    {
                        if ((r.gameObject.name == "Enemy" && r.gameObject.activeSelf == true) || (r.gameObject.name == "SurpriseBag" && r.gameObject.activeSelf == true) ||
                            (r.gameObject.name == "TreasureBox" && r.gameObject.activeSelf == true))
                        {
                            notEmpty = true;
                        }
                        if (r.gameObject.name == "player" && notEmpty == false)
                            r.gameObject.SetActive(true);
                    }
                    setLocation(x);
                }
            }
        }
        cube.gameObject.GetComponent<Button>().interactable = true;
    }
    private void Duel()
    {
        Debug.Log("duel");
        if (currentLocation == 55)
            winOrLose.isBoss = true;
        else
            winOrLose.isBoss = false;
        SceneManager.LoadScene("fightScene");

        //cube.gameObject.GetComponent<Button>().interactable = true;
    }

    public void setLocation(int newLocation)
    {
        currentLocation = newLocation;
    }

    public void setBarAndData()
    {
        bar.transform.localScale = new Vector3(0f, 0f, 0f);
        bar.transform.localScale = new Vector3(1f, (-PlayerData.experience / PlayerData.expGoal), 1f);
        Debug.Log(PlayerData.experience / PlayerData.expGoal);
        foreach (Transform a in barCanvas.transform)
        {
            if (a.gameObject.name == "currentExp")
            {
                a.GetComponent<Text>().text = PlayerData.experience.ToString();
            }
            if (a.gameObject.name == "money")
            {
                a.GetComponent<Text>().text = PlayerData.money.ToString();
            }
            if(a.gameObject.name == "goalExp")
            {
                a.GetComponent<Text>().text = PlayerData.expGoal.ToString();
            }
            if (a.gameObject.name == "level")
            {
                a.GetComponent<Text>().text = PlayerData.Level.ToString();
            }
        }
    }

    private void deleteCurrentSquarsFiture()
    {
        GameObject block = pickSquare(currentLocation);
        foreach (Transform r in block.transform)
        {
            if (situation == 5)
            {
                if (r.gameObject.name == "Enemy")
                {
                    r.gameObject.SetActive(false);
                    updateExpAndMoney();
                }
            }
            else
            {
                if (r.gameObject.name != "player")
                    r.gameObject.SetActive(false);
                if (situation == 2)
                {
                    if (r.gameObject.name == "player")
                        r.gameObject.SetActive(false);
                }
            }
        }
    }

    public void AfterWin()
    {
        situation = 5;//if the player won
        Debug.Log("player win player win");
        foreach (Transform s in windowCanvas.transform)
        {
            if (s.gameObject.name == "playerWon")
            {
                exp = (int)(Random.Range(40f, 61f));
                PlayerData.experience += (float)exp;

                money = (int)(Random.Range(25f, 41f));
                PlayerData.money += (float)money;
                Login.UpdateExpAndMoney(money, exp);

                window.gameObject.SetActive(true);
                s.gameObject.SetActive(true);
                foreach (Transform a in s.gameObject.transform)
                {
                    if (a.gameObject.name == "expAmount")
                    {
                        a.GetComponent<Text>().text = exp.ToString();
                    }
                    if (a.gameObject.name == "moneyAmount")
                    {
                        a.GetComponent<Text>().text = money.ToString();
                    }
                    if(a.gameObject.name == "youGet")
                    {
                        a.GetComponent<Text>().text = PlayerData.PlayerName.ToString() + " get: ";
                    }
                }
            }
        }
    }

    public void levelUp()
    {
        if (PlayerData.experience >= PlayerData.expGoal)
        {
            PlayerData.LevelUp();
            situation = 7;
            foreach (Transform s in windowCanvas.transform)
            {
                if (s.gameObject.name == "levelUp")
                {
                    money = (int)(40f + PlayerData.Level * 10f);
                    PlayerData.money += (float)money;
                    Login.UpdateMoneyAndLevel(PlayerData.money, PlayerData.experience);

                    window.gameObject.SetActive(true);
                    s.gameObject.SetActive(true);
                    foreach (Transform a in s.gameObject.transform)
                    {
                        if (a.gameObject.name == "moneyAmount")
                        {
                            a.GetComponent<Text>().text = money.ToString();
                        }
                        if (a.gameObject.name == "youGet")
                        {
                            a.GetComponent<Text>().text = PlayerData.PlayerName.ToString() + " get: ";
                        }
                    }
                }
            }
            setBarAndData();
        }
        else
            missionAccomplished();
    }

    public void missionAccomplished()
    {
        bool isMissiomAccomplished = true;
        for (int i = 0; i < 56; i++)
        {
            GameObject b = pickSquare(i);
            foreach (Transform Square in b.transform)
            {
                if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
                    isMissiomAccomplished = false;
            }
        }
        if (isMissiomAccomplished == true)
        {
            situation = 9;
            foreach (Transform s in windowCanvas.transform)
            {
                if (s.gameObject.name == "missionAccomplished")
                {
                    exp = 60;
                    PlayerData.experience += (float)exp;

                    money = 60;
                    PlayerData.money += (float)money;
                    Login.UpdateExpAndMoney(money, exp);

                    window.gameObject.SetActive(true);
                    s.gameObject.SetActive(true);
                    foreach (Transform a in s.gameObject.transform)
                    {
                        if (a.gameObject.name == "expAmount")
                        {
                            a.GetComponent<Text>().text = exp.ToString();
                        }
                        if (a.gameObject.name == "moneyAmount")
                        {
                            a.GetComponent<Text>().text = money.ToString();
                        }
                        if (a.gameObject.name == "youGet")
                        {
                            a.GetComponent<Text>().text = PlayerData.PlayerName.ToString() + " get: ";
                        }
                    }
                }
            }
        }
        else
            backToTowerButton.gameObject.SetActive(true);
    }

    public void ReturnToTheTower()
    {
        bool isMissiomAccomplished = true;
        for (int i = 0; i < 56; i++)
        {
            GameObject b = pickSquare(i);
            foreach (Transform Square in b.transform)
            {
                if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
                    isMissiomAccomplished = false;
            }
        }
        if (isMissiomAccomplished == true)
        {
            winOrLose.isFirstDuelOver = false;
            SceneManager.LoadScene("towerScene");
        }
        else
        {
            foreach (Transform s in windowCanvas.transform)
            {
                window.gameObject.SetActive(true);
                if (s.gameObject.name == "MissionDidntAccomplished")
                    s.gameObject.SetActive(true);

                if (s.gameObject.name == "Button")
                    s.gameObject.SetActive(false);
            }
        }
    }

    public void MissionDidntAccomplished(bool stay)
    {
        if (stay)
        {
            foreach (Transform s in windowCanvas.transform)
            {
                window.gameObject.SetActive(false);
                if (s.gameObject.name == "MissionDidntAccomplished")
                    s.gameObject.SetActive(false);

                if (s.gameObject.name == "Button")
                    s.gameObject.SetActive(true);
            }
        }
        else
        {
            winOrLose.isFirstDuelOver = false;
            SceneManager.LoadScene("towerScene");
        }
    }
}
