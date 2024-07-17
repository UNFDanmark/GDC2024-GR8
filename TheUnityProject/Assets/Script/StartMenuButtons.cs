using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuButtons : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    

    public void StartGame()
    {
        SceneManager.LoadScene("!Main Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
