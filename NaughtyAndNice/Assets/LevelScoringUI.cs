using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScoringUI : MonoBehaviour
{
    public int niceChildrenSatisfied;
    public int niceChildrenMult;
    public int naughtyChildrenChased;
    public bool allChildrenCleared;
    public bool finalLevel;
    bool updated = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!updated) {

            updated = true;
            int score; 
            string text = "Nice children satisfied : " + niceChildrenSatisfied;
            text += "\nNaughty children chased : " + naughtyChildrenChased;

            if (!finalLevel)
            {                
                text += "\nChristmas Magic points earned :";
                text += "\n(" + niceChildrenSatisfied + " * " + niceChildrenMult;
                text += "\n + " + naughtyChildrenChased + " * 5)";
                if (allChildrenCleared)
                {
                    text += "\n* 1.5 (clear bonus)";
                }
                score = (int)Mathf.Round((niceChildrenSatisfied * niceChildrenMult + naughtyChildrenChased * 5) * (float)(allChildrenCleared ? 1.5 : 1));
                text += "\n= " + score;
                if (Random.Range(0f, 1f) < score / 100f)
                {
                    text += "\nLucky!! You got an upgrade!!\n";
                    int upgrade = Random.Range(0, 4);
                    switch (upgrade)
                    {
                        case 0:
                            text += "Gift power +5";
                            Santa.giftPower += 5;
                            break;
                        case 1:
                            text += "Gift per level +5";
                            Santa.giftPerLevel += 5;
                            break;
                        case 2:
                            text += "Charge +1";
                            Santa.maxBullets += 1;
                            break;
                        case 3:
                            text += "Throw speed +1";
                            Santa.throwSpeed += 1;
                            break;
                    }
                }
                text += "\nHit \"Enter\" to return to map";

                gameObject.GetComponentInChildren<Text>().text = text;
                Santa.points += score;
                Santa.totalPoints += score;
            }
            else {
                text += "\nFinal score :";
                text += "\n(" + niceChildrenSatisfied + " * " + niceChildrenMult;
                text += "\n + " + naughtyChildrenChased + " * 5)";

                score = (int)Mathf.Round((niceChildrenSatisfied * niceChildrenMult + naughtyChildrenChased * 5));
                text += "\n= " + score;

                text += "\nHit \"Enter\" to access ending screen";

                gameObject.GetComponentInChildren<Text>().text = text;
                Santa.finalScore = score;
            }

            Santa.niceChildrenSatisfied += niceChildrenSatisfied;
            Santa.naughtyChildrenChased += naughtyChildrenChased;
        }
    }
}
