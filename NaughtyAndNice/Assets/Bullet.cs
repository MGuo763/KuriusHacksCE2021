using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float size = 1;
    public bool moving;
    float initialSize;
    

    // Time needed for a bullet to hit, in seconds
    public const float bulletTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        initialSize = gameObject.GetComponent<Transform>().localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (size > 0 && moving) {
            size -= Time.deltaTime * Santa.throwSpeed / bulletTime;
            gameObject.GetComponent<Transform>().localScale = new Vector3(size * initialSize, size * initialSize, 1);
        }
    }
}
