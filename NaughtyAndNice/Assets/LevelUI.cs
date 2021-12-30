using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    Text generalInfoText;
    Text giftRemainText;
    public int timeRemain;
    public int pointsNeeded;
    public int currentPoints;
    public int giftRemain;
    public bool showGiftNumber;

    // Start is called before the first frame update
    void Start()
    {
        Text[] texts = gameObject.GetComponentsInChildren<Text>();
        foreach (Text t in texts) {
            if (t.text.Contains("Time"))
            {
                generalInfoText = t;
            }
            else {
                giftRemainText = t;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        string text = "Points gained in level: " + currentPoints;
        text += "\nPoints needed per children : " + pointsNeeded;
        text += "\nTime remaining : " + timeRemain;
        generalInfoText.text = text;
        giftRemainText.text = "" + (showGiftNumber ? "" + giftRemain : "");
    }
}
