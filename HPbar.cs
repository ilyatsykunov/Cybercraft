using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    public Canvas canvas;
    public Image healthBar;
    public GameObject healthBarPrefab;
    private Human parentScript;

    // Use this for initialization
    void Awake()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        parentScript = GetComponentInParent<Human>();
        if (healthBar != null)
        {
            ChangeSizeOfHealthBar();
            Vector3 hpBarPosition = Camera.main.WorldToScreenPoint(this.transform.position);
            healthBar.transform.position = hpBarPosition;
        }
        else
        {
            var spawnImage = Instantiate(healthBarPrefab) as GameObject;
            spawnImage.transform.SetParent(canvas.transform, false);
            healthBar = spawnImage.GetComponent<Image>();
        }
    }

    // Update is called once per frame
    void Update()
    {


    }
    void ChangeSizeOfHealthBar()
    {
        int health = parentScript.health;
        float width = 30 * health / 100;
        var healthBarTransform = healthBar.gameObject.transform as RectTransform;
        healthBarTransform.sizeDelta = new Vector2(width, healthBarTransform.sizeDelta.y);
    }
}

