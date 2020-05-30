using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellClass
{
	private string name;
	private float stamina;
	private float damage;
	private int stars;
	public SpellClass(string name, float stamina, float damage)
	{
		this.name = name;
		this.stamina = stamina;
		this.damage = damage;
	}

	public string getNameSpell()
	{
		return this.name;
	}

	public float getStaminaSpell()
	{
		return this.stamina;
	}

	public float getDamage()
	{
		return this.damage;
	}

	public void setStars()
	{
		stars++;
		setNewStarAttacks();
	}

	public int getStars()
	{
		return this.stars;
	}

	private void setNewStarAttacks()
	{
		if(stars == 1)
			this.stamina -= 5;
		if (stars == 2)
		{
			if(this.damage != 0)
				this.stamina -= 5;
			else
				this.damage += 5;
		}
	}
}