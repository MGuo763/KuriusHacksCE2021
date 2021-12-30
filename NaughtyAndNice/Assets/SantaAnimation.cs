using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaAnimation : MonoBehaviour
{
    bool throwing = false;
    float throwTimer = 1f;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (throwing) {
            throwTimer -= Time.deltaTime;
            if (throwTimer < 0) {
                throwing = false;
                gameObject.GetComponent<Animator>().enabled = false;
            }
        }
    }

    public void throwAnimation(){
        throwing = true;
        throwTimer = 1f;
        gameObject.GetComponent<Animator>().enabled = true;
        gameObject.GetComponent<Animator>().Rebind();
        gameObject.GetComponent<Animator>().Update(0f);

    }
}
