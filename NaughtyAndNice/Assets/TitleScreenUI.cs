using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleScreenUI : MonoBehaviour
{
    public GameObject titleObject;
    public GameObject instructionsObject;
    public bool instructionsShown;
    
    // Start is called before the first frame update
    void Start()
    {
        titleObject = Instantiate(titleObject);
        instructionsShown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!instructionsShown)
            {
                Destroy(titleObject);
                instructionsObject = Instantiate(instructionsObject);
                instructionsShown = true;
            }
        }        

        if (Input.GetKeyDown(KeyCode.I) && instructionsObject.GetComponent<InstructionsText>().isLastPage()) {
            Destroy(instructionsObject);
            SceneManager.LoadScene("map");
        }
    }
}
