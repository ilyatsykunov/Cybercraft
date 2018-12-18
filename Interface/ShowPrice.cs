using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrice : MonoBehaviour {

    public Text priceText;

    void ActivateText(string price)
    {
        priceText.gameObject.SetActive(true);
        priceText.text = "price: " + price;
    }
    public void DeactivateText()
    {
        priceText.gameObject.SetActive(false);
    }

    public void Building1()
    {
        ActivateText("7000");
    }
    public void Building2()
    {
        ActivateText("15000");
    }
    public void Building3()
    {
        ActivateText("20000");
    }
    public void Unit1()
    {
        ActivateText("1000");
    }
    public void Unit2()
    {
        ActivateText("2000");
    }
    public void Unit3()
    {
        ActivateText("3000");
    }
}
