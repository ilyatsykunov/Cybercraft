using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrice : MonoBehaviour {

    public Text priceText;

    public void ActivateText(GameObject building)
    {
        priceText.gameObject.SetActive(true);
        priceText.text = "Price: £" + building.GetComponent<Building>().price.ToString();
        priceText.color = Color.white;
    }
    public void DeactivateText()
    {
        priceText.gameObject.SetActive(false);
    }
}
