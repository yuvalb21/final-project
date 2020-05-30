using UnityEngine;
using UnityEngine.UI;
using Proyecto26;

using System.Collections;
using System.Collections.Generic;
using System;
using System.Text.RegularExpressions;
using FullSerializer;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class Register : MonoBehaviour
{
    [SerializeField]
    public GameObject usernameRegister;
    public GameObject emailRegister;
    public GameObject passwordRegister;
    public GameObject confPasswordRegister;
    public GameObject comments;

    private string databaseURL = "https://yuvalprojectdatabase.firebaseio.com/users/";

    public static string UsernameText;
    public static string PasswordText;
    public static string ConfPasswordText;

    public static int level;
    public static float money;
    public static float experience;
    public static float totalExp;
    public static bool[] Cards = new bool[12];

    private bool namecheck = true;

    public void OnSubmit()
    {
        namecheck = true;
        PasswordText = passwordRegister.GetComponent<InputField>().text;
        UsernameText = usernameRegister.GetComponent<InputField>().text;
        ConfPasswordText = confPasswordRegister.GetComponent<InputField>().text;
        level = 1;
        money = 0f;
        experience = 0f;
        totalExp = 0f;
        for(int i=0; i<Cards.Length; i++)
        {
            if (i <= 1 || i == 11 || i == 10)
                Cards[i] = true;
            else
                Cards[i] = false;
        }
        

        if (UsernameText == "")
        {
            comments.GetComponent<Text>().text = "input an username";
            return;
        }
        if (PasswordText == "")
        {
            comments.GetComponent<Text>().text = "input a password";
            return;
        }

        if (ConfPasswordText == "")
        {
            comments.GetComponent<Text>().text = "confirm your password";
            return;
        }

        if(PasswordText.Length < 6)
        {
            comments.GetComponent<Text>().text = "Use 6 characters or more for your password";
            return;
        }

        if (UsernameText.Length > 10)
        {
            comments.GetComponent<Text>().text = "Use 10 characters or less for your username";
            return;
        }

        if (PasswordText.Length > 10)
        {
            comments.GetComponent<Text>().text = "Use 10 characters or less for your password";
            return;
        }


        RestClient.Get<User>(databaseURL + UsernameText + ".json").Then(response =>
        {
            if (response.Username == UsernameText)
            {
                namecheck = false;
                usernameRegister.GetComponent<InputField>().text = "";
                passwordRegister.GetComponent<InputField>().text = "";
                confPasswordRegister.GetComponent<InputField>().text = "";
                PasswordText = "";
                UsernameText = "";
                ConfPasswordText = "";
                namecheck = true;
                comments.GetComponent<Text>().text = "That username is taken. Try another";
            }

        }).Catch(error =>
        {
            Debug.Log("good usesrname");
            if (namecheck)
            {
                if (string.Equals(PasswordText, ConfPasswordText))
                {
                    PostToDataBase();
                }
                else
                    comments.GetComponent<Text>().text = "Those passwords didn't match. Try again.";
            }
        });
    }

    private void PostToDataBase()
    {
        User user = new User();
        RestClient.Put(databaseURL + UsernameText + ".json", user);
        usernameRegister.GetComponent<InputField>().text = "";
        passwordRegister.GetComponent<InputField>().text = "";
        confPasswordRegister.GetComponent<InputField>().text = "";
        comments.GetComponent<Text>().text = "Register succeeded";
    }
}
