using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextUpdate : MonoBehaviour
{
    public int time;
    public int giftRemain;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        string text = "Christmas Magic points : " + Santa.points;
        text += "\nTime remaining : " + time;
        Text[] texts = gameObject.GetComponents<Text>();

        foreach (Text t in texts) {
            if (t.text.Contains("Time"))
            {
                t.text = text;
            }
            else {
                t.text = "" + giftRemain;
            }
        }

    }
}
