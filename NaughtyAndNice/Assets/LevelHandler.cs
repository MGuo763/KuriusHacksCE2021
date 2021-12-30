using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    // Time left for the level
    public float timer = 60;
    bool timerRunning = false;
    bool levelEnd = false;
    List<GameObject> bullets;
    bool bulletType;    // true: gift; false: coal
    int niceChildSatisfied;
    int naughtyChildSatisfied;
    int giftRemain;
    int niceChildNumber;
    int naughtyChildNumber;
    int childRemain;
    GameObject selectCoalObject;
    GameObject selectGiftObject;
    public GameObject giftObject;
    public List<Sprite> giftSprites;
    public GameObject coalObject;    
    public GameObject spawnChildObject;
    public GameObject levelUIObject;
    public GameObject listObject;
    public GameObject santaObject;
    public GameObject pointerObject;
    public GameObject levelScoringUIObject;
    public float childWidth = 2;
    public float childHeight = 4;
    public int giftPointNeed = 5;
    public int levelLayer;
    public static bool highScoreMode = false;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletType = false;
        levelEnd = false;
        levelLayer = Santa.depth;
        bullets = new List<GameObject>();
        giftRemain = Santa.giftPerLevel;
        niceChildSatisfied = naughtyChildSatisfied = 0;
        if (levelLayer < 6)
        {
            spawnChildObject.GetComponent<spawnchild>().childAmount = niceChildNumber = childRemain
                = 5 + Random.Range(0, levelLayer);
            int naughtyChildNumber = Random.Range(1, levelLayer);
            childRemain += naughtyChildNumber;
            spawnChildObject.GetComponent<spawnchild>().naughtyChildAmount = naughtyChildNumber;
            spawnChildObject = Instantiate(spawnChildObject);
            giftPointNeed = 5 * (1 + (int)Mathf.Ceil(levelLayer / 2f) + Random.Range(-1, 1));
        }
        else {
            spawnChildObject.GetComponent<spawnchild>().childAmount = niceChildNumber = childRemain
                = 7;
            naughtyChildNumber = 3;
            childRemain += naughtyChildNumber;
            spawnChildObject.GetComponent<spawnchild>().naughtyChildAmount = naughtyChildNumber;
            spawnChildObject = Instantiate(spawnChildObject);
            giftPointNeed = 25;
        }


        levelUIObject = Instantiate(levelUIObject);
        levelUIObject.GetComponent<LevelUI>().timeRemain = (int)timer;
        levelUIObject.GetComponent<LevelUI>().giftRemain = giftRemain;
        levelUIObject.GetComponent<LevelUI>().pointsNeeded = giftPointNeed;
        listObject = Instantiate(listObject);
        spawnChildObject.GetComponent<spawnchild>().list = listObject;
        spawnChildObject.GetComponent<spawnchild>().pointsNeeded = giftPointNeed;
        pointerObject = Instantiate(pointerObject);
        selectCoalObject = Instantiate(coalObject);
        selectCoalObject.transform.position = new Vector3(-9, -16, 0);
        selectCoalObject.transform.localScale = new Vector3(2, 2, 1);
        selectCoalObject.GetComponent<SpriteRenderer>().color = Color.red;
        selectCoalObject.GetComponent<Coal>().moving = false;
        selectGiftObject = Instantiate(giftObject);
        selectGiftObject.transform.position = new Vector3(9, -16, 0);
        selectGiftObject.transform.localScale = new Vector3(2, 2, 1);
        selectGiftObject.GetComponent<Gift>().moving = false;
        santaObject = Instantiate(santaObject);
    }
   
    // Update is called once per frame
    void Update()
    {
        if (levelEnd) {

            if (Input.GetKeyDown(KeyCode.Return)) {
                if (levelLayer >= 6)
                    SceneManager.LoadScene("end");
                else
                    SceneManager.LoadScene("map");
            }
        }
        else
        {
            // Level timer
            if (timerRunning)
            {
                timer -= Time.deltaTime;
                levelUIObject.GetComponent<LevelUI>().timeRemain = (int)timer;
            }

            if (timer < 0 ||
                childRemain == 0 && levelLayer < 6 ||
                giftRemain == 0 && bullets.Count == 0)
            {
                Debug.Log("Level cleared");
                levelEnd = true;
                timerRunning = false;
                Destroy(selectCoalObject);
                Destroy(selectGiftObject);
                for (int i = bullets.Count - 1; i >= 0; i--)
                {
                    GameObject remove = bullets[i];
                    bullets.RemoveAt(i);
                    Destroy(remove);
                }
                Destroy(spawnChildObject);
                Destroy(levelUIObject);
                Destroy(pointerObject);
                Destroy(listObject);
                levelScoringUIObject = Instantiate(levelScoringUIObject);
                levelScoringUIObject.GetComponentInChildren<Text>().GetComponent<LevelScoringUI>().niceChildrenSatisfied = niceChildSatisfied;
                levelScoringUIObject.GetComponentInChildren<Text>().GetComponent<LevelScoringUI>().naughtyChildrenChased = naughtyChildSatisfied;
                levelScoringUIObject.GetComponentInChildren<Text>().GetComponent<LevelScoringUI>().niceChildrenMult = giftPointNeed;
                levelScoringUIObject.GetComponentInChildren<Text>().GetComponent<LevelScoringUI>().allChildrenCleared = childRemain == 0;
                levelScoringUIObject.GetComponentInChildren<Text>().GetComponent<LevelScoringUI>().finalLevel = levelLayer >= 6;

            }
            else if (childRemain == 0 && levelLayer >= 6)
            {
                spawnChildObject.GetComponent<spawnchild>().spawnChildren(niceChildNumber, naughtyChildNumber, true);
                childRemain = niceChildNumber + naughtyChildNumber;
            }
            

            // Bullets collisions when bullet timer reaches 0

            // Indexes of bullets that reached 0
            LinkedList<int> destroyIndexes = new LinkedList<int>();

            // Indexes of satisfied nice children or naughty children with coal
            LinkedList<int> destroyIndexesChild = new LinkedList<int>();


            for (int i = 0; i < bullets.Count; i++)
            {

                // Reached 0
                if (bullets[i].GetComponent<Bullet>().size < 0)
                {
                    Bullet bullet = bullets[i].GetComponent<Bullet>();
                    Vector3 bulletPos = bullet.transform.position;
                    Debug.Log("BulletPos" + bulletPos.x + ", " + bulletPos.y);

                    // Check for all children
                    for (int j = 0; j < spawnChildObject.GetComponent<spawnchild>().childList.Count; j++)
                    {
                        GameObject child = spawnChildObject.GetComponent<spawnchild>().childList[j];
                        Vector3 childPos = child.transform.position;
                        Debug.Log("childPos" + childPos.x + ", " + childPos.y);
                        // If hit
                        if (bulletPos.x > childPos.x - childWidth / 2f && bulletPos.x < childPos.x + childWidth / 2f
                            && bulletPos.y > childPos.y - childHeight / 2f && bulletPos.y < childPos.y + childHeight / 2f)
                        {
                            Debug.Log("hit");
                            if (child.tag.Equals("naughty") && bullet is Coal)
                            {
                                // Naughty and got coal, child go away
                                Debug.Log("hit naughty");
                                destroyIndexesChild.AddFirst(j);
                                naughtyChildSatisfied++;
                                levelUIObject.GetComponent<LevelUI>().currentPoints += 5;
                            }
                            else if (child.tag.Equals("nice") && bullet is Gift)
                            {
                                // Nice children got gift, update points
                                Debug.Log("hit nice");
                                child.GetComponent<child>().pointsNeeded -= Santa.giftPower;

                                // Nice children satisfied
                                if (child.GetComponent<child>().pointsNeeded <= 0)
                                {
                                    destroyIndexesChild.AddFirst(j);
                                    niceChildSatisfied++;
                                    levelUIObject.GetComponent<LevelUI>().currentPoints += giftPointNeed;
                                }
                            }

                            // A bullet hits once
                            break;
                        }
                    }

                    destroyIndexes.AddFirst(i);
                }
            }

            foreach (int destroyIndex in destroyIndexes)
            {
                Debug.Log("destroying bullet");
                GameObject remove = bullets[destroyIndex];
                bullets.RemoveAt(destroyIndex);
                Destroy(remove);
            }

            foreach (int destroyIndexChild in destroyIndexesChild)
            {
                GameObject remove = spawnChildObject.GetComponent<spawnchild>().childList[destroyIndexChild];
                spawnChildObject.GetComponent<spawnchild>().childList.RemoveAt(destroyIndexChild);
                remove.GetComponent<child>().satisfy();
                childRemain--;
            }

            if (Input.GetKeyDown("space"))
            {
                bulletType = !bulletType;
                if (bulletType)
                {
                    selectGiftObject.GetComponent<SpriteRenderer>().color = Color.red;
                    selectCoalObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else
                {
                    selectGiftObject.GetComponent<SpriteRenderer>().color = Color.white;
                    selectCoalObject.GetComponent<SpriteRenderer>().color = Color.red;

                }
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                // Show the list
                timerRunning = !timerRunning;
                spawnChildObject.GetComponent<spawnchild>().toggleChildren();
                foreach (GameObject b in bullets)
                {
                    b.GetComponent<Bullet>().moving = !b.GetComponent<Bullet>().moving;
                }
                if (timerRunning)
                {
                    listObject.GetComponent<List>().hide();
                    levelUIObject.GetComponent<LevelUI>().showGiftNumber = true;
                }
                else
                {
                    listObject.GetComponent<List>().show();
                    levelUIObject.GetComponent<LevelUI>().showGiftNumber = false;
                }

            }
        }

    }

    private void OnMouseDown()
    {
        Debug.Log("mouseDown");
        if (bullets.Count < Santa.maxBullets && giftRemain > 0 && timerRunning) {
            Vector3 bulletPosition = Input.mousePosition;
            bulletPosition = Camera.main.ScreenToWorldPoint(bulletPosition);
            bulletPosition.z = 0;
            santaObject.GetComponent<SantaAnimation>().throwAnimation();
            
            if (bulletType)
            {
                // gift               
                GameObject newGift = Instantiate(giftObject, bulletPosition, Quaternion.identity);
                newGift.GetComponent<Gift>().moving = true;
                newGift.GetComponent<SpriteRenderer>().sprite = giftSprites[Random.Range(0, giftSprites.Count)];
                bullets.Add(newGift);
                giftRemain--;
                levelUIObject.GetComponent<LevelUI>().giftRemain = giftRemain;


            }
            else 
            {
                // coal
                GameObject newCoal = Instantiate(coalObject, bulletPosition, Quaternion.identity);
                newCoal.GetComponent<Coal>().moving = true;
                bullets.Add(newCoal);
            }
        }
    }

    private void OnMouseOver()
    {
        if (!levelEnd)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            mousePosition.z = 0;
            pointerObject.transform.position = mousePosition;
        }
    }

}
