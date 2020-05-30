using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData
{
	public static int[] SpellsLevelArray = new int[13];
	public static string[] NamesArray = new string[13];
	public static bool[] IsCardBought = new bool[12];
	public static int PermanentNum = 15;
	public static float experience;
	public static float money;
	public static float expGoal;

	public static bool button;
	public static int Level;
	public static float Hp;
	public static float Stamina;
	public static float StaminaBonus;
	public static string PlayerName;

	public static string firstPlaceName;
	public static string secondPlaceName;
	public static string thirdPlaceName;
	public static float firstPlaceTotalExp;
	public static float secondPlaceTotalExp;
	public static float thirdPlaceTotalExp;

	public static void startt(int _level,float expreiencee, float mony, bool[] _cards, string name1, string name2, string name3, float exp1, float exp2, float exp3, string name)
	{
		Level = _level;
		experience = expreiencee;
		money = mony;
		expGoal = 500;

		PlayerName = name;

		Hp = 100f + PermanentNum * (Level-1);
		Stamina = 100f+ PermanentNum * (Level-1);

		firstPlaceName = name1;
		secondPlaceName = name2;
		thirdPlaceName = name3;

		firstPlaceTotalExp = exp1;
		secondPlaceTotalExp = exp2;
		thirdPlaceTotalExp = exp3;

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
		//NamesArray[12] = "Heal";

		for(int i=0; i<_cards.Length; i++)
		{
			IsCardBought[i] = _cards[i];
		}

		for (int i = 0; i < SpellsLevelArray.Length; i++)
		{
			SpellsLevelArray[i] = 0;
		}

		for (int j = 1; j < Level; j++)
			expGoal += 300;
	}

	public static void LevelUp()
	{
		Level++;
		Stamina += PermanentNum;
		Hp += PermanentNum;
		experience -= expGoal;

		expGoal += 300;
	}

	public static string GetCardName(int i)
	{
		return NamesArray[i];
	}
}
