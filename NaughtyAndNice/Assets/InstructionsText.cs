using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsText : MonoBehaviour
{
    public int instructionsPage = 0;
    public string[] instructions = { "Ho ho ho!\n" +
        "Santa Claus is coming to town!\n\n" +

        "\"He's making a list\n" +
        "And checking it twice.\n" +
        "Gonna find out\n" +
        "Who's naughty and nice.\n" +
        "Santa Claus is comin' to town.\"\n\n" +

        "Follow Santa's journey as he rewards nice children with gifts and naughty children with coal!\n" +

        "During the game, you will have to choose between multiple paths before reaching the final level, " +
            "where a highscore mode will enable you to share your final score with others!\n\n" +

        "Use left and right arrows to navigate in the instructions",

        "Naughty and Nice is a roguelike shooter game where the player uses his mouse to aim and throw gifts " +
            "or coal at children, in order to collect Christmas Magic points that can be exchanged for power-" +
            "ups in elven shops. Power-ups will come in handy in the final level in order to achieve a higher " +
            "score.\n\n" +

        "The player starts his journey at an initial node on the left of the map. At each step, he can choose " +
            "between multiple paths on the right with number keys. Each path will lead to a different node " +
            "corresponding to different events.\n\n" +

        "When the player has reached the final node on the right, he will face the final level where his final score " +
            "is computed according to the score reached in that level.",

        "Map instructions :\n" +
        "Different types of event nodes can be encountered on the map.\n\n" +

        "Level nodes:\n" +

        "Challenge a level to gain Christmas Magic points or even power-ups.\n\n" +

        "Resource node:\n" +

        "Gain a random amount of Christmas Magic points.\n\n" +

        "Shop node:\n" +

        "Allows to exchange Christmas Magic points for power-ups. Select desired product with number keys.\n\n" +

        "To choose a node, the player hits a number key. He then hits \"Enter\" to enter the node.",

        "Level instructions 1:\n" +
        "During a level, the player uses his mouse to aim and throw gifts or coal at children.\n\n" +

        "At the beginning of each level, a list containing the images of nice and naughty children will " +
            "be shown. The list can be consulted at any moment with the hotkey \"L\".\n\n" +

        "Coal must be thrown at naughty kids and gifts be thrown at nice kids.\n\n" +

        "The amount of gifts that can be thrown in each level is fixed, but the number of coal isn't. " +
            "Player alternate between those objects with the \"Spacebar\" key.\n\n" +

        "Each gift give some satisfaction to a child.The quantity of gift points needed to satisfy a " +
            "child vary for each level.",

        "Level instructions 2:\n" +
        "A level finishes when all children received their gift or coal, when all gifts are thrown or when " +
            "the timer runs out.\n\n" +

        "Note : take care of naughty children first! If the gift quantity " +
            "runs out, Santa will not stay in the level any longer even though there are naughty children left! Time is " +
            "precious on Christmas night!!\n\n" +

        "Highscore mode :\n\n" +

        "The last level encountered. Children on screen will respawn as long as the timer doesn't run out and " +
            "Santa has remaining gifts. The score reached in that level is the final score of the game.",

        "Santa's stats:\n" +
        "Different stats affects Santa's perfermance in a level. Those stats can be upgraded at a shop node.\n\n" +

        "Gift power : the amount of satisfaction a nice child receives for a gift, very important stat for " +
            "higher levels\n\n" +

        "Gift per level : the amount of gifts Santa brings in to each level, the level ends when the quantity " +
            "of gifts runs out\n\n" +

        "Charge : the maximum quantity of gifts or coal that can exist on screen at once\n\n" +

        "Throw speed : affects the time needed for a gift or coal to reach destination",

        "General advices :\n" +
        "To avoid wasting gifts in a level, start with throwing some coal to get a grisp of the time " +
            "needed for a projectile to reach its destination!\n\n" +

        "The content of a node is randomly generated and varies with each game run. Do not hesitate to challenge " +
            "the game many times in order to reach a higher final score!\n\n" +

        "Shop nodes each offer random upgrades. It's sometimes necessary to visit many shops to get " +
            "the wanted upgrade!\n\n" +

        "Have fun!!\n\n" +

        "Press \"I\" to toggle the instructions page"
    };


    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text
            = instructions[instructionsPage];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameObject.GetComponent<InstructionsText>().nextPage();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            gameObject.GetComponent<InstructionsText>().lastPage();
        }
    }

    public void nextPage() {
        instructionsPage++;
        if (instructionsPage == instructions.Length) instructionsPage--;

        gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text
            = instructions[instructionsPage];
    }

    public void lastPage() {
        instructionsPage--;
        if (instructionsPage < 0) instructionsPage++;

        gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Text>().text
            = instructions[instructionsPage];
    }

    public bool isLastPage() {
        return instructions.Length - 1 == instructionsPage;
    }
}
