using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class child : MonoBehaviour
{
    //points needed for child to be satisfied
    public float pointsNeeded; 
    public float speedX;
    public float speedY;
    public GameObject room; 
    public float speedMult;
    public bool moving;
    public int childWidth = 8;
    public int childHeight = 15;
    public const float minSpeed = 2f;
    public const float maxSpeed = 10f;
    public Sprite destroySprite;
    public bool destroyed = false;
    public float destroyTimer = 1f;


    // Start is called before the first frame update
    void Start()
    {
        int dirX = (Random.Range(0, 2)); 
        int dirY = (Random.Range(0, 2));
        dirX = dirX == 0 ? -1 : 1;
        dirY = dirY == 0 ? -1 : 1;
        if (gameObject.tag == "naughty")
        {
            speedMult = 1f;
        }
        else
        {
            speedMult = .5f;
        }
        speedX = Random.Range(minSpeed, maxSpeed) * speedMult * dirX;
        speedY = Random.Range(minSpeed, maxSpeed) * speedMult * dirY;
    }

    // Update is called once per frame
    void Update()
    {
        if (!destroyed)
        {
            float x = gameObject.transform.position.x;
            float y = gameObject.transform.position.y;

            float posX = room.transform.position.x;
            float posY = room.transform.position.y;
            float sizeX = (room.transform.localScale.x) / 2;
            float sizeY = (room.transform.localScale.y) / 2;

            //Debug.Log(sizeX + " " + sizeY); 

            //change dir/speed if child is at a wall
            if (x <= posX - sizeX + childWidth / 2)
            {
                speedX = Random.Range(minSpeed, maxSpeed) * speedMult;

            }
            else if (x >= posX + sizeX - childWidth / 2)
            {
                speedX = -Random.Range(minSpeed, maxSpeed) * speedMult;
            }

            if (y <= posY - sizeY + childHeight / 2)
            {
                speedY = Random.Range(minSpeed, maxSpeed) * speedMult;
            }
            else if (y >= posY + sizeY - childHeight / 2)
            {
                speedY = -Random.Range(minSpeed, maxSpeed) * speedMult;
            }

            if (moving)
                gameObject.GetComponent<Transform>().position =
                    new Vector3(speedX * Time.deltaTime + x, speedY * Time.deltaTime + y, 0);
        }
        else {
            destroyTimer -= Time.deltaTime;
            if (destroyTimer < 0) {
                Destroy(gameObject);
            }
        }
    }

    public void satisfy() {
        destroyed = true;
        gameObject.GetComponent<Animator>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = destroySprite;
        moving = false;
    }

    /*void OnCollisionEnter2D(Collision2D col) {
        if (gameObject.tag == "naughty") {

        } else if (gameObject.tag == "nice") {

        }
        if (col.gameObject.tag == "naughty" || col.gameObject.tag == "nice") {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }*/
}
