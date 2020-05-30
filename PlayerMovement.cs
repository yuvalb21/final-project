using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public GameObject ButtonsCanvas;
    public GameObject BlocksArray;
    public GameObject background;
    public GameObject enemy;
    public GameObject courtPositionsArray;
    public GameObject window;
    public GameObject result;

    private string[] MoveOrderArray = new string[3];
    private int PlayNum = 0;
    private float[] StaminaArray = new float[3];
    private float[] DamageArray = new float[3];
    private float ifHeal = 0;

    private bool isFull = false;
    private bool stop = false;

    public StaminaBar staiminabarPlayer;
    public StaminaBar staiminabarEnemy;

    public StaminaBar hpbarPlayer;
    public StaminaBar hpbarEnemy;

    private Attacks attack;
    private screenManager screenManager;
    private PlayerClass playerManager;
    private enemyManager enemyManager;
    void Start()
    {
        attack = new Attacks(player, BlocksArray, enemy);
        screenManager = new screenManager(player, BlocksArray, ButtonsCanvas, background, enemy, courtPositionsArray);
        playerManager = new PlayerClass(PlayerData.Level, PlayerData.Hp, PlayerData.Stamina);
        enemyManager = new enemyManager(PlayerData.Hp, PlayerData.Stamina, winOrLose.isBoss);//fix enemy hp

        for (int i = 0; i < MoveOrderArray.Length; i++)
            MoveOrderArray[i] = "";

        for (int i = 0; i < StaminaArray.Length; i++)
            StaminaArray[i] = 0f;

        for (int i = 0; i < DamageArray.Length; i++)
            DamageArray[i] = 0f;

        staiminabarPlayer.resetStats(PlayerData.Stamina);
        hpbarPlayer.resetStats(PlayerData.Hp);

        if (winOrLose.isBoss)
        {
            staiminabarEnemy.resetStats(PlayerData.Stamina + 15f);
            hpbarEnemy.resetStats(PlayerData.Hp + 15f);
        }

        else
        {
            staiminabarEnemy.resetStats(PlayerData.Stamina);
            hpbarEnemy.resetStats(PlayerData.Hp);
        }
    }

    IEnumerator Continue()
    {
        screenManager.isScreen();
        yield return new WaitForSeconds(2);
        setActiveCards();

        enemyManager.pickAttacks(attack.getCrntRow(), attack.getCrntClmns(), attack.getEnemyCrntRow(), attack.getEnemyCrntClmns());

        for(int i=0; i<MoveOrderArray.Length; i++)
        {
            if (PlayerBeforeEnemy(i) == true)
            {
                WhichMove(MoveOrderArray[i]);
                direction();
                isenemyHited(i);
                staiminabarPlayer.TakeStamina(StaminaArray[i], PlayerData.Stamina);
                yield return new WaitForSeconds(2);
                DeleteImageButton(i.ToString());

                if(MoveOrderArray[i] != "Shield")
                {
                    foreach (Transform SquareFloor in BlocksArray.transform)
                        SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                    yield return new WaitForSeconds(1);
                }

                if (enemyManager.getTotalHpEnemy() <= 0)
                {
                    Debug.Log("you won");
                    i = 2;
                    stop = true;
                    Destroy(enemy);
                    winOrLoseWindow(true);
                }

                if (stop == false)
                {
                    WhichMove(enemyManager.getCurrentMove(i));
                    direction();
                    isplayerHited(i);
                    staiminabarEnemy.TakeStamina(enemyManager.getCurrentstaminaMove(i), enemyManager.getStaminaEnemy());
                    yield return new WaitForSeconds(2);
                }
            }
            else
            {
                WhichMove(enemyManager.getCurrentMove(i));
                direction();
                isplayerHited(i);
                staiminabarEnemy.TakeStamina(enemyManager.getCurrentstaminaMove(i), enemyManager.getStaminaEnemy());
                yield return new WaitForSeconds(2);

                if(enemyManager.getCurrentMove(i) != "ShieldEnemy")
                {
                    foreach (Transform SquareFloor in BlocksArray.transform)
                        SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
                    yield return new WaitForSeconds(1);

                }
                if (playerManager.getTotalHpPlayer() <= 0)
                {
                    Debug.Log("you lost");
                    i = 2;
                    stop = true;
                    Destroy(player);
                    winOrLoseWindow(false);
                }
                if (stop == false)
                {
                    WhichMove(MoveOrderArray[i]);
                    //hpbarPlayer.TakeStamina(-ifHeal, playerManager.getHpPlayer());
                    //ifHeal = 0;
                    direction();
                    isenemyHited(i);
                    staiminabarPlayer.TakeStamina(StaminaArray[i], PlayerData.Stamina);
                    yield return new WaitForSeconds(2);
                    DeleteImageButton(i.ToString());
                }
            }
            if (playerManager.getTotalHpPlayer() <= 0)
            {
                Debug.Log("you lost");
                i = 2;
                stop = true;
                Destroy(player);
                winOrLoseWindow(false);
            }
            if (enemyManager.getTotalHpEnemy() <= 0)
            {
                Debug.Log("you won");
                i = 2;
                stop = true;
                Destroy(enemy);
                winOrLoseWindow(true);
            }

            foreach (Transform SquareFloor in BlocksArray.transform)
                SquareFloor.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);
            yield return new WaitForSeconds(1);
        }

        if(stop == false)
        {
            PlayNum = 0;
            isFull = false;

            for (int i = 0; i < MoveOrderArray.Length; i++)
                MoveOrderArray[i] = "";

            if (playerManager.getTotalStaminaPlayer() + playerManager.getStaminaBonus() > 100)
            {
                playerManager.setTotalStamina(100);
            }

            else
            {
                playerManager.setTotalStamina(playerManager.getTotalStaminaPlayer() + playerManager.getStaminaBonus());
            }

            staiminabarPlayer.TakeStamina(-playerManager.getStaminaBonus(), PlayerData.Stamina);
            staiminabarPlayer.resetStats(playerManager.getTotalStaminaPlayer());

            staiminabarEnemy.TakeStamina(-enemyManager.getStaminaBonus(), enemyManager.getStaminaEnemy());
            staiminabarEnemy.resetStats(enemyManager.getTotalStaminaEnemy());




            for (int i = 0; i < StaminaArray.Length; i++)
            {
                //Debug.Log("stamina array " + i + " " + StaminaArray[i]);
                StaminaArray[i] = 0f;
            }

            MoreStamina();

            screenManager.isContinueB();
            enemyManager.ResetEnemy();
            CourtPositions();
            Debug.Log("choose moves again");
        }
    }

    public void ReSelection()
    {
        Debug.Log("zzzzzzzzzzzzzzzzz "+playerManager.getTotalStaminaPlayer());

        PlayNum = 0;

        setActiveCards();

        DeleteImageButton("0");
        DeleteImageButton("1");
        DeleteImageButton("2");

        for(int i=0; i<StaminaArray.Length; i++)
        {
            playerManager.setTotalStamina(playerManager.getTotalStaminaPlayer() + StaminaArray[i]);
            Debug.Log("fffffffff " + playerManager.getTotalStaminaPlayer());
        }

        MoreStamina();

        for (int i = 0; i < playerManager.spellsArray.Length; i++)
        {
            DontHaveEnoughStamina(playerManager.spellsArray[i]);
        }


        for (int i = 0; i < StaminaArray.Length; i++)
        {
            StaminaArray[i] = 0f;
        }

        for (int i = 0; i < DamageArray.Length; i++)
        {
            DamageArray[i] = 0f;
        }

        isFull = false;
    }

    public void CellMove(string MoveCard)
    {
        if (isFull == false)
        {
            Debug.Log("playnum: " + PlayNum);
            foreach (Transform ChosenCard in ButtonsCanvas.transform.GetChild(0))
            {
                if (ChosenCard.gameObject.name == MoveCard)
                {
                    ChosenCard.gameObject.GetComponent<Button>().gameObject.SetActive(false);
                }
            }

            bool isAttack = false;
            for (int i = 0; i < playerManager.spellsArray.Length; i++)
            {
                if (playerManager.spellsArray[i].getNameSpell() == MoveCard)
                {
                    isAttack = true; // this is an attack
                    playerManager.setTotalStamina(playerManager.getTotalStaminaPlayer() - playerManager.spellsArray[i].getStaminaSpell());

                    Debug.Log("kkkkkkkkkkkkkkkk " + playerManager.getTotalStaminaPlayer() + " " + playerManager.spellsArray[i].getStaminaSpell() + " " + MoveCard);

                    StaminaArray[PlayNum] = playerManager.spellsArray[i].getStaminaSpell();
                    DamageArray[PlayNum] = playerManager.spellsArray[i].getDamage();

                }
            }

            if (isAttack == false)
            {
                StaminaArray[PlayNum] = 0;
                DamageArray[PlayNum] = 0;
            }
            //bool s = false;

            if (StaminaArray[PlayNum] < 0) //if the card is MagicRenewal
            {
                if (playerManager.getTotalStaminaPlayer() - StaminaArray[PlayNum] > playerManager.getStaminaPlayer())
                {
                    playerManager.setTotalStamina(playerManager.getStaminaPlayer());
                }
                MoreStamina();
            }

            for (int i = 0; i < playerManager.spellsArray.Length; i++)
            {
                DontHaveEnoughStamina(playerManager.spellsArray[i]);
            }

            MoveOrderArray[PlayNum] = MoveCard;
            ListOfMoves();
        }
        else
            Debug.Log("for change cards you need to reset your current cards");
    }

    public void IsContinue()
    {
        bool a=true;
        foreach (Transform ChosenButton in ButtonsCanvas.transform)
        {
            if (ChosenButton.gameObject.name == "Disappear")
                a = ChosenButton.gameObject.activeSelf;
        }
        bool isEmpty = false;
        for(int i=0; i<MoveOrderArray.Length; i++)
        {
            if (MoveOrderArray[i] == "")
                isEmpty = true;
        }
        if (isEmpty == false)
            StartCoroutine(Continue());
        else
        {
            if (!a)
            {
                screenManager.isScreen();
                for (int i = 0; i < playerManager.spellsArray.Length; i++)
                {
                    //Debug.Log("dont have enough stamina" + i);
                    DontHaveEnoughStamina(playerManager.spellsArray[i]);
                }
            }
            else
                Debug.Log("you need to choose moves for all of the three");
        }
    }

    private void WhichMove(string MoveCard)
    {
        //movement
        if (MoveCard == "up")
            attack.up();
        if (MoveCard == "down")
            attack.down();
        if (MoveCard == "right")
            attack.right();
        if (MoveCard == "left")
            attack.left();

        //3 squares
        if (MoveCard == "IceBallAttack")
            attack.IceBallAttack();
        if (MoveCard == "SnowFlakesAttack")
            attack.SnowFlakesAttack();
        if (MoveCard == "FogAttack")
            attack.FogAttack();

        //4 squares
        if (MoveCard == "IciclesAttack")
            attack.IciclesAttack();
        if (MoveCard == "WaterFallAttack")
            attack.WaterFallAttack();
        if (MoveCard == "IceFieldAttack")
            attack.IceFieldAttack();
        if (MoveCard == "WhirlpoolAttack")
            attack.WhirlpoolAttack();

        //5 squares
        if (MoveCard == "HailAttack")
            attack.HailAttack();
        if (MoveCard == "AvalancheAttack")
            attack.AvalancheAttack();
        if (MoveCard == "TsunamiAttack")
            attack.TsunamiAttack();

        //others
        if (MoveCard == "MagicRenewal")
            attack.MagicRenewal();
        if (MoveCard == "Heal")
            attack.Heal();
        if (MoveCard == "Shield")
            attack.Shield();

        //enemy
        if (MoveCard == "upEnemy")
            attack.upEnemy();
        if (MoveCard == "downEnemy")
            attack.downEnemy();
        if (MoveCard == "rightEnemy")
            attack.rightEnemy();
        if (MoveCard == "leftEnemy")
            attack.leftEnemy();
        if (MoveCard == "MagicRenewalEnemy")
            attack.MagicRenewalEnemy();
        if (MoveCard == "RockPunchAttack")
            attack.RockPunchAttackEnemy();
        if (MoveCard == "RollingStoneAttack")
            attack.RollingStoneAttackEnemy();
        if (MoveCard == "ShieldEnemy")
            attack.ShieldEnemy();

    }

    private void ListOfMoves()
    {
        foreach (Transform ChosenButton in ButtonsCanvas.transform)
        {
            if (ChosenButton.gameObject.name == "Move" + PlayNum.ToString())
            {
                foreach (Transform ChosenCard in ButtonsCanvas.transform.GetChild(0))
                {
                    if (ChosenCard.gameObject.name == MoveOrderArray[PlayNum])
                    {
                        ChosenButton.gameObject.GetComponent<Image>().sprite = ChosenCard.gameObject.GetComponent<Image>().sprite;
                    }
                }
            }
        }
        PlayNum++;
        if (PlayNum > 2)
        {
            isFull = true;
        }
    }

    private void DeleteImageButton(string ButtonNum)
    {
        foreach (Transform ChosenCard in ButtonsCanvas.transform)
        {
            if (ChosenCard.gameObject.name == "Move" + ButtonNum)
            {
                ChosenCard.gameObject.GetComponent<Image>().sprite = null;
            }
        }
    }

    private void DontHaveEnoughStamina(SpellClass spell)
    {
        foreach (Transform ChosenCard in ButtonsCanvas.transform.GetChild(0))
        {
            if (ChosenCard.gameObject.name == spell.getNameSpell() && spell.getNameSpell() != "MagicRenewal")
            {
                float sum = playerManager.getTotalStaminaPlayer() - spell.getStaminaSpell();
                if (sum < 0)
                { 
                    ChosenCard.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    private void MoreStamina()
    {
        for (int i = 0; i < playerManager.spellsArray.Length; i++)
        {
            foreach (Transform ChosenCard in ButtonsCanvas.transform.GetChild(0))
            {
                if (playerManager.spellsArray[i].getStaminaSpell() <= playerManager.getTotalStaminaPlayer())
                {
                    ChosenCard.gameObject.GetComponent<Button>().interactable = true;
                }
            }
        }
    }

    private void setActiveCards()
    {
        foreach (Transform ChosenButton in ButtonsCanvas.transform.GetChild(0))
        {

            if (ChosenButton.gameObject.name == MoveOrderArray[0])
            {
                //ChosenButton.gameObject.GetComponent<Button>().interactable = true;
                ChosenButton.gameObject.GetComponent<Button>().gameObject.SetActive(true);
            }
            if (ChosenButton.gameObject.name == MoveOrderArray[1])
            {
                //ChosenButton.gameObject.GetComponent<Button>().interactable = true;
                ChosenButton.gameObject.GetComponent<Button>().gameObject.SetActive(true);
            }
            if (ChosenButton.gameObject.name == MoveOrderArray[2])
            {
                //ChosenButton.gameObject.GetComponent<Button>().interactable = true;
                ChosenButton.gameObject.GetComponent<Button>().gameObject.SetActive(true);
            }
        }
    }

    private bool PlayerBeforeEnemy(int num)
    {
        bool isEnemyAttack = false;
        bool isPlayerAttack = false;

        if (enemyManager.getCurrentMove(num).Contains("Attack"))
            isEnemyAttack = true;

        if (MoveOrderArray[num].Contains("Attack"))
            isPlayerAttack = true;

        if (isEnemyAttack == false && isPlayerAttack == true)
            return false;
        else
            return true;
    }

    private void direction()
    {
        if(attack.isDirection() == true)
        {
            if(attack.getCrntClmns() > attack.getEnemyCrntClmns())
            {
                player.transform.position = new Vector3(player.transform.position.x + 13.84f, player.transform.position.y, 100f);
                enemy.transform.position = new Vector3(enemy.transform.position.x - 13.84f, enemy.transform.position.y, 100f);
            }
            if (attack.getCrntClmns() < attack.getEnemyCrntClmns())
            {
                player.transform.position = new Vector3(player.transform.position.x - 13.84f, player.transform.position.y, 100f);
                enemy.transform.position = new Vector3(enemy.transform.position.x + 13.84f, enemy.transform.position.y, 100f);
            }

            player.transform.localScale = new Vector3((player.transform.localScale.x*-1), player.transform.localScale.y, player.transform.localScale.z);
            enemy.transform.localScale = new Vector3((enemy.transform.localScale.x * -1), enemy.transform.localScale.y, enemy.transform.localScale.z);

            Debug.Log("player local scale: " + player.transform.position.x);
        }
    }

    private void isplayerHited(int num)
    {
        if(attack.getIsPlayerHited() && MoveOrderArray[num] != "Shield")
        {
            float y = enemyManager.getCurrentdamageMove(num);
            playerManager.setTotalHpPlayer(playerManager.getTotalHpPlayer() - y);
            if (playerManager.getTotalHpPlayer() < 0)
                y = y + enemyManager.getTotalHpEnemy();
            hpbarPlayer.TakeStamina(y, playerManager.getHpPlayer());
            Debug.Log("ooooooooooooooooo " + playerManager.getTotalHpPlayer());
        }
    }

    private void isenemyHited(int num)
    {
        if (attack.getIsEnemyHited() && enemyManager.getCurrentMove(num) != "ShieldEnemy")
        {
            float u = DamageArray[num];
            enemyManager.setTotalHpEnemy(enemyManager.getTotalHpEnemy() - DamageArray[num]);
            if (enemyManager.getTotalHpEnemy() < 0)
                u = DamageArray[num] + enemyManager.getTotalHpEnemy();
            hpbarEnemy.TakeStamina(u, enemyManager.getHpEnemy());
            Debug.Log("mmmmmmmmmmmmmmmmmm " + enemyManager.getTotalHpEnemy());
        }
    }

    private void CourtPositions()
    {
        foreach (Transform s in courtPositionsArray.transform)
        {
            if (s.gameObject.name == "empty")
            {
                foreach (Transform q in courtPositionsArray.transform)
                {
                    if (q.gameObject.name.Contains("position"))
                        q.gameObject.GetComponent<SpriteRenderer>().sprite = s.gameObject.GetComponent<SpriteRenderer>().sprite;
                }
            }
        }
        if ((attack.getEnemyCrntRow() == attack.getCrntRow()) && (attack.getEnemyCrntClmns() == attack.getCrntClmns()))
        {
            foreach (Transform e in courtPositionsArray.transform)
            {
                if (e.gameObject.name == "position" + attack.getCrntRow().ToString() + attack.getCrntClmns().ToString())
                {
                    foreach (Transform c in courtPositionsArray.transform)
                    {
                        if (c.gameObject.name == "sameBlock")
                        {
                            e.gameObject.GetComponent<SpriteRenderer>().sprite = c.gameObject.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
            }
        }
        else
        {
            foreach (Transform x in courtPositionsArray.transform)
            {
                if (x.gameObject.name == "position" + attack.getCrntRow().ToString() + attack.getCrntClmns().ToString())
                {
                    Debug.Log("good1 "+ x.gameObject.name);
                    foreach (Transform a in courtPositionsArray.transform)
                    {
                        if (a.gameObject.name == "playerLocation")
                        {
                            Debug.Log("good2");
                            x.gameObject.GetComponent<SpriteRenderer>().sprite = a.gameObject.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
        
                if (x.gameObject.name == "position" + attack.getEnemyCrntRow().ToString() + attack.getEnemyCrntClmns().ToString())
                {
                    foreach (Transform b in courtPositionsArray.transform)
                    {
                        if (b.gameObject.name == "enemyLocation")
                        {
                            x.gameObject.GetComponent<SpriteRenderer>().sprite = b.gameObject.GetComponent<SpriteRenderer>().sprite;
                        }
                    }
                }
            }
        }
    }

    private void winOrLoseWindow(bool won)
    {
        foreach (Transform button in ButtonsCanvas.transform)
        {
            if (button.gameObject.name == "ContinueButton")
                button.gameObject.SetActive(false);
        }
        window.gameObject.SetActive(true);
        if (won == true)
        {
            result.GetComponent<Text>().text = PlayerData.PlayerName+" Won";
            winOrLose.win = true;
        }
        else
        {
            result.GetComponent<Text>().text = PlayerData.PlayerName +" Lost";
            winOrLose.win = false;
        }
        winOrLose.isFirstDuelOver = true;

    }

    public void BackToMap()
    {
        SceneManager.LoadScene("mapScene");
    }
}