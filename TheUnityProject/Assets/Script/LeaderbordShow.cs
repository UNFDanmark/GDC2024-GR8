using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LeaderbordShow : MonoBehaviour
{
    // Start is called before the first frame update
    public Leaderboards leaderboardScript;
    private TMP_Text textThing;
    private string textToShow;
    public bool show;
    void Start()
    {
        
        textThing = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if (show)
        {
            textThing.text = "";
            for (int i = 0; i < leaderboardScript.topWhat; i++)
            {
                //textToShow += leaderboardScript.topNames[i];
                textToShow += $"#{1+i} ";
                textToShow += leaderboardScript.topScores[i];
                textToShow += "\n";
            }
            textThing.text = textToShow;
            show = false;
        }
        
        
    }
}
