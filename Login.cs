using Proyecto26;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public class Login : MonoBehaviour
{
    public GameObject usernameText;
    public GameObject passwordText;

    [SerializeField]
    private GameObject comments;

    public static string Password;
    public static string Username;

    public static int num = 100000000;

    public static string[] top3Names = new string[3];
    public static float[] top3TotalExp = new float[3];

    public static string first = "FirstPlace.json";
    public static string Second = "SecondPlace.json";
    public static string Third = "ThirdPlace.json";

    private static string databaseURL = "https://yuvalprojectdatabase.firebaseio.com/users/";
    private static string databaseURLTop3 = "https://yuvalprojectdatabase.firebaseio.com/top3/";

    private void Awake()
    {
        for(int i=0; i<top3Names.Length;i++)
        {
            top3Names[i] = "";
            top3TotalExp[i] = 0;
        }
    }

    public void OnSubmit()
    {
        Debug.Log("try to login");
        Username = usernameText.GetComponent<InputField>().text; ;
        Password = passwordText.GetComponent<InputField>().text; ;
        if (Password == null || Password == "")
        {
            comments.GetComponent<Text>().text = "input a password!";
            return;
        }
        if (Username == null || Username == "")
        {
            comments.GetComponent<Text>().text = "input a username!";
            return;
        }
        RestClient.Get<User>(databaseURL + Username + ".json").Then(response =>
        {
            Debug.Log("1" + response.Username + " 2 " + Username + " 3 " + response.Password + " 4 " + Password);
            if (string.Equals(response.Username, Username) && string.Equals(response.Password, Password))
            {
                Debug.Log("work");
                //input data to the playerData 
                PlayerData.startt(response.level, response.experience, response.money, response.Cards, top3Names[0], top3Names[1], top3Names[2],
                    top3TotalExp[0], top3TotalExp[1], top3TotalExp[2], Username);
                FirstPlace();
                SecondPlace();
                ThirdPlace();
                comments.GetComponent<Text>().text = "";
                Debug.Log(PlayerData.firstPlaceName);
                usernameText.GetComponent<InputField>().text = "";
                passwordText.GetComponent<InputField>().text = "";
                SceneManager.LoadScene("towerScene");
            }
            else
                comments.GetComponent<Text>().text = "Incorrect username or password";
        }).Catch(error =>
        {
            comments.GetComponent<Text>().text = "fail";
        });

    }
    public static void UpdateMoney(float num)
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(response =>
        {
            User temp = response;
            temp.money = num;
            RestClient.Put(databaseURL + Username + ".json", temp);

        }).Catch(error =>
        {
            Debug.Log("fail");
        });
    }

    public static void UpdateMoneyAndLevel(float num, float exp)
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(response =>
        {
            User temp = response;
            temp.money = num;
            temp.level++;
            temp.experience = exp;
            RestClient.Put(databaseURL + Username + ".json", temp);

        }).Catch(error =>
        {
            Debug.Log("fail");
        });
    }

    public static void UpdateExpAndMoney(float money, float exp)
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(response =>
        {
            User temp = response;
            temp.experience += exp;
            temp.totalExp += exp;
            temp.money += money;
            RestClient.Put(databaseURL + Username + ".json", temp);
            RestClient.Get<User>(databaseURL + Username + ".json").Then(resp =>
            {
                Debug.Log("first");
                for (int i = 0; i < num; i++)
                { }
                Debug.Log("finishhh");
                Top3TableArray();
            }).Catch(error => Debug.Log("fail")); 

        }).Catch(error =>
        {
            Debug.Log("fail");
        });
    }

    private static void FirstPlace()
    {
        RestClient.Get<User>(databaseURLTop3 + first).Then(response =>
        {
            top3Names[0] = response.Username;
            top3TotalExp[0] = response.totalExp;
            PlayerData.firstPlaceName = response.Username;
            PlayerData.firstPlaceTotalExp = response.totalExp;
        }).Catch(error =>{
            User user = new User();
            user.Username = "";
            user.Password = "";
            user.totalExp = 0f;
            top3Names[0] = "";
            top3TotalExp[0] = 0;
            RestClient.Put(databaseURLTop3 + first, user);});
    }

    private static void SecondPlace()
    {
        RestClient.Get<User>(databaseURLTop3 + Second).Then(response =>
        {
            top3Names[1] = response.Username;
            top3TotalExp[1] = response.totalExp;
            PlayerData.secondPlaceName = response.Username;
            PlayerData.secondPlaceTotalExp = response.totalExp;
        }).Catch(error =>
        {
            User user = new User();
            user.Username = "";
            user.Password = "";
            user.totalExp = 0f;
            top3Names[1] = "";
            top3TotalExp[1] = 0;
            RestClient.Put(databaseURLTop3 + Second, user);
        });
    }

    private static void ThirdPlace()
    {
        RestClient.Get<User>(databaseURLTop3 + Third).Then(response =>
        {
            top3Names[2] = response.Username;
            top3TotalExp[2] = response.totalExp;
            PlayerData.thirdPlaceName = response.Username;
            PlayerData.thirdPlaceTotalExp = response.totalExp;
        }).Catch(error =>
        {
            User user = new User();
            user.Username = "";
            user.Password = "";
            user.totalExp = 0f;
            top3Names[2] = "";
            top3TotalExp[2] = 0;
            RestClient.Put(databaseURLTop3 + Third, user);
        });
    }

    private static void Top3TableArray()
    {
        if (Username == top3Names[0])
        {
            RestClient.Get<User>(databaseURL + Username + ".json").Then(responsePlayer =>
             {
                 RestClient.Get<User>(databaseURLTop3 + first).Then(responseFirstPlace =>
                 {
                     top3TotalExp[0] = responsePlayer.totalExp;
                     PlayerData.firstPlaceTotalExp = responsePlayer.totalExp;
                     RestClient.Put(databaseURLTop3 + first, responsePlayer);
                 }).Catch(error => Debug.Log("fail"));
             }).Catch(error => Debug.Log("fail"));
        }

        else if (Username == top3Names[1])
        {
            RestClient.Get<User>(databaseURL + Username + ".json").Then(responsePlayer =>
            {
                if (responsePlayer.totalExp > top3TotalExp[0])
                {
                    RestClient.Get<User>(databaseURLTop3 + first).Then(FirstPlaceResponse =>
                    {
                        top3Names[1] = top3Names[0];
                        top3TotalExp[1] = top3TotalExp[0];
                        top3Names[0] = responsePlayer.Username;
                        top3TotalExp[0] = responsePlayer.totalExp;

                        PlayerData.firstPlaceTotalExp = responsePlayer.totalExp;
                        PlayerData.firstPlaceName = responsePlayer.Username; 
                        PlayerData.secondPlaceName = FirstPlaceResponse.Username;
                        PlayerData.secondPlaceTotalExp = FirstPlaceResponse.totalExp;

                        RestClient.Put(databaseURLTop3 + first, responsePlayer);
                        RestClient.Put(databaseURLTop3 + Second, FirstPlaceResponse);
                    }).Catch(error => { Debug.Log("fail"); });
                }
                else
                {
                    top3TotalExp[1] = responsePlayer.totalExp;
                    PlayerData.secondPlaceTotalExp = responsePlayer.totalExp;
                    RestClient.Put(databaseURLTop3 + Second, responsePlayer);
                }
            }).Catch(error => Debug.Log("fail"));
        }

        else if (Username == top3Names[2])
        {
            RestClient.Get<User>(databaseURL + Username + ".json").Then(responsePlayer =>
            {
                if (responsePlayer.totalExp > top3TotalExp[0])
                {
                    RestClient.Get<User>(databaseURLTop3 + first).Then(FirstPlaceResponse =>
                    {
                        RestClient.Get<User>(databaseURLTop3 + Second).Then(SecondPlaceResponse =>
                        {
                            top3TotalExp[1] = top3TotalExp[0];
                            top3Names[1] = top3Names[0];
                            top3TotalExp[0] = responsePlayer.totalExp;
                            top3Names[0] = Username;
                            top3Names[2] = SecondPlaceResponse.Username;
                            top3TotalExp[2] = SecondPlaceResponse.totalExp;

                            PlayerData.firstPlaceTotalExp = responsePlayer.totalExp;
                            PlayerData.firstPlaceName = responsePlayer.Username;
                            PlayerData.secondPlaceName = FirstPlaceResponse.Username;
                            PlayerData.secondPlaceTotalExp = FirstPlaceResponse.totalExp;
                            PlayerData.thirdPlaceName = SecondPlaceResponse.Username;
                            PlayerData.thirdPlaceTotalExp = SecondPlaceResponse.totalExp;

                            RestClient.Put(databaseURLTop3 + first, responsePlayer);
                            RestClient.Put(databaseURLTop3 + Second, FirstPlaceResponse);
                            RestClient.Put(databaseURLTop3 + Third, SecondPlaceResponse);
                        }).Catch(error => { Debug.Log("fail"); });
                    }).Catch(error => { Debug.Log("fail"); });
                }
                else if (responsePlayer.totalExp > top3TotalExp[1])
                {
                    RestClient.Get<User>(databaseURLTop3 + Second).Then(SecondPlaceResponse =>
                    {
                        top3Names[1] = top3Names[2];
                        top3TotalExp[1] = top3TotalExp[2];
                        top3Names[2] = SecondPlaceResponse.Username;
                        top3TotalExp[2] = SecondPlaceResponse.totalExp;

                        PlayerData.secondPlaceName = responsePlayer.Username;
                        PlayerData.secondPlaceTotalExp = responsePlayer.totalExp;
                        PlayerData.thirdPlaceName = SecondPlaceResponse.Username;
                        PlayerData.thirdPlaceTotalExp = SecondPlaceResponse.totalExp;

                        RestClient.Put(databaseURLTop3 + Second, responsePlayer);
                        RestClient.Put(databaseURLTop3 + Third, SecondPlaceResponse);
                    }).Catch(error => { Debug.Log("fail"); });
                }
                else
                {
                    top3TotalExp[2] = responsePlayer.totalExp;
                    PlayerData.thirdPlaceTotalExp = responsePlayer.totalExp;
                    RestClient.Put(databaseURLTop3 + Third, responsePlayer);
                }
            }).Catch(error => { Debug.Log("fail"); });           
        }
        else
            IfNotInTable();
    }

    private static void IfNotInTable()
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(PlayerResponse =>
        {
            if (PlayerResponse.totalExp > top3TotalExp[2])
            {
                if (PlayerResponse.totalExp > top3TotalExp[1])
                {
                    if (PlayerResponse.totalExp > top3TotalExp[0])
                    {
                        RestClient.Get<User>(databaseURLTop3 + first).Then(FirstPlaceResponse =>
                        {
                            RestClient.Get<User>(databaseURLTop3 + Second).Then(SecondPlaceResponse =>
                            {
                                PlayerData.firstPlaceTotalExp = PlayerResponse.totalExp;
                                PlayerData.firstPlaceName = PlayerResponse.Username;
                                PlayerData.secondPlaceName = FirstPlaceResponse.Username;
                                PlayerData.secondPlaceTotalExp = FirstPlaceResponse.totalExp;
                                PlayerData.thirdPlaceName = SecondPlaceResponse.Username;
                                PlayerData.thirdPlaceTotalExp = SecondPlaceResponse.totalExp;

                                top3TotalExp[1] = top3TotalExp[0];
                                top3Names[1] = top3Names[0];
                                top3TotalExp[0] = PlayerResponse.totalExp;
                                top3Names[0] = Username;
                                top3Names[2] = SecondPlaceResponse.Username;
                                top3TotalExp[2] = SecondPlaceResponse.totalExp;

                                RestClient.Put(databaseURLTop3 + first, PlayerResponse);
                                RestClient.Put(databaseURLTop3 + Second, FirstPlaceResponse);
                                RestClient.Put(databaseURLTop3 + Third, SecondPlaceResponse);
                            }).Catch(error => { Debug.Log("fail"); });
                        }).Catch(error => { Debug.Log("fail"); });
                    }
                    else
                    {
                        RestClient.Get<User>(databaseURLTop3 + Second).Then(SecondPlaceResponse =>
                        {
                            top3Names[2] = top3Names[1];
                            top3TotalExp[2] = top3TotalExp[1];
                            top3TotalExp[1] = PlayerResponse.totalExp;
                            top3Names[1] = Username;

                            PlayerData.secondPlaceName = PlayerResponse.Username;
                            PlayerData.secondPlaceTotalExp = PlayerResponse.totalExp;
                            PlayerData.thirdPlaceName = SecondPlaceResponse.Username;
                            PlayerData.thirdPlaceTotalExp = SecondPlaceResponse.totalExp;

                            RestClient.Put(databaseURLTop3 + Second, PlayerResponse);
                            RestClient.Put(databaseURLTop3 + Third, SecondPlaceResponse);
                        }).Catch(error => { Debug.Log("fail"); });
                    }
                }
                else
                {
                    top3TotalExp[2] = PlayerResponse.totalExp;
                    top3Names[2] = Username;

                    PlayerData.thirdPlaceName = PlayerResponse.Username;
                    PlayerData.thirdPlaceTotalExp = PlayerResponse.totalExp;

                    RestClient.Put(databaseURLTop3 + Third, PlayerResponse);
                }
            }
        }).Catch(error => { Debug.Log("fail"); });
    }

    public static void Restartt()
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(responsePlayer =>
        {
            responsePlayer.level = 1;
            responsePlayer.money = 0f;
            responsePlayer.experience = 0f;
            responsePlayer.totalExp = 0f;
            for (int i = 0; i < responsePlayer.Cards.Length; i++)
            {
                if (i <= 1 || i == 11 || i == 10)
                    responsePlayer.Cards[i] = true;
                else
                    responsePlayer.Cards[i] = false;
            }
            RestClient.Put(databaseURL + Username + ".json", responsePlayer);
            if(Username == top3Names[0])
            {
                RestClient.Get<User>(databaseURLTop3 + Second).Then(SecondPlaceResponse =>
                {
                    RestClient.Get<User>(databaseURLTop3 + Third).Then(ThirdPlaceResponse =>
                    {
                        top3TotalExp[0] = top3TotalExp[1];
                        top3Names[0] = top3Names[1];
                        top3TotalExp[1] = ThirdPlaceResponse.totalExp;
                        top3Names[1] = ThirdPlaceResponse.Username;
                        User user = new User();
                        user.Username = "";
                        user.Password = "";
                        user.totalExp = 0f;
                        top3Names[2] = "";
                        top3TotalExp[2] = 0;
                        RestClient.Put(databaseURLTop3 + first, SecondPlaceResponse);
                        RestClient.Put(databaseURLTop3 + Second, ThirdPlaceResponse);
                        RestClient.Put(databaseURLTop3 + Third, user);
                    }).Catch(error => { Debug.Log("fail"); });
                }).Catch(error => { Debug.Log("fail"); });
            }

            if (Username == top3Names[1])
            {
                RestClient.Get<User>(databaseURLTop3 + Third).Then(ThirdPlaceResponse =>
                {
                    top3TotalExp[1] = top3TotalExp[2];
                    top3Names[1] = top3Names[2];
                    User user = new User();
                    user.Username = "";
                    user.Password = "";
                    user.totalExp = 0f;
                    top3Names[2] = "";
                    top3TotalExp[2] = 0;
                    RestClient.Put(databaseURLTop3 + Second, ThirdPlaceResponse);
                    RestClient.Put(databaseURLTop3 + Third, user);
                }).Catch(error => { Debug.Log("fail"); });
            }

            if (Username == top3Names[2])
            {
                User user = new User();
                user.Username = "";
                user.Password = "";
                user.totalExp = 0f;
                top3Names[2] = "";
                top3TotalExp[2] = 0;
                RestClient.Put(databaseURLTop3 + Third, user);
            }
            SceneManager.LoadScene("loginScene");
        }).Catch(error => Debug.Log("fail"));
    }

    public static void UpdatePurchaseCardAndMoney(int i, float money)
    {
        RestClient.Get<User>(databaseURL + Username + ".json").Then(response =>
        {
            User temp = response;
            temp.Cards[i] = true;
            temp.money = money;
            RestClient.Put(databaseURL + Username + ".json", temp);

        }).Catch(error =>
        {
            Debug.Log("fail");
        });
    }

}