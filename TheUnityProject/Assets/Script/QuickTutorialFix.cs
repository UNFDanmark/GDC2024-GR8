using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuickTutorialFix : MonoBehaviour
{
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 10 || Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("!Main Scene");
        }
    }
}
