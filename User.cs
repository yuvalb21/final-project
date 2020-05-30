using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]

public class User
{
    public string Username;
    public string Password; 

    public int level;
    public float money;
    public float experience;
    public float totalExp;
    public bool[] Cards = new bool[12];

    public User()
    {
        Username = Register.UsernameText;
        Password = Register.PasswordText;
        totalExp = Register.totalExp;

        level = Register.level;
        money = Register.money;
        experience = Register.experience;
        for(int i=0; i<Cards.Length; i++)
        {
            Cards[i] = Register.Cards[i];
        }
    }
}
