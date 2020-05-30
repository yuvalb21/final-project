using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StaminaBar : MonoBehaviour
{
    private float currentStamina;
    public GameObject StaminaTextbox;
    private void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 1);   
    }
    public void resetStats(float totalStamina)
    {
        currentStamina = totalStamina;
        StaminaTextbox.GetComponent<Text>().text = currentStamina.ToString();
    }
    public void TakeStamina(float stamina, float totalStamina)
    {
        if (currentStamina - stamina > totalStamina)
        {
            this.currentStamina = totalStamina;
        }
        else
        {
            this.currentStamina = this.currentStamina - stamina;
        }

        if (this.currentStamina <= 0)
        {
            transform.localScale = new Vector3(0f, 1f, 1f);
            this.currentStamina = 0;
        }
        else
            transform.localScale = new Vector3((currentStamina / totalStamina), 1f, 1f);
        StaminaTextbox.GetComponent<Text>().text = currentStamina.ToString();
    }
}
