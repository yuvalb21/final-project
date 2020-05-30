using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks
{
    public GameObject player;
    public GameObject BlocksArray;
    public GameObject enemy;

    private float Yunits = 14f;
    private float Xunits = 28f;
    //private static int[,] FloorArr = new int[3, 4];
    public int CrntRow;
    public int CrntClmns;
    public int EnemyCrntRow;
    public int EnemyCrntClmns;
    private bool isPlayerHited = false;
    private bool isEnemyHited = false;
    //private static int[,] EnemyFloorArr = new int[3, 4];

    private object SquareFloor;

    private bool direction;

    public Attacks(GameObject _player, GameObject bArray, GameObject _enemy)
    {
        player = _player;
        BlocksArray = bArray;
        enemy = _enemy;
        start();
    }

    private void start()
    {
        CrntRow = 1;
        CrntClmns = 3;
        EnemyCrntRow = 1;
        EnemyCrntClmns = 0;

        //its mabey unneccecery
        //for (int i = 0; i < 3; i++)
        //{
        //    for (int j = 0; j < 4; j++)
        //    {
        //        if (i == CrntRow && j == CrntClmns)
        //        {
        //            FloorArr[i, j] = 1;
        //        }
        //        else if(i == EnemyCrntRow && j == EnemyCrntClmns)
        //        {
        //            EnemyFloorArr[i, j] = 2;
        //        }
        //        else
        //        {
        //            FloorArr[i, j] = 0;
        //        }
        //    }
        //}
        direction = true;
    }

    public bool isDirection()
    {
        if(direction == true)
        {
            if (EnemyCrntClmns > CrntClmns)
            {
                direction = false;
                return true;
            }
            else
                return false;
        }
        else
        {
            if (CrntClmns > EnemyCrntClmns)
            {
                direction = true;
                return true;
            }
            else
                return false;
        }

    }
    private void PlayerSquareMove(int a)
    {
        //FloorArr[CrntRow, CrntClmns] = 0;
        if (a == 1)
        {
            CrntRow = CrntRow - 1;
        }
        if (a == 2)
        {
            CrntRow = CrntRow + 1;
        }
        if (a == 3)
        {
            CrntClmns = CrntClmns + 1;
        }
        if (a == 4)
        {
            CrntClmns = CrntClmns - 1;
        }
        //FloorArr[CrntRow, CrntClmns] = 1;
    }

    public int getCrntRow()
    {
        return CrntRow;
    }
    public int getCrntClmns()
    {
        return CrntClmns;
    }
    public int getEnemyCrntRow()
    {
        return EnemyCrntRow;
    }
    public int getEnemyCrntClmns()
    {
        return EnemyCrntClmns;
    }

    public void up()
    {
        if (CrntRow != 0)
        {
            player.transform.Translate(0, Yunits, 0);
            Debug.Log("row: " + CrntRow + " clmns: " + CrntClmns);
            PlayerSquareMove(1);
            Debug.Log("up");
        }
        else
        {
            Debug.Log("cant move up " + CrntRow);
        }
        isEnemyHited = false;
    }

    public void down()
    {
        if (CrntRow != 2)
        {
            player.transform.Translate(0, -Yunits, 0);
            Debug.Log("row: " + CrntRow + " clmns: " + CrntClmns);
            PlayerSquareMove(2);
            Debug.Log("down");
        }
        else
        {
            Debug.Log("cant move down");
        }
        isEnemyHited = false;
    }

    public void right()
    {
        if (CrntClmns != 3)
        {
            player.transform.Translate(Xunits, 0, 0);
            Debug.Log("row: " + CrntRow + " clmns: " + CrntClmns);
            PlayerSquareMove(3);
            Debug.Log("right");
        }
        else
        {
            Debug.Log("cant move right");
        }
        isEnemyHited = false;
    }

    public void left()
    {
        if (CrntClmns != 0)
        {
            player.transform.Translate(-Xunits, 0, 0);
            Debug.Log("row: " + CrntRow + " clmns: " + CrntClmns);
            PlayerSquareMove(4);
            Debug.Log("left");
        }
        else
        {
            Debug.Log("cant move left");
        }
        isEnemyHited = false;
    }

    public void IceBallAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses iceball attack 
        if (direction)
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 2).ToString();
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 2).ToString();
            ListOfBlocks(posisions, true);
        }
    }

    public void SnowFlakesAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses SnowFlakes attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }

    public void FogAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses Fog attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 2).ToString();
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 2).ToString();
            ListOfBlocks(posisions, true);
        }
    }



    public void WaterFallAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses WaterFall attack 
        if (direction)
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 1).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 1).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }

    public void IciclesAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses icicles attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 2).ToString();
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 2).ToString();
            ListOfBlocks(posisions, true);
        }
    }

    public void WhirlpoolAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses Fog attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }

    public void IceFieldAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses icicles attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }



    public void HailAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses Fog attack 
        if (direction)
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + (CrntRow - 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 2).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }

    public void AvalancheAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses icicles attack 
        if (direction)
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 2).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + CrntRow.ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 2).ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 2).ToString() + ",";
            ListOfBlocks(posisions, true);
        }
    }

    public void TsunamiAttack()
    {
        //in this function will be the damage and the stamina of the player when he uses icicles attack 
        if (direction)
        {
            string posisions = "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns - 2).ToString();
            ListOfBlocks(posisions, true);
        }
        else
        {
            string posisions = "Floor" + (CrntRow + 1).ToString() + CrntClmns.ToString() + ",";
            posisions += "Floor" + CrntRow.ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow + 1).ToString() + (CrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (CrntRow - 1).ToString() + (CrntClmns + 2).ToString();
            ListOfBlocks(posisions, true);
        }
    }


    public void MagicRenewal()
    {
        isEnemyHited = false;
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            if (SquareFloor.gameObject.name == "Floor" + CrntRow.ToString() + CrntClmns.ToString())
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(240, 240, 0);
        }
    }

    public void Heal()
    {
        isEnemyHited = false;
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            if (SquareFloor.gameObject.name == "Floor" + CrntRow.ToString() + CrntClmns.ToString())
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 8235294f, 0.09803922f , 1f);
        }
    }

    public void Shield()
    {
        isEnemyHited = false;
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            if (SquareFloor.gameObject.name == "Floor" + CrntRow.ToString() + CrntClmns.ToString())
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0.7249699f, 1f);
        }
    }


    private void ListOfBlocks(string posisions, bool playe)
    {
        string[] a = posisions.Split(',');
        Debug.Log("rowEnemy: " + EnemyCrntRow + " clmnsEnemy: " + EnemyCrntClmns);
        Debug.Log("row: " + CrntRow + " clmns: " + CrntClmns);
        isEnemyHited = false;
        isPlayerHited = false;
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (SquareFloor.gameObject.name == a[i])
                {
                    if (playe == true)//player attack
                    {
                        SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 40);
                        if (a[i] == "Floor" + EnemyCrntRow.ToString() + EnemyCrntClmns.ToString())
                        {
                            Debug.Log(a[i] + " enemy hited");
                            isEnemyHited = true;
                            Debug.Log("enemyyyyyyyy " + EnemyCrntRow + " lllllll " + EnemyCrntClmns);
                        }
                    }
                    else//enemy attack
                    {
                        SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5490196f, 0.2745098f, 0.07843138f, 1f);
                        if (a[i] == "Floor" + CrntRow.ToString() + CrntClmns.ToString())
                        {
                            Debug.Log(a[i] +" player hited");
                            isPlayerHited = true;
                        }
                    }   
                }
            }
        }
    }

    public bool getIsEnemyHited()
    {
        return isEnemyHited;
    }

    public bool getIsPlayerHited()
    {
        return isPlayerHited;
    }

    public void upEnemy()
    {
        if (EnemyCrntRow != 0)
        {
            enemy.transform.Translate(0, Yunits, 0);
            Debug.Log("rowEnemy: "+EnemyCrntRow+" clmnsEnemy: "+EnemyCrntClmns);
            EnemySquareMove(1);
            Debug.Log("up");
        }
        else
        {
            Debug.Log("cant move up Enemy");
        }
        //isPlayerHited = false;
    }

    public void downEnemy()
    {
        if (EnemyCrntRow != 2)
        {
            enemy.transform.Translate(0, -Yunits, 0);
            Debug.Log("rowEnemy: " + EnemyCrntRow + " clmnsEnemy: " + EnemyCrntClmns);
            EnemySquareMove(2);
            Debug.Log("down");
        }
        else
        {
            Debug.Log("cant move down Enemy");
        }
        //isPlayerHited = false;
    }

    public void rightEnemy()
    {
        if (EnemyCrntClmns != 3)
        {
            enemy.transform.Translate(Xunits, 0, 0);
            Debug.Log("rowEnemy: " + EnemyCrntRow + " clmnsEnemy: " + EnemyCrntClmns);
            EnemySquareMove(3);
            Debug.Log("right");
        }
        else
        {
            Debug.Log("cant move right Enemy");
        }
        //isPlayerHited = false;
    }

    public void leftEnemy()
    {
        if (EnemyCrntClmns != 0)
        {
            enemy.transform.Translate(-Xunits, 0, 0);
            Debug.Log("rowEnemy: " + EnemyCrntRow + " clmnsEnemy: " + EnemyCrntClmns);
            EnemySquareMove(4);
            Debug.Log("left");
        }
        else
        {
            Debug.Log("cant move left Enemy");
        }
        //isPlayerHited = false;
    }

    private void EnemySquareMove(int a)
    {
        //FloorArr[EnemyCrntRow, EnemyCrntClmns] = 0;
        if (a == 1)
        {
            EnemyCrntRow = EnemyCrntRow - 1;
        }
        if (a == 2)
        {
            EnemyCrntRow = EnemyCrntRow + 1;
        }
        if (a == 3)
        {
            EnemyCrntClmns = EnemyCrntClmns + 1;
        }
        if (a == 4)
        {
            EnemyCrntClmns = EnemyCrntClmns - 1;
        }
        isPlayerHited = false;
        Debug.Log("rowEnemy: " + EnemyCrntRow + " clmnsEnemy: " + EnemyCrntClmns);
        //FloorArr[EnemyCrntRow, EnemyCrntClmns] = 2;
    }

    public void MagicRenewalEnemy()
    {
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            if (SquareFloor.gameObject.name == "Floor" + EnemyCrntRow.ToString() + EnemyCrntClmns.ToString())
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(240, 240, 0);
        }
        Debug.Log("MagicRenewalEnemy");
        isPlayerHited = false;
    }

    public void RockPunchAttackEnemy()
    {
        if (direction)
        {
            string posisions = "Floor" + (EnemyCrntRow - 1).ToString() + (EnemyCrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow - 1).ToString() + (EnemyCrntClmns + 2).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns + 1).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns + 2).ToString() + ",";
            Debug.Log("RockPunchAttack");
            ListOfBlocks(posisions, false);
        }
        else
        {
            string posisions = "Floor" + (EnemyCrntRow - 1).ToString() + (EnemyCrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow - 1).ToString() + (EnemyCrntClmns - 2).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns - 1).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns - 2).ToString() + ",";
            Debug.Log("RockPunchAttack");
            ListOfBlocks(posisions, false);
        }
    }

    public void RollingStoneAttackEnemy()
    {
        if (direction)
        {
            string posisions = "Floor" + EnemyCrntRow.ToString() + (EnemyCrntClmns + 1).ToString() + ",";
            posisions += "Floor" + EnemyCrntRow.ToString() + (EnemyCrntClmns + 2).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns + 2).ToString() + ",";
            Debug.Log("RollingStoneAttack");
            ListOfBlocks(posisions, false);
        }
        else
        {
            string posisions = "Floor" + EnemyCrntRow.ToString() + (EnemyCrntClmns - 1).ToString() + ",";
            posisions += "Floor" + EnemyCrntRow.ToString() + (EnemyCrntClmns - 2).ToString() + ",";
            posisions += "Floor" + (EnemyCrntRow + 1).ToString() + (EnemyCrntClmns - 2).ToString() + ",";
            Debug.Log("RollingStoneAttack");
            ListOfBlocks(posisions, false);
        }
    }

    public void ShieldEnemy()
    {
        isPlayerHited = false;
        foreach (Transform SquareFloor in BlocksArray.transform)
        {
            if (SquareFloor.gameObject.name == "Floor" + EnemyCrntRow.ToString() + EnemyCrntClmns.ToString())
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0.7249699f, 1f);
        }
    }
}