using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroyscript : MonoBehaviour
{
    public GameObject mapgen; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake() {
        DontDestroyOnLoad(mapgen); 
    }
}
