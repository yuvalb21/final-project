using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class orderSquares : MonoBehaviour
{
    public GameObject squaresArray;
    public GameObject cubeText;
    public GameObject ArrowsArray;
    public GameObject cube;
    public GameObject plus1Cube;
    public GameObject minus1Cube;
    public GameObject ThrowAgain;
    public GameObject window;
    public GameObject windowCanvas;
    public GameObject bar;
    public GameObject barCanvas;
    public GameObject backToTowerButton;
    public GameObject backToTheTower;

    public GameObject TableWindow;
    public GameObject tableCells;
    public GameObject CanvasButtons;


    public GameObject firstName;
    public GameObject secondName;
    public GameObject thirdName;
    public GameObject firstTotalExp;
    public GameObject secondTotalExp;
    public GameObject ThirdTotalExp;

    private situationss situationss;

    private int CurrentPlayerLocation;
    private string[] StringSquareArray = new string[56];
    private int routesNumber = 1;
    private static int constant = 7;
    private string[,] possibleRouts = new string[constant, constant];
    private int number = 0;
    private int counter;
    private int otherNum = 0;
    int[] doubles = new int[4];
    int enemies;

    private void Start()
    {
        if (winOrLose.isFirstDuelOver == false)
        {
            winOrLose.start();
            //PlayerData.startt();
            CurrentPlayerLocation = 0;

            situationss = new situationss(CurrentPlayerLocation, squaresArray, windowCanvas, window, cube, bar, barCanvas, backToTowerButton);
            situationss.setBarAndData();

            int treasures = 5;
            int enemies = 4;
            int surpriseBag = 3;
            bool isEnemy = true;
            bool isTreasure = true;
            bool isSurpriseBag = true;
            int Ienemy = 0;
            int Itreasure = 0;
            int IsurpriseBag = 0;
            int sum = treasures + enemies + surpriseBag;

            plus1Cube.gameObject.GetComponent<Button>().interactable = false;
            minus1Cube.gameObject.GetComponent<Button>().interactable = false;
            ThrowAgain.gameObject.GetComponent<Button>().interactable = false;

            while (sum != 0)
            {
                for (int j = 1; j < 55; j++)
                {
                    resett(j);
                }
                Ienemy = 0;
                Itreasure = 0;
                IsurpriseBag = 0;
                treasures = 5;
                enemies = 4;
                surpriseBag = 3;
                isEnemy = true;
                isTreasure = true;
                isSurpriseBag = true;

                for (int i = 1; i < 55; i++)
                {
                    int num = (int)(Random.Range(0f, 13f));
                    GameObject block = pickSquare(i);
                    foreach (Transform Square in block.transform)
                    {
                        if (num == 0 && enemies > 0 && isEnemy == true && (Ienemy + 1 != i) && (IsurpriseBag + 1 != i) && (Itreasure + 1 != i) &&
                            (Ienemy + 2 != i) && (IsurpriseBag + 2 != i) && (Itreasure + 2 != i))
                        {
                            if (Square.gameObject.name == "Enemy")
                            {
                                enemies--;
                                isEnemy = false;
                                Ienemy = i;
                                Square.gameObject.SetActive(true);
                            }
                        }
                        else if (num == 4 && treasures > 0 && isTreasure == true && (Itreasure + 1 != i) && (IsurpriseBag + 1 != i) &&
                            (Ienemy + 1 != i) && (Itreasure + 2 != i) && (IsurpriseBag + 2 != i) && (Ienemy + 2 != i))
                        {
                            if (Square.gameObject.name == "TreasureBox")
                            {
                                treasures--;
                                isTreasure = false;
                                Square.gameObject.SetActive(true);
                                Itreasure = i;
                            }
                        }
                        else if (num == 8 && surpriseBag > 0 && isSurpriseBag == true && (IsurpriseBag + 1 != i) && (Itreasure + 1 != i) && (Ienemy + 1 != i) &&
                            (IsurpriseBag + 2 != i) && (Itreasure + 2 != i) && (Ienemy + 2 != i))
                        {
                            if (Square.gameObject.name == "SurpriseBag")
                            {
                                surpriseBag--;
                                isSurpriseBag = false;
                                Square.gameObject.SetActive(true);
                                IsurpriseBag = i;
                            }
                        }
                    }
                    isEnemy = true;
                    isTreasure = true;
                    isSurpriseBag = true;
                }
                sum = treasures + enemies + surpriseBag;
                //Debug.Log("sum: " + sum + " enemies: " + enemies + " treasures: " + treasures + " surprisebags: " + surpriseBag);
            }
        }
        else
        {
            situationss = new situationss(winOrLose.playerLocation, squaresArray, windowCanvas, window, cube, bar, barCanvas, backToTowerButton);
            situationss.setBarAndData();

            ThrowAgain.gameObject.SetActive(winOrLose.throwAgain);
            plus1Cube.gameObject.SetActive(winOrLose.plus1);
            minus1Cube.gameObject.SetActive(winOrLose.minus1);

            GameObject b = pickSquare(55);
            foreach (Transform Square in b.transform)
            {
                if (Square.gameObject.activeSelf == true)
                    Square.gameObject.SetActive(false);
            }
            b = pickSquare(0);
            foreach (Transform Square in b.transform)
            {
                if (Square.gameObject.activeSelf == true)
                    Square.gameObject.SetActive(false);
            }
            for (int i=0; i<StringSquareArray.Length; i++)
            {
                b = pickSquare(i);
                foreach (Transform Square in b.transform)
                {
                    for(int f=0; f<winOrLose.treasureBoxesArray.Length; f++)
                    {
                        if(i == winOrLose.playerLocation)
                        {
                            if (Square.gameObject.name == "player")
                                Square.gameObject.SetActive(true);
                        }
                        if(i == winOrLose.treasureBoxesArray[f] && i != 0)
                        {
                            if (Square.gameObject.name == "TreasureBox")
                                Square.gameObject.SetActive(true);
                        }
                        if (i == winOrLose.enemiesArray[f] && i != 0)
                        {
                            if (Square.gameObject.name == "Enemy")
                                Square.gameObject.SetActive(true);
                        }
                        if(f < 3 && i == winOrLose.surpriseBagsArray[f] && i != 0)
                        {
                            if (Square.gameObject.name == "SurpriseBag")
                                Square.gameObject.SetActive(true);
                        }
                    }
                }
            }
            if (winOrLose.win == true)
            {
                Debug.Log("player win player win");
                PlayerData.button = false;
                situationss.AfterWin();
                //levelUpOrAccomplishedMission();
            }
            
        }
    }

    private GameObject pickSquare(int num)
    {
        foreach (Transform SquareFloor in squaresArray.transform)
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

    private void resett(int num)
    {
        foreach (Transform SquareFloor in squaresArray.transform)
        {
            if (SquareFloor.gameObject.name == "sqaure" + num.ToString())
            {
                GameObject block = SquareFloor.gameObject;
                foreach (Transform Square in block.transform)
                {
                    Square.gameObject.SetActive(false);
                }
            }
        }
    }

    public void MoveOptions(int situation)
    {
        counter = 0;
        SArray();

        for (int i = 0; i < StringSquareArray.Length; i++)
        {
            GameObject go = pickSquare(i);
            foreach (Transform x in go.transform)
            {
                if (x.gameObject.name == "player" && x.gameObject.activeSelf == true)
                {
                    CurrentPlayerLocation = i;
                }
            }
        }
        string[] TotalPossibleRoutes = StringSquareArray[CurrentPlayerLocation].Split(',');
        //number = 5;

        if (situation == 1)
        {
            number++;
            plus1Cube.gameObject.SetActive(false);
            if(minus1Cube.gameObject.activeSelf == true)
                minus1Cube.gameObject.GetComponent<Button>().interactable = false;
            if (ThrowAgain.gameObject.activeSelf == true)
                ThrowAgain.gameObject.GetComponent<Button>().interactable = false;
        }
        if (situation == 2)
        {
            number--;
            minus1Cube.gameObject.SetActive(false);
            if (plus1Cube.gameObject.activeSelf == true)
                plus1Cube.gameObject.GetComponent<Button>().interactable = false;
            if (ThrowAgain.gameObject.activeSelf == true)
                ThrowAgain.gameObject.GetComponent<Button>().interactable = false;
        }
        if(situation == 3)
        {
            number = (int)(Random.Range(1f, 7f));
            ThrowAgain.gameObject.SetActive(false);
            if (plus1Cube.gameObject.activeSelf == true)
                plus1Cube.gameObject.GetComponent<Button>().interactable = false;
            if (minus1Cube.gameObject.activeSelf == true)
                minus1Cube.gameObject.GetComponent<Button>().interactable = false;
        }
        if(situation != 0)
        {
            foreach (Transform x in ArrowsArray.transform)
            {
                x.gameObject.SetActive(false);
            }
        }
        if (situation == 0 || situation == 3)
        {
            number = (int)(Random.Range(1f, 7f));
            cubeText.GetComponent<Text>().text = number.ToString();
        }
        if(situation == 0)
        {
            if (plus1Cube.gameObject.activeSelf == true)
                plus1Cube.gameObject.GetComponent<Button>().interactable = true;

            if (minus1Cube.gameObject.activeSelf == true)
                minus1Cube.gameObject.GetComponent<Button>().interactable = true;

            if(number == 1)
                minus1Cube.gameObject.GetComponent<Button>().interactable = false;

            if (ThrowAgain.gameObject.activeSelf == true)
                ThrowAgain.gameObject.GetComponent<Button>().interactable = true;
        }

        cube.gameObject.GetComponent<Button>().interactable = false;

        for (int y = 0; y < constant; y++)
        {
            for (int e = 0; e < constant; e++)
            {
                possibleRouts[y, e] = "";
            }
        }

        for (int i = 0; i < TotalPossibleRoutes.Length; i++)
        {
            if (i != 0)
                counter++;
            possibleRouts[counter, 0] = TotalPossibleRoutes[i];
            Debug.Log("counter: " + counter);
            TheOptions(int.Parse(TotalPossibleRoutes[i]), CurrentPlayerLocation);
        }
    }

    public void Movement(int destination)
    {
        plus1Cube.gameObject.GetComponent<Button>().interactable = false;
        minus1Cube.gameObject.GetComponent<Button>().interactable = false;
        ThrowAgain.gameObject.GetComponent<Button>().interactable = false;

        for (int j=0; j<6; j++)
        {
            if(possibleRouts[j,number-1] == destination.ToString())
                StartCoroutine(PlayerMovementt(j));
        }
    }

    IEnumerator PlayerMovementt(int ChosenRoute)
    {
        foreach (Transform x in ArrowsArray.transform)
        {
            x.gameObject.SetActive(false);
        }
        GameObject currentLocation = pickSquare(CurrentPlayerLocation);
        foreach (Transform Square in currentLocation.transform)
        {
            if (Square.gameObject.name == "player")
            {
                Square.gameObject.SetActive(false);
            }
        }
        for (int i = 0; i < number; i++)
        {
            if(i != 0)
            {
                currentLocation = pickSquare(int.Parse(possibleRouts[ChosenRoute, i-1]));
                foreach (Transform Square in currentLocation.transform)
                {
                    if (Square.gameObject.name == "player")
                    {
                        Square.gameObject.SetActive(false);
                    }
                }
            }
            currentLocation = pickSquare(int.Parse(possibleRouts[ChosenRoute,i]));
            foreach (Transform Square in currentLocation.transform)
            {
                if (Square.gameObject.name == "player")
                {
                    Square.gameObject.SetActive(true);
                }
            }
            yield return new WaitForSeconds(1);
        }
        EnemiesMovement();
        yield return new WaitForSeconds(1);
        for (int i = 0; i < StringSquareArray.Length; i++)
        {
            GameObject go = pickSquare(i);
            foreach (Transform x in go.transform)
            {
                if (x.gameObject.name == "player" && x.gameObject.activeSelf == true)
                {
                    CurrentPlayerLocation = i;
                    situationss.setLocation(CurrentPlayerLocation);
                }
            }
        }
        Debug.Log("current loaction: " + CurrentPlayerLocation);
        GameObject o = pickSquare(CurrentPlayerLocation);
        bool notEmpty = false;
        foreach (Transform x in o.transform)
        {
            if ((x.gameObject.name == "Enemy" && x.gameObject.activeSelf == true) || (x.gameObject.name == "SurpriseBag" && x.gameObject.activeSelf == true) ||
                (x.gameObject.name == "TreasureBox" && x.gameObject.activeSelf == true))
            {
                notEmpty = true;
            }
        }
        if (notEmpty == true)
        {
            rememberLocations();
            situationss.actions();
        }
        else
            cube.gameObject.GetComponent<Button>().interactable = true;
    }

    private void SArray()
    {
        for (int i = 0; i < StringSquareArray.Length; i++)
        {
            StringSquareArray[i] = "";
        }

        StringSquareArray[0] = "1";
        StringSquareArray[55] = "54";
        StringSquareArray[40] = "39";
        StringSquareArray[35] = "34";

        StringSquareArray[4] = "3,5,20,25";
        StringSquareArray[20] = "4,21";
        StringSquareArray[25] = "4,26";

        StringSquareArray[19] = "18,24,36,41";
        StringSquareArray[24] = "19,23";
        StringSquareArray[36] = "19,37";
        StringSquareArray[41] = "19,42";

        StringSquareArray[31] = "30,32,44";
        StringSquareArray[44] = "31,45";

        StringSquareArray[50] = "43,49,51";
        StringSquareArray[43] = "42,50";



        for (int i = 0; i < StringSquareArray.Length; i++)
        {
            if (i != 0 && i != 55 && i != 40 && i != 35 && i != 4 && i != 20 && i != 25 && i != 19 &&
                i != 24 && i != 36 && i != 41 && i != 31 && i != 44 && i != 50 && i != 43)
            {
                StringSquareArray[i] = (i - 1).ToString() + "," + (i + 1).ToString();
            }
        }
    }

    private void TheOptions(int NextLocation, int _last)
    {
        int cpl = NextLocation;
        int lastLocation = _last;
        bool SkipCell = false;
        routesNumber = 1;
        
        
        for(int j=0; j<routesNumber; j++)
        {
            if (j > 0)
            {
                possibleRouts[counter,0] = possibleRouts[counter - 1,0];
            }
            for (int i = 1; i < number; i++)
            {
                string[] a = StringSquareArray[cpl].Split(',');
                if (a.Length == 1)
                {
                    lastLocation = cpl;
                    cpl = int.Parse(a[0]);
                    possibleRouts[counter, i] = cpl.ToString();
                }
                if (a.Length == 2)
                {
                    if (a[0] == lastLocation.ToString())
                    {
                        lastLocation = cpl;
                        cpl = int.Parse(a[1]);
                        possibleRouts[counter, i] = cpl.ToString();
                    }
                    else
                    {
                        lastLocation = cpl;
                        cpl = int.Parse(a[0]);
                        possibleRouts[counter, i] = cpl.ToString();
                    }
                    
                }
                if (a.Length > 2)
                {
                    routesNumber = a.Length - 1;
                    if (a[j] == lastLocation.ToString())
                    {
                        SkipCell = true;
                        lastLocation = cpl;
                        cpl = int.Parse(a[j + 1]);
                        possibleRouts[counter, i] = cpl.ToString();
                    }
                    else
                    {
                        if (SkipCell == true)
                        {
                            lastLocation = cpl;
                            cpl = int.Parse(a[j + 1]);
                            possibleRouts[counter, i] = cpl.ToString();
                        }
                        else
                        {
                            lastLocation = cpl;
                            cpl = int.Parse(a[j]);
                            possibleRouts[counter, i] = cpl.ToString();
                        }
                    }
                }
            }
            cpl = NextLocation;
            lastLocation =_last ;
            if(j+1 < routesNumber)
                counter++;
        }
        for(int u=0; u< constant; u++)
        {
            for(int y=0; y< constant; y++)
            {
                if(possibleRouts[u,y] == "")
                {
                    if (y == 0)
                        y = constant;
                    else
                    {
                        ActiveArrow(possibleRouts[u, y - 1]);
                    }
                }
                else
                {
                    if (y == constant-1)
                    {
                        ActiveArrow(possibleRouts[u, y]);
                    }
                }

            }
        }
        for (int u = 0; u < constant; u++)
        {
            for (int y = 0; y < constant; y++)
            {
                Debug.Log("row: " + u + " coulmn: " + y + " value: " + possibleRouts[u, y]);

            }
        }
    }

    private void ActiveArrow(string square)
    {
        foreach (Transform x in ArrowsArray.transform)
        {
            if (x.gameObject.name == "Button" + square)
            {
                x.gameObject.SetActive(true);
            }
        }
    }

    private void EnemiesMovement()
    {
        doubles = new int[4];
        for (int x = 0; x < doubles.Length; x++)
            doubles[x] = 0;
        enemies = 0;
        for (int i = 1; i < 55; i++)
        {
            bool isEnemy = false;
            bool isPlayer = false;
            GameObject block = pickSquare(i);
            foreach (Transform Square in block.transform)
            {
                if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
                {
                    bool moved = false;
                    for (int x = 0; x < doubles.Length; x++)
                    {
                        if (doubles[x] == i)
                            moved = true;
                    }
                    if (moved == false)
                    {
                        isEnemy = true;
                        Debug.Log("enemy: " + i);
                    }
                }
                if (Square.gameObject.name == "player" && Square.gameObject.activeSelf == true)
                {
                    isPlayer = true;
                }
            }
            if (isEnemy == true && isPlayer == false)
            {
                bool change = true;
                int num = (int)(Random.Range(0f, 61f));
                while (change == true)
                {
                    if ((num >= 0 && num < 15 && otherNum >= 0 && otherNum < 15) || (num >= 15 && num < 30 && otherNum >= 15 && otherNum < 30) ||
                        (num >= 30 && num < 45 && otherNum >= 30 && otherNum < 45) || (num >= 45 && num < 60 && otherNum >= 45 && otherNum < 60))
                    {
                        num = (int)(Random.Range(0f, 61f));
                        change = true;
                    }
                    else
                    {
                        change = false;
                    }
                }
                foreach (Transform Square in block.transform)
                {
                    if (Square.gameObject.name == "Enemy")
                    {
                        Square.gameObject.SetActive(false);
                    }
                }
                string[] w = StringSquareArray[i].Split(',');
                if (w.Length == 1)
                {
                    if (isEnemyNotThere(int.Parse(w[0])))
                    {
                        block = pickSquare(int.Parse(w[0]));
                        doubles[enemies] = int.Parse(w[0]);
                        enemies++;
                    }
                    else
                    {
                        block = pickSquare(i);
                        doubles[enemies] = i;
                        enemies++;
                    }
                }
                if (w.Length == 2)
                {
                    if (num < 30 && isEnemyNotThere(int.Parse(w[0])))
                    {
                        block = pickSquare(int.Parse(w[0]));
                        doubles[enemies] = int.Parse(w[0]);
                        enemies++;
                    }
                    else if (isEnemyNotThere(int.Parse(w[1])))
                    {
                        doubles[enemies] = int.Parse(w[1]);
                        enemies++;
                        block = pickSquare(int.Parse(w[1]));
                    }
                    else
                    {
                        if (isEnemyNotThere(int.Parse(w[0])))
                        {
                            block = pickSquare(int.Parse(w[0]));
                            doubles[enemies] = int.Parse(w[0]);
                            enemies++;
                        }
                        else
                        {
                            block = pickSquare(i);
                            doubles[enemies] = i;
                            enemies++;
                        }
                    }
                }
                if (w.Length == 3)
                {
                    if (num < 20 && isEnemyNotThere(int.Parse(w[0])))
                    {
                        block = pickSquare(int.Parse(w[0]));
                        doubles[enemies] = int.Parse(w[0]);
                        enemies++;
                    }
                    else if (num < 40 && num >= 20 && isEnemyNotThere(int.Parse(w[1])))
                    {
                        block = pickSquare(int.Parse(w[1]));
                        doubles[enemies] = int.Parse(w[1]);
                        enemies++;
                    }
                    else if (isEnemyNotThere(int.Parse(w[2])))
                    {
                        block = pickSquare(int.Parse(w[2]));
                        doubles[enemies] = int.Parse(w[2]);
                        enemies++;
                    }
                    else
                    {
                        if (isEnemyNotThere(int.Parse(w[0])))
                        {
                            block = pickSquare(int.Parse(w[0]));
                            doubles[enemies] = int.Parse(w[0]);
                            enemies++;
                        }
                        else if (isEnemyNotThere(int.Parse(w[1])))
                        {
                            block = pickSquare(int.Parse(w[1]));
                            doubles[enemies] = int.Parse(w[1]);
                            enemies++;
                        }
                        else
                        {
                            block = pickSquare(i);
                            doubles[enemies] = i;
                            enemies++;
                        }
                    }
                }
                if (w.Length == 4)
                {
                    if (num < 15 && isEnemyNotThere(int.Parse(w[0])))
                    {
                        block = pickSquare(int.Parse(w[0]));
                        doubles[enemies] = int.Parse(w[0]);
                        enemies++;
                    }
                    else if (num < 30 && num >= 15 && isEnemyNotThere(int.Parse(w[1])))
                    {
                        block = pickSquare(int.Parse(w[1]));
                        doubles[enemies] = int.Parse(w[1]);
                        enemies++;
                    }
                    else if (num < 45 && num >= 30 && isEnemyNotThere(int.Parse(w[2])))
                    {
                        block = pickSquare(int.Parse(w[2]));
                        doubles[enemies] = int.Parse(w[2]);
                        enemies++;
                    }
                    else if (isEnemyNotThere(int.Parse(w[3])))
                    {
                        block = pickSquare(int.Parse(w[3]));
                        doubles[enemies] = int.Parse(w[3]);
                        enemies++;
                    }
                    else
                    {
                        if (isEnemyNotThere(int.Parse(w[0])))
                        {
                            block = pickSquare(int.Parse(w[0]));
                            doubles[enemies] = int.Parse(w[0]);
                            enemies++;
                        }
                        else if (isEnemyNotThere(int.Parse(w[1])))
                        {
                            block = pickSquare(int.Parse(w[1]));
                            doubles[enemies] = int.Parse(w[1]);
                            enemies++;
                        }
                        else if (isEnemyNotThere(int.Parse(w[2])))
                        {
                            block = pickSquare(int.Parse(w[2]));
                            doubles[enemies] = int.Parse(w[2]);
                            enemies++;
                        }
                    }
                }
                foreach (Transform Square in block.transform)
                {
                    if (Square.gameObject.name == "Enemy")
                    {
                        Square.gameObject.SetActive(true);
                    }
                }
            }
        }
        Debug.Log("current loaction: " + CurrentPlayerLocation);
    }

    private bool isEnemyNotThere(int des)
    {
        bool value = true;
        GameObject b = pickSquare(des);
        foreach (Transform Square in b.transform)
        {
            if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
                value = false;
            if (Square.gameObject.name == "TreasureBox" && Square.gameObject.activeSelf == true)
                value = false;
            if (Square.gameObject.name == "SurpriseBag" && Square.gameObject.activeSelf == true)
                value = false;
        }
        if (des == 55)
            value = false;
        return value;
    }

    public void countinueButton()
    {
        situationss.AfterContinueButton();
    }

    private void rememberLocations()
    {
        winOrLose.minus1 = minus1Cube.gameObject.activeSelf;
        winOrLose.plus1 = plus1Cube.gameObject.activeSelf;
        winOrLose.throwAgain = ThrowAgain.gameObject.activeSelf;

        int trbox = 0;
        int srpbag = 0;
        int enemies = 0;
        winOrLose.setEmptyCells();
        for (int i = 0; i < StringSquareArray.Length; i++)
        {
            GameObject b = pickSquare(i);
            foreach (Transform Square in b.transform)
            {
                if(Square.gameObject.name == "TreasureBox" && Square.gameObject.activeSelf == true)
                {
                    winOrLose.treasureBoxesArray[trbox] = i;
                    trbox++;
                }
                if (Square.gameObject.name == "SurpriseBag" && Square.gameObject.activeSelf == true)
                {
                    winOrLose.surpriseBagsArray[srpbag] = i;
                    srpbag++;
                }
                if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
                {
                    winOrLose.enemiesArray[enemies] = i;
                    enemies++;
                }
                if (Square.gameObject.name == "player" && Square.gameObject.activeSelf == true)
                    winOrLose.playerLocation = i;
            }
        }
    }

    //private void levelUpOrAccomplishedMission()
    //{
    //    if(PlayerData.experience >= PlayerData.expGoal)
    //    {
    //        PlayerData.LevelUp();
    //        situationss.levelUp();
    //        situationss.setBarAndData();
    //    }
    //    bool isMissiomAccomplished = true;
    //    for (int i = 0; i < StringSquareArray.Length; i++)
    //    {
    //        GameObject b = pickSquare(i);
    //        foreach (Transform Square in b.transform)
    //        {
    //            if (Square.gameObject.name == "Enemy" && Square.gameObject.activeSelf == true)
    //                isMissiomAccomplished = false;
    //        }
    //    }
    //    if(isMissiomAccomplished == true)
    //    {
    //        situationss.missionAccomplished();
    //        situationss.levelUp();
    //    }
    //
    //}

    public void ReturnTower()
    {
        situationss.ReturnToTheTower();
    }

    public void MissionDidntAccomplished(int num)
    {
        if (num == 1)
            situationss.MissionDidntAccomplished(true);
        else
            situationss.MissionDidntAccomplished(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CanvasButtons.gameObject.SetActive(false);
            TableWindow.gameObject.SetActive(true);
            tableCells.gameObject.SetActive(true);

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
        TableWindow.gameObject.SetActive(false);
        tableCells.gameObject.SetActive(false);
    }
    public void Restart()
    { Login.Restartt(); }
    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
