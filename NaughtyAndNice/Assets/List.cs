using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
    public List<RuntimeAnimatorController> naughtyChildrenSprites;
    public List<RuntimeAnimatorController> niceChildrenSprites;
    public GameObject childObject;
    public List<GameObject> shownChildren;
    public Sprite openSprite;
    public Sprite closedSprite;

    // Start is called before the first frame update
    void Start()
    {
        show();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show() {
        gameObject.transform.position = new Vector3(0, 0, 0);
        gameObject.transform.rotation = Quaternion.identity;
        gameObject.transform.localScale = new Vector3(14, 14, 1);
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
        gameObject.GetComponent<SpriteRenderer>().sprite = openSprite;
        float posY = 8f;
        float posX = -5f;
        foreach(RuntimeAnimatorController s in niceChildrenSprites) {
            GameObject newChild = Instantiate(childObject);
            newChild.transform.localScale = new Vector3(2, 2, 1);
            newChild.transform.position = new Vector3(posX, posY, 0);
            newChild.GetComponent<child>().moving = false;
            newChild.GetComponent<SpriteRenderer>().sortingOrder = 7;
            newChild.GetComponent<Animator>().runtimeAnimatorController = s;
            posX += 5;
            shownChildren.Add(newChild);
        }
        posY = -5f;
        posX = -5f;
        foreach (RuntimeAnimatorController s in naughtyChildrenSprites)
        {
            GameObject newChild = Instantiate(childObject);
            newChild.transform.localScale = new Vector3(2, 2, 1);
            newChild.transform.position = new Vector3(posX, posY, 0);
            newChild.GetComponent<child>().moving = false;
            newChild.GetComponent<SpriteRenderer>().sortingOrder = 7;
            newChild.GetComponent<Animator>().runtimeAnimatorController = s;
            posX += 5;
            shownChildren.Add(newChild);
        }

    }

    public void hide() {
        gameObject.transform.position = new Vector3(0, 5, 0);
        gameObject.transform.rotation = new Quaternion(0,0,1,0);
        gameObject.transform.localScale = new Vector3(12, 12, 1);

        gameObject.GetComponent<SpriteRenderer>().sprite = closedSprite;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        for (int i = shownChildren.Count - 1; i >= 0; i--) {
            GameObject remove = shownChildren[i];
            shownChildren.RemoveAt(i);
            Destroy(remove);
        }
    }
}
