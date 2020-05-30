using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class enemyManager : MonoBehaviour
{
	//private GameObject[] slots;
	public SpellClass[] attacks;
	public GameObject Player;
	public GameObject BlocksArray;

	private int Level;
	private float Hp;
	private float TotalHp;
	private float TotalStamina;
	private float Stamina;
	private float StaminaBonus;
	private bool isBoss;
	private int turn = 0;
	int number;

	public int rowEnemy;
	public int coulmnEnemy;

	private string[] movesArray = new string[8];
	private string[] moves = new string[3];
	public float[] staminaMoves = new float[3];
	public float[] damageMoves = new float[3];
	public enemyManager(float hp, float stamina, bool isBoss)
	{
		this.Hp = hp;
		this.TotalStamina = stamina;
		this.Stamina = stamina;
		this.TotalHp = hp;
		this.StaminaBonus = 14 + Level;
		this.isBoss = isBoss;

		movesArray[0] = "downEnemy";
		movesArray[1] = "upEnemy";
		movesArray[2] = "rightEnemy";
		movesArray[3] = "leftEnemy";
		movesArray[4] = "ShieldEnemy";
		movesArray[5] = "MagicRenewalEnemy";
		movesArray[6] = "RockPunchAttack";
		movesArray[7] = "RollingStoneAttack";

		attacks = new SpellClass[3];

		if (isBoss == false)
		{
			attacks[1] = new SpellClass("RockPunchAttack", 20f, 20f);
			attacks[0] = new SpellClass("RollingStoneAttack", 15f, 15f);
		}

		if (isBoss == true)
		{
			attacks[1] = new SpellClass("RockPunchAttack", 20f, 30f);
			attacks[0] = new SpellClass("RollingStoneAttack", 15f, 25f);
			this.Hp += 15;
			this.TotalStamina += 15;
			this.Stamina += 15;
			this.TotalHp += 15;
		}
		attacks[2] = new SpellClass("MagicRenewalEnemy", -20f, 0f);

		ResetEnemy();
	}
	public void pickAttacks(int rowPlayer, int coulmnPlayer, int _rowEnemy, int _coulmnEnemy)
	{
		rowEnemy = _rowEnemy;
		coulmnEnemy = _coulmnEnemy;
		int counter = 0;
		bool continu = false;


		while(continu == false)
		{
			bool staminafull = false;
			bool RemoteAttackOrToMuchStamina = false;
			bool doubleMovement = false;

			
			int num = (int)(Random.Range(0f, 501f));

			if (num <= 40)
				number = 0;
			else if (num <= 80 && num > 40)
				number = 1;
			else if (num <= 120 && num > 80)
				number = 2;
			else if (num <= 160 && num > 120)
				number = 3;
			else if (num <= 260 && num > 160)
				number = 4;
			else if (num <= 340 && num > 260)
				number = 5;
			else if (num <= 420 && num > 340)
				number = 6;
			else if (num <= 500 && num > 420)
				number = 7;


			Debug.Log("num: " + number);
			moves[counter] = movesArray[number];
		
			if (this.TotalStamina == this.Stamina && moves[counter] == attacks[2].getNameSpell())//if the stamina full and the card is magic renewal
			{
				staminafull = true;
			}
		
			if((counter >= 1 && (moves[1] == moves[0] || moves[1] == moves[2] || moves[0] == moves[2]) && moves[counter].Contains("Attack") == false))
			{
				//if there is movement to any direction twice
				doubleMovement = true;
			}
		
			if(moves[counter].Contains("Attack") == true)
			{
				//if the attack's stamina required is more than the current stamina or the enemy is too far from the player
				SpellClass attack = whichAtttack(moves[counter]);
				if((attack.getStaminaSpell() > this.TotalStamina || (coulmnPlayer - coulmnEnemy == 3) || (coulmnPlayer - coulmnEnemy == -3) || (rowPlayer - rowEnemy == 2) || (rowPlayer - rowEnemy == -2)) && turn == 0)
					RemoteAttackOrToMuchStamina = true;
			}
		
			if (doubleMovement == false && RemoteAttackOrToMuchStamina == false && staminafull == false)
			{
				for (int i = 0; i < attacks.Length; i++)
				{
					if (moves[counter] == attacks[i].getNameSpell())
					{
						this.TotalStamina -= attacks[i].getStaminaSpell();
						staminaMoves[counter] = attacks[i].getStaminaSpell();
						damageMoves[counter] = attacks[i].getDamage();
						Debug.Log(this.TotalStamina + " " + attacks[i].getNameSpell() +" "+ attacks[i].getStaminaSpell());
					}
				}
				counter++;
				if (counter > 2)
					continu = true;
			}
			movement();
		}
		if ((!moves[0].Contains("Attack")) && (!moves[1].Contains("Attack")) && (!moves[2].Contains("Attack")) && turn > 0)
		{
			if (this.TotalStamina >= attacks[0].getStaminaSpell() && this.TotalStamina >= attacks[1].getStaminaSpell())
			{
				Debug.Log("no attack");
				int numMoves = (int)(Random.Range(0f ,3f));
				int atack = (int)(Random.Range(0f, 2f));
				Debug.Log(atack + " pppppppppp " + numMoves);
				moves[numMoves] = attacks[atack].getNameSpell();
		
				this.TotalStamina -= attacks[atack].getStaminaSpell();
				staminaMoves[numMoves] = attacks[atack].getStaminaSpell();
				damageMoves[numMoves] = attacks[atack].getDamage();
			}
		}



		this.TotalStamina += 15;
		if (this.TotalStamina > this.Stamina)
			this.TotalStamina = this.Stamina;

		//moves[2] = "RollingStoneAttack";
		//staminaMoves[2] = attacks[1].getStaminaSpell();
		//this.TotalStamina -= attacks[1].getStaminaSpell();
		for (int i = 0; i < moves.Length; i++)
		{
			Debug.Log("000099999999 " + moves[i]);
			Debug.Log("Llllllllllll " + staminaMoves[i] + " " +i);
		}			

		Debug.Log(this.TotalStamina + " jjjjjjjj");
		turn++;
	}

	private SpellClass whichAtttack(string a)
	{
		if (a == attacks[0].getNameSpell())
			return attacks[0];
		else
			return attacks[1];
	}

	public string getCurrentMove(int number)
	{
		return moves[number];
	}

	public float getCurrentstaminaMove(int number)
	{
		return staminaMoves[number];
	}

	public float getCurrentdamageMove(int number)
	{
		return damageMoves[number];
	}

	public void ResetEnemy()
	{
		for (int i = 0; i < moves.Length; i++)
			moves[i] = "";

		for (int i = 0; i < staminaMoves.Length; i++)
			staminaMoves[i] = 0;

		for (int i = 0; i < damageMoves.Length; i++)
			damageMoves[i] = 0;
	}

	public float getStaminaEnemy()
	{
		return this.Stamina;
	}
	public float getTotalStaminaEnemy()
	{
		return this.TotalStamina;
	}

	public float getStaminaBonus()
	{
		return this.StaminaBonus;
	}
	public float getHpEnemy()
	{
		return this.Hp;
	}
	public float getTotalHpEnemy()
	{
		return this.TotalHp;
	}
	public void setTotalHpEnemy(float num)
	{
		this.TotalHp = num;
	}
	private void movement()
	{
		bool moved = false;
		int number;
		if (coulmnEnemy == 0 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "leftEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "leftEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (coulmnEnemy == 3 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "rightEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "rightEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 2 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "downEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "downEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 0 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "upEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "upEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 0 && coulmnEnemy == 0 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "upEnemy" || moves[i] == "leftEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "upEnemy" || moves[i] == "leftEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 2 && coulmnEnemy == 0 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "downEnemy" || moves[i] == "leftEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "downEnemy" || moves[i] == "leftEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 0 && coulmnEnemy == 3 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "upEnemy" || moves[i] == "rightEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "upEnemy" || moves[i] == "rightEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}

		if (rowEnemy == 2 && coulmnEnemy == 3 && moved == false)
		{
			for (int i = 0; i < moves.Length; i++)
			{
				if (moves[i] == "downEnemy" || moves[i] == "rightEnemy")
				{
					number = (int)(Random.Range(0f, 4f));
					moves[i] = movesArray[number];
					while (moves[i] == "downEnemy" || moves[i] == "rightEnemy")
					{
						number = (int)(Random.Range(0f, 4f));
						moves[i] = movesArray[number];
					}
					UpdateMovement(number);
					moved = true;
				}
			}
		}
	}
	private void UpdateMovement(int num)
	{
		if (num == 0)
			rowEnemy++;
		if (num == 1)
			rowEnemy--;
		if (num == 2)
			coulmnEnemy++;
		if (num == 3)
			coulmnEnemy--;
	}
}
