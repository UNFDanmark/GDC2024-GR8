using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathInputStuff : MonoBehaviour
{
    public Leaderboards leaderboard;
    public LeaderbordShow lbs;
    private TMP_InputField inputField;
    private GameObject inputFieldObject;
    private bool enteredName = false;
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
    }

    void Update()
    {
        if (leaderboard.newHighScore)
        {
            inputFieldObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return) && enteredName)
            {
                SceneManager.LoadScene("Main Menu");
            }
            if (Input.GetKeyDown(KeyCode.Return) && !enteredName)
            {
                enteredName = true;
                print(inputField.text);
            }
        }
        else
        {
            inputFieldObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
        
    }
    
    public void OnTextChange(string text)
    {
        if (text.EndsWith("\n"))
        {
            //InputText(text.Remove(text.Length - 1));
            inputField.text.Remove(text.Length - 1);
        }
    }
}
