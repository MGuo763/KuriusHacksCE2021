using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class shophandler : MonoBehaviour
{
    //total list of items 
    public string[] list; 
    //total list of sprites
    public Sprite[] itemSprites; 
    //has been spawned 
    public bool[] spawned; 
    //amount of items in the shop
    public int amitem; 
    //list of items currently in the store
    public string[] inShop; 
    //effect of items in shopw
    public int[] effect; 
    //amount of items player currently has (static because it must be accessible across all scenes)
    public static string[] myItems; 
    // Start is called before the first frame update
    void Start()
    {
        spawned = new bool[list.Length]; 
        inShop = new string[amitem]; 
        effect = new int[amitem]; 
        for(int i = 0; i<amitem; i++) {
            int index = (int)(Random.Range(0, list.Length)); 
            while (spawned[index] == true) {
                index = (int)(Random.Range(0, list.Length)); 
            }
            spawned[index] = true; 
            int e = (int)(Random.Range(1, 4)); 
            inShop[i] = list[index]; 
            effect[i] = e; 
            string name = "item" + i.ToString(); 
            GameObject item = GameObject.Find(name); 
            SpriteRenderer spr = item.GetComponent<SpriteRenderer>(); 
            spr.sprite = itemSprites[index]; 
        }

        for(int i = 0; i<amitem; i++) {
            string name = i.ToString() + "descript"; 
            GameObject text = GameObject.Find(name);
            int e = 0; 
            int cost = 0; 
            string des = i.ToString() + ": "; 
            switch(inShop[i]) {
                case "maxBullets":
                    e = effect[i]; 
                    cost = 40 + effect[i]*10; 
                    des += "increases the maximum amount of bullets by " + e.ToString() + ". Cost: " + cost.ToString(); 
                    text.GetComponent<UnityEngine.UI.Text>().text = des;
                    break;
                case "throwSpeed":
                    e = effect[i]; 
                    cost = 10 + effect[i]*10; 
                    des += "increases throwing speed by " + e.ToString() + ". Cost: " + cost.ToString(); 
                    text.GetComponent<UnityEngine.UI.Text>().text = des;
                    break;
                case "giftPower":
                    e = effect[i] * 5; 
                    cost = 40 + effect[i]*10; 
                    des += "increases gift power by " + e.ToString() + ". Cost: " + cost.ToString(); 
                    text.GetComponent<UnityEngine.UI.Text>().text = des;
                    break;
                case "giftPerLevel":
                    e = effect[i] * 5; 
                    cost = 40 + effect[i]*10; 
                    des += "increases gifts per level by " + e.ToString() + ". Cost: " + cost.ToString(); 
                    text.GetComponent<UnityEngine.UI.Text>().text = des;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject p = GameObject.Find("points"); 
        p.GetComponent<UnityEngine.UI.Text>().text = "Santa Points: " + Santa.points.ToString(); 
        GameObject mb = GameObject.Find("maxBullets"); 
        mb.GetComponent<UnityEngine.UI.Text>().text = "Max Bullets: " + Santa.maxBullets.ToString();
        GameObject ts = GameObject.Find("throwSpeed"); 
        ts.GetComponent<UnityEngine.UI.Text>().text = "Throw Speed: " + Santa.throwSpeed.ToString();
        GameObject gp = GameObject.Find("giftPower"); 
        gp.GetComponent<UnityEngine.UI.Text>().text = "Gift Power: " + Santa.giftPower.ToString();
        GameObject gpl = GameObject.Find("giftPerLevel"); 
        gpl.GetComponent<UnityEngine.UI.Text>().text = "Gift Per Level: " + Santa.giftPerLevel.ToString();
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            int e = 0; 
            int cost = 0; 
            switch(inShop[0]) {
                case "maxBullets":
                    e = effect[0]; 
                     cost = 40 + effect[0]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.maxBullets += e; 
                    }
                    break;
                case "throwSpeed":
                     e = effect[0]; 
                     cost = 10 + effect[0]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.throwSpeed += e; 
                    }
                    break;
                case "giftPower":
                     e = effect[0] * 5; 
                     cost = 40 + effect[0]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPower += e; 
                    }
                    break;
                case "giftPerLevel":
                     e = effect[0] * 5; 
                     cost = 40 + effect[0]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPerLevel += e; 
                    }
                    break;
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha1)) {
            int e = 0; 
            int cost = 0; 
            switch(inShop[1]) {
                case "maxBullets":
                     e = effect[1]; 
                     cost = 40 + effect[1]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.maxBullets += e; 
                    }
                    break;
                case "throwSpeed":
                     e = effect[1]; 
                     cost = 10 + effect[1]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.throwSpeed += e; 
                    }
                    break;
                case "giftPower":
                     e = effect[1] * 5; 
                     cost = 40 + effect[1]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPower += e; 
                    }
                    break;
                case "giftPerLevel":
                     e = effect[1] * 5; 
                     cost = 40 + effect[1]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPerLevel += e; 
                    }
                    break;
            }
        } else if (Input.GetKeyDown(KeyCode.Alpha2)){
            int e = 0; 
            int cost = 0; 
            switch(inShop[2]) {
                case "maxBullets":
                     e = effect[2]; 
                     cost = 40 + effect[2]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.maxBullets += e; 
                    }
                    break;
                case "throwSpeed":
                     e = effect[2]; 
                     cost = 10 + effect[2]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.throwSpeed += e; 
                    }
                    break;
                case "giftPower":
                     e = effect[2] * 5; 
                     cost = 40 + effect[2]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPower += e; 
                    }
                    break;
                case "giftPerLevel":
                     e = effect[2] * 5; 
                     cost = 40 + effect[2]*10; 
                    if (Santa.points >= cost) {
                        Santa.points -= cost; 
                        Santa.giftPerLevel += e; 
                    }
                    break; 
            }
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            SceneManager.LoadScene("map"); 
        }
    }
}
