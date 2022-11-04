using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollarBill : MonoBehaviour
{
    int dollarBillWorth = 20;
    [SerializeField] AudioClip moneyPickedSFX;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        SoundManager.i.PlaySound(moneyPickedSFX);
        MoneyManager.i.AddMoney(dollarBillWorth);
        Destroy(this.gameObject);
    }
}
