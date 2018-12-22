using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIbarController : MonoBehaviour {

    private Image img;
    private Color ranColor;
    private float timeLeft;
    public List<Image> listOfPics = new List<Image>();

    // Use this for initialization
    void Start () {
        img = gameObject.GetComponent<Image>();
        listOfPics.Add(img);
    }
    private void Update()
    {
        foreach(Image pic in listOfPics)
        {
            if (timeLeft <= Time.deltaTime)
            {
                pic.color = ranColor;
                ranColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
                StartCoroutine("Wait");
                timeLeft = Random.Range(5, 10);
            }
            else
            {
                pic.color = Color.Lerp(img.color, ranColor, Time.deltaTime / timeLeft);
                
            }
        }
        timeLeft -= Time.deltaTime;

    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(Random.Range(3, 6));
    }
    public void ActivateButton(GameObject pic)
    {
        pic.SetActive(true);
    }
    public void DeactivateButton(GameObject pic)
    {
        pic.SetActive(false);
    }
    public void HighlightButton(Image button)
    {
        listOfPics.Remove(button);
        button.color = Color.white;
    }
    public void AddToList(Image button)
    {
        if (!listOfPics.Contains(button))
        {
            listOfPics.Add(button);
        }
    }
}
