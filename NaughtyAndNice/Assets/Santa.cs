using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santa : MonoBehaviour
{
    // Will contain static info on Santa such as number of remaining gifts, points accumulated
    // and different properties such as bullet speed, maximum bullet on screen, sight of vision (if blindness implemented)
    // etc.
    // Santa may also hold power-up items which could give more time during a level, 2x pts for a limited period of
    // time, etc (if enough time to implement)
    //
    public static int depth = 0;
    public static int maxBullets = 1;
    public static float throwSpeed = 1;
    public static int giftPower = 5;
    public static int giftPerLevel = 10;
    public static int points = 0;
    public static int totalPoints = 0;
    public static int resourceNodesVisited = 0;
    public static int pointsGotAtResources = 0;
    public static int shopNodesVisited = 0;    
    public static int levelNodesVisited = 0;
    public static int niceChildrenSatisfied = 0;
    public static int naughtyChildrenChased = 0;
    public static int finalScore = 0;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
