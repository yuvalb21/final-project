using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClass 
{
	public SpellClass[] spellsArray = new SpellClass[13];
	private string[] NamesArray = new string[13];
	private float num = 15f;

	private int Level;
	private float Hp;
	private float TotalHp;
	private float TotalStamina;
	private float Stamina;
	private float StaminaBonus;

	public PlayerClass(int level, float hp, float stamina)
	{
		this.Level = level;
		this.Hp = hp;
		this.TotalStamina = stamina;
		this.Stamina = stamina;
		this.TotalHp = hp;
		this.StaminaBonus = 14 + Level;


		NamesArray[0] = "SnowFlakesAttack";
		NamesArray[1] = "FogAttack";
		NamesArray[2] = "WaterFallAttack";
		NamesArray[3] = "HailAttack";
		NamesArray[4] = "IciclesAttack";
		NamesArray[5] = "IceBallAttack";
		NamesArray[6] = "IceFieldAttack";
		NamesArray[7] = "WhirlpoolAttack";
		NamesArray[8] = "AvalancheAttack";
		NamesArray[9] = "TsunamiAttack";
		NamesArray[10] = "MagicRenewal";
		NamesArray[11] = "Shield";
		NamesArray[12] = "Heal";

		for (int i = 0; i<spellsArray.Length; i++)
		{
			if (i <= 9)
			{
				spellsArray[i] = new SpellClass(NamesArray[i], (num + i * 5), (num + i * 5));
			}
			if(i == 10)
				spellsArray[i] = new SpellClass(NamesArray[i], -20f, 0f);//change it that it will be dependent in the level
														   
			if (i == 12)								   
				spellsArray[i] = new SpellClass(NamesArray[i], 20f, 20f);//change it that it will be dependent in the level
														   
			if (i == 11)								   
				spellsArray[i] = new SpellClass(NamesArray[i], 0f, 0f);//change it that it will be dependent in the level

		}
	}

	public float getStaminaPlayer()
	{
		return this.Stamina;
	}
	public float getTotalStaminaPlayer()
	{
		return this.TotalStamina;
	}

	public float getStaminaBonus()
	{
		return this.StaminaBonus;
	}
	public float getHpPlayer()
	{
		return this.Hp;
	}

	public float getTotalHpPlayer()
	{
		return this.Hp;
	}

	public void setTotalStamina(float numb)
	{
		this.TotalStamina = numb;
	}

	public void setTotalHpPlayer(float numb)
	{
		this.TotalHp = numb;
	}
}
