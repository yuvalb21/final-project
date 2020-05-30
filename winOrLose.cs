using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class winOrLose
{
    public static bool win;
    public static bool isBoss;
    public static bool isFirstDuelOver = false;

    public static int[] enemiesArray = new int[5];
    public static int[] treasureBoxesArray = new int[5];
    public static int[] surpriseBagsArray = new int[3];
    public static int playerLocation;

    public static bool plus1;
    public static bool minus1;
    public static bool throwAgain;
    public static void start()
    {
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            enemiesArray[i] = 0;
            treasureBoxesArray[i] = 0;
        }
        for (int i = 0; i < surpriseBagsArray.Length; i++)
        {
            surpriseBagsArray[i] = 0;
        }
        plus1 = true;
        minus1 = true;
        throwAgain = true;
    }

    public static void setEmptyCells()
    {
        for (int i = 0; i < enemiesArray.Length; i++)
        {
            enemiesArray[i] = 0;
            treasureBoxesArray[i] = 0;
        }
        for (int i = 0; i < surpriseBagsArray.Length; i++)
        {
            surpriseBagsArray[i] = 0;
        }
    }
}