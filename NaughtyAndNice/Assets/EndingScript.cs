using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string text = "Congrats!! You helped Santa through his journey around town!";
        text += "\n\nDuring this trip, you visited :";
        text += "\n" + Santa.levelNodesVisited + " houses with children";
        text += "\nBringing smile to " + Santa.niceChildrenSatisfied + " children";
        text += "\nAnd punishing " + Santa.naughtyChildrenChased + " naughty children";
        text += "\n\nYou also :";
        text += "\nReceived help from " + Santa.resourceNodesVisited + " elves";
        text += "\nGiving you " + Santa.pointsGotAtResources + " Christmas Magic points";
        text += "\nAnd spent " + (Santa.totalPoints - Santa.points) + " points at " + Santa.shopNodesVisited + " elven shops";
        text += "\n\nDuring the journey, your Santa learned to :";
        text += "\nBring " + Santa.giftPerLevel + " gifts worth " + Santa.giftPower + " points each to every house visited";
        text += "\nAnd throw those gifts at a speed of " + Santa.throwSpeed + ", keeping " + Santa.maxBullets + " projectiles on screen";
        text += "\n\nYou acquired " + Santa.totalPoints + " Christmas Magic Points during your whole trip";
        text += "\nAnd got a final score of " + Santa.finalScore + " on the last level.";
        text += "\n\nShare this exploit with your friends!!";
        gameObject.GetComponentInChildren<Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
