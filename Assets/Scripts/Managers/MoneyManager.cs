using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager i;

    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] Animator moneyAnim;
    int currentMoney = 0;

    private void Awake()
    {
        i = this;
    }

    public void AddMoney(int moneyToAdd)
    {
        currentMoney += moneyToAdd;
        moneyText.text = currentMoney.ToString();
        moneyAnim.SetTrigger("PlayEffect");
    }
}
