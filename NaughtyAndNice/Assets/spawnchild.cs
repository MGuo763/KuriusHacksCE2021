using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnchild : MonoBehaviour
{
    
    //array of all children sprites
    public RuntimeAnimatorController[] children;
    public List<Sprite> happyChildrenSprites;
    public List<Sprite> sadChildrenSprites;
    //keeps track of whether child has already been chosen 
    public List<int> niceChildrenSpritesIndexes;
    public List<int> naughtyChildrenSpritesIndexes;
    //amount of children spawned in the room
    public int childAmount = 2;
    public int naughtyChildAmount = 0;
    public int pointsNeeded;
    //list of children 
    public List<GameObject> childList; 
    public GameObject childObject; 
    public GameObject roomObject;
    public GameObject list;


    // Start is called before the first frame update
    void Start()
    {
        childList = new List<GameObject>();
        niceChildrenSpritesIndexes = new List<int>();
        naughtyChildrenSpritesIndexes = new List<int>();
        bool[] chosen = new bool[children.Length];
        
        int index;
        for (int i = 0; i < children.Length-3; i++) {
            do
            {
                index = (int)(Random.Range(0, children.Length));
            } while (chosen[index]);
            chosen[index] = true;

            if (Random.Range(0, 2) == 0)
            {
                niceChildrenSpritesIndexes.Add(index);
            }
            else {
                naughtyChildrenSpritesIndexes.Add(index);
            }
        }

        // At least two nice child sprite
        do
        {
            index = (int)(Random.Range(0, children.Length));
        } while (chosen[index]);
        chosen[index] = true;

        niceChildrenSpritesIndexes.Add(index);

        do
        {
            index = (int)(Random.Range(0, children.Length));
        } while (chosen[index]);
        chosen[index] = true;

        niceChildrenSpritesIndexes.Add(index);

        // At least one naughty child sprite
        do
        {
            index = (int)(Random.Range(0, children.Length));
        } while (chosen[index]);
        chosen[index] = true;

        naughtyChildrenSpritesIndexes.Add(index);

        spawnChildren(childAmount, naughtyChildAmount, false);

        List<RuntimeAnimatorController> niceChildrenSprites = new List<RuntimeAnimatorController>();
        List<RuntimeAnimatorController> naughtyChildrenSprites = new List<RuntimeAnimatorController>();
        foreach (int i in niceChildrenSpritesIndexes) {
            niceChildrenSprites.Add(children[i]);
        }
        foreach (int i in naughtyChildrenSpritesIndexes)
        {
            naughtyChildrenSprites.Add(children[i]);
        }
        list.GetComponent<List>().niceChildrenSprites = niceChildrenSprites;
        list.GetComponent<List>().naughtyChildrenSprites = naughtyChildrenSprites;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleChildren() {
        foreach(GameObject child in childList) {
            child.GetComponent<child>().moving = !child.GetComponent<child>().moving;
        }
    }

    public void spawnChildren(int niceChildrenNb, int naughtyChildrenNb, bool moving) {
        // Generate children

        float posX = roomObject.transform.position.x;
        float posY = roomObject.transform.position.y;
        float sizeX = (roomObject.transform.localScale.x) / 2;
        float sizeY = (roomObject.transform.localScale.y) / 2;

        for (int i = 0; i < childAmount + naughtyChildAmount; i++)
        {

            float x = Random.Range(posX - sizeX, posX + sizeX);
            float y = Random.Range(posY - sizeY, posY + sizeY);

            GameObject newChild = Instantiate(childObject);
            newChild.transform.position = new Vector3(x, y, 0);
            int childSpriteIndex;
            // Choose sprite according to tag 
            if (i < naughtyChildAmount)
            {
                newChild.tag = "naughty";
                childSpriteIndex = naughtyChildrenSpritesIndexes[Random.Range(0, naughtyChildrenSpritesIndexes.Count)];
                newChild.GetComponent<Animator>().runtimeAnimatorController =
                    children[childSpriteIndex];
                newChild.GetComponent<child>().destroySprite = sadChildrenSprites[childSpriteIndex];
                newChild.GetComponent<child>().moving = moving;
                newChild.GetComponent<SpriteRenderer>().sortingOrder = 4;

                // Must put naughty children first so that they steal the gifts of nice children
                childList.Add(newChild);
            }
            else
            {
                newChild.tag = "nice";
                childSpriteIndex = niceChildrenSpritesIndexes[Random.Range(0, niceChildrenSpritesIndexes.Count)];

                newChild.GetComponent<Animator>().runtimeAnimatorController =
                    children[childSpriteIndex];
                newChild.GetComponent<child>().destroySprite = happyChildrenSprites[childSpriteIndex];
                newChild.GetComponent<child>().pointsNeeded = pointsNeeded;
                newChild.GetComponent<child>().moving = moving;
                childList.Add(newChild);
            }
        }
    }
}
