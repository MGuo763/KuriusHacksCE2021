using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class mapgen : MonoBehaviour
{
    //number of levels
    public int numlevel = 7; 
    public static Node[] nodes; 
    public static int[,] mat; 
    public static int current; 
    public int numnodes = 15; 
    public int numshops = 1; 
    public int numresource = 3; 
    int LEVEL = 0; 
    int SHOP = 1; 
    int BOSS = 2; 
    int RESOURCE = 3; 
    int START = 4; 
    public GameObject camera; 
    public GameObject line; 
    public Sprite[] sprites; 
    static bool spawned = false; 
    public static bool[] beaten; 
    public bool instructions = false; 
    public GameObject instruct; 

    //what each key press does 
    int[] keyDo = new int[6]; 

    void resetKeyDo () {
        for(int i = 0; i<keyDo.Length; i++) {
            keyDo[i] = -1; 
        }
        int cRoad = 0; 
        for(int i = 0; i<numnodes; i++) {
            if (mat[current, i] == 1) {
                keyDo[cRoad] = i; 
                cRoad++; 
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start!"); 
        if (!spawned) {
            Debug.Log("spawned!"); 
        current = 0; 
        numnodes = 15; 
        mat = new int[numnodes, numnodes]; 
        nodes = new Node[numnodes]; 
        beaten = new bool[numnodes]; 

        
        nodes[0] = new Node(START); 
        int temp = 0; 
        GameObject sq = GameObject.Find(temp.ToString()); 
        sq.GetComponent<SpriteRenderer>().sprite = sprites[START]; 
        beaten[0] = true; 

        nodes[numnodes-1] = new Node(BOSS); 
        temp = numnodes-1; 
        sq = GameObject.Find(temp.ToString()); 
        sq.GetComponent<SpriteRenderer>().sprite = sprites[BOSS]; 

        for(int i = 1; i<numnodes-1; i++) {
            int lvl = 0; 
            if (i == 12) {
                lvl = SHOP; 
                nodes[i] = new Node(lvl); 
            } else {
                lvl = (int)(Random.Range(0, 4)); 
                bool sat = false;
                 while(!sat) {
                    if (lvl == RESOURCE) {
                        if(numresource == 0) {
                            lvl = (int)(Random.Range(0, 4)); 
                        } else {
                            numresource--; 
                            sat = true; 
                        }
                    } else if (lvl == SHOP) {
                        if (numshops == 0 || i == 1 || i == 2 || i == 3) {
                            lvl = (int)(Random.Range(0, 4)); 
                        } else {
                            numshops--; 
                            sat = true; 
                        }
                    } else if (lvl == BOSS) {
                        lvl = (int)(Random.Range(0, 4)); 
                    }
                    else {
                        sat = true; 
                    }
                }
                nodes[i] = new Node(lvl); 
            }
            
            sq = GameObject.Find(i.ToString()); 
            sq.GetComponent<SpriteRenderer>().sprite = sprites[lvl]; 
        }

        //map
        mat[0, 1] = 1; 
        mat[0, 2] = 1; 
        mat[0, 3] = 1; 
        // mat[3, 0] = 1; 
        // mat[2, 0] = 1; 
        // mat[1, 0] = 1; 

        // mat[1, 2] = 1; 
        // mat[2, 3] = 1; 
        // mat[2, 1] = 1; 
        // mat[3, 2] = 1; 

        mat[1, 4] = 1; 
        //mat[4, 1] = 1; 
        mat[2, 5] = 1; 
        //mat[5, 2] = 1; 
        mat[3, 5] = 1;
        //mat[5, 3] = 1; 

        // mat[4, 5] = 1; 
        // mat[5, 4] = 1; 

        mat[4, 6] = 1; 
        mat[4, 7] = 1; 
        //mat[6, 4] = 1; 
        //mat[7, 4] = 1; 
        mat[5, 8] = 1; 
        //mat[8, 5] = 1; 
        mat[5, 7] = 1; 
        //mat[7, 5] = 1; 

        // mat[6, 7] = 1; 
        // mat[7, 8] = 1; 
        // mat[8, 7] = 1; 
        // mat[7, 6] = 1; 

        mat[6, 9] = 1; 
        mat[7, 9] = 1; 
        // mat[8, 9] = 1; 
        //mat[9, 6] = 1; 
        //mat[9, 7] = 1; 
        // mat[9, 8] = 1; 

        mat[7, 10] = 1; 
        mat[8, 10] = 1; 
        //mat[10, 7] = 1; 
        //mat[10, 8] = 1; 

        // mat[9, 10] = 1; 
        // mat[10, 9] = 1; 

        mat[9, 11] = 1; 
        mat[9, 12] = 1;
        //mat[11, 9] = 1; 

        mat[10, 12] = 1; 
        mat[10, 13] = 1; 
        //mat[12, 10] = 1; 
        //mat[13, 10] = 1; 

        // mat[11, 12] = 1; 
        // mat[12, 11] = 1; 
        // mat[12, 13] = 1; 
        // mat[13, 12] = 1; 

        mat[11, 14] = 1; 
        mat[12, 14] = 1; 
        mat[13, 14] = 1; 
        // mat[14, 13] = 1;
        // mat[14, 12] = 1; 
        // mat[14, 11] = 1; 

        GameObject cur = GameObject.Find(current.ToString()); 
        this.resetKeyDo(); 
        this.clearText();
        cur.GetComponent<SpriteRenderer>().color = Color.white;

        //draws line between nodes
        for(int i = 0; i<numnodes; i++) {
            for(int j = 0; j<numnodes; j++) {
                if (mat[i, j] == 1) {
                    int sqnum = i; 
                    int sqnum2 = j; 
                    GameObject sq1 = GameObject.Find(sqnum.ToString()); 
                    GameObject sq2 = GameObject.Find(sqnum2.ToString()); 
                    Vector3 point1 = sq1.transform.position; 
                    Vector3 point2 = sq2.transform.position; 
                    Vector3[] positions = new Vector3[2] {point1, point2}; 
                    Instantiate(line); 
                    LineRenderer lr = line.GetComponent<LineRenderer>(); 
                    lr.SetWidth(1f, 1f); 
                    lr.positionCount = 2; 
                    lr.SetPositions(positions); 
                }   
            }
        }
        spawned = true; 
    } else {
        GameObject cur = GameObject.Find(current.ToString()); 
        this.resetKeyDo(); 
        this.clearText();
        cur.GetComponent<SpriteRenderer>().color = Color.white;

        //draws line between nodes
        for(int i = 0; i<numnodes; i++) {
            for(int j = 0; j<numnodes; j++) {
                if (mat[i, j] == 1) {
                    int sqnum = i; 
                    int sqnum2 = j; 
                    GameObject sq1 = GameObject.Find(sqnum.ToString()); 
                    GameObject sq2 = GameObject.Find(sqnum2.ToString()); 
                    Vector3 point1 = sq1.transform.position; 
                    Vector3 point2 = sq2.transform.position; 
                    Vector3[] positions = new Vector3[2] {point1, point2}; 
                    Instantiate(line); 
                    LineRenderer lr = line.GetComponent<LineRenderer>(); 
                    lr.SetWidth(1f, 1f); 
                    lr.positionCount = 2; 
                    lr.SetPositions(positions); 
                }   
            }
        }
        for(int i = 0; i<numnodes; i++) {
            GameObject sq = GameObject.Find(i.ToString()); 
            SpriteRenderer sr = sq.GetComponent<SpriteRenderer>(); 
            sr.sprite = sprites[nodes[i].type]; 
            if (beaten[i]) {
                sr.color = Color.white; 
            }
        }

        float x = cur.transform.position.x; 
        float y = cur.transform.position.y; 
        float z = -10f; 
        camera.transform.position = new Vector3(x, y, z); 
        cur.GetComponent<SpriteRenderer>().color = Color.white;
    }
    Debug.Log(current); 
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(current); 
        GameObject now = GameObject.Find(current.ToString()); 
        Debug.Log(now.name); 
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (nodes[current].type == LEVEL && beaten[current] == false) {
                beaten[current] = true;
                Santa.levelNodesVisited++; 
                SceneManager.LoadScene("level"); 
            } else if (nodes[current].type == RESOURCE) {
                GameObject resource = GameObject.Find("resource"); 
                GameObject t = GameObject.Find("Text"); 
                if (beaten[current] == false) {
                    int points = (int)(Random.Range(20, 51)); 
                    string[] awful = new string[3] {"You've gained ", points.ToString(), " Santa Points!\nPress Enter To Exit Screen"}; 
                    string textforresource = string.Concat(awful);
                    t.GetComponent<UnityEngine.UI.Text>().text =  textforresource; 
                    resource.GetComponent<Canvas>().sortingOrder = 3; 
                    Santa.points += points; 
                    Santa.pointsGotAtResources += points; 
                    Santa.resourceNodesVisited++; 
                    Santa.totalPoints += points; 
                    Debug.Log("santa points = "+Santa.points); 
                    beaten[current] = true;  
                } else {
                    resource.GetComponent<Canvas>().sortingOrder = 0; 
                }
            } else if (nodes[current].type == SHOP && beaten[current] == false) {
                beaten[current] = true; 
                SceneManager.LoadScene("shop");
                Santa.shopNodesVisited++; 
            } else if (nodes[current].type == BOSS && beaten[current] == false) {
                LevelHandler.highScoreMode = true; 
                beaten[current] = true;  
                SceneManager.LoadScene("level"); 
            }
        }

        if (beaten[current]) {
        if(Input.GetKeyDown(KeyCode.Alpha0)) {
            if (keyDo[0] != -1) {
                current = keyDo[0]; 
                this.resetKeyDo(); 
                this.clearText(); 

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white;
                Santa.depth++; 
            }
        } else if(Input.GetKeyDown(KeyCode.Alpha1)) {
            if (keyDo[1] != -1) {
                current = keyDo[1]; 
                this.resetKeyDo(); 
                this.clearText();

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white;
                Santa.depth++; 
            }
        } else if(Input.GetKeyDown(KeyCode.Alpha2)) {
            if (keyDo[2] != -1) {
                current = keyDo[2]; 
                this.resetKeyDo(); 
                this.clearText();

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white;
                Santa.depth++; 
            }
        } else if(Input.GetKeyDown(KeyCode.Alpha3)) {
            if (keyDo[3] != -1) {
                current = keyDo[3]; 
                this.resetKeyDo(); 
                this.clearText();

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white;
                Santa.depth++; 
            }
        } else if(Input.GetKeyDown(KeyCode.Alpha4)) {
            if (keyDo[4] != -1) {
                current = keyDo[4]; 
               this.resetKeyDo(); 
                this.clearText();

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white;
                Santa.depth++;
            }
        } else if(Input.GetKeyDown(KeyCode.Alpha5)) {
            if (keyDo[5] != -1) {
                current = keyDo[5]; 
                this.resetKeyDo(); 
                this.clearText();

                GameObject cur = GameObject.Find(current.ToString()); 
                float x = cur.transform.position.x; 
                float y = cur.transform.position.y; 
                float z = -10f; 
                camera.transform.position = new Vector3(x, y, z); 
                cur.GetComponent<SpriteRenderer>().color = Color.white; 
                Santa.depth++; 
            }
        
        }
        }
        if (Input.GetKeyDown(KeyCode.I)) {
            if (instructions) {
                instruct.GetComponent<Canvas>().sortingOrder = -1; 
                instructions = !instructions; 
            } else {
                instruct.GetComponent<Canvas>().sortingOrder = 3; 
                instructions = !instructions; 
            }
        }
        //here
        GameObject mb = GameObject.Find("maxBullets"); 
        mb.GetComponent<UnityEngine.UI.Text>().text = "charge: " + Santa.maxBullets.ToString(); 
        mb = GameObject.Find("throwSpeed"); 
        mb.GetComponent<UnityEngine.UI.Text>().text = "throw speed: " + Santa.throwSpeed.ToString(); 
        mb = GameObject.Find("giftPower"); 
        mb.GetComponent<UnityEngine.UI.Text>().text = "gift power: " + Santa.giftPower.ToString(); 
        mb = GameObject.Find("giftPerLevel"); 
        mb.GetComponent<UnityEngine.UI.Text>().text = "gift per level: " + Santa.giftPerLevel.ToString(); 
    }

    void clearText() {
        for(int i = 0; i<numnodes; i++) {
            string name = i.ToString() + "t"; 
            GameObject text = GameObject.Find(name); 
            text.GetComponent<UnityEngine.UI.Text>().text = ""; 
        }
        for(int i = 0; i<keyDo.Length; i++) {
            if (keyDo[i] != -1) {
                string name = keyDo[i].ToString()+"t"; 
                GameObject text = GameObject.Find(name); 
                text.GetComponent<UnityEngine.UI.Text>().text = i.ToString(); 
            }
        }
    }
}


public class Node {
    public int type;

    public Node(int type) {
        this.type = type; 
    }

}
