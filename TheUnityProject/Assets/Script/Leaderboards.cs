using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Leaderboards : MonoBehaviour
{
    public int[] topScores;
    
    private int latestPosition;

    public PlayerHealth ph;
    private bool dieBool;
    public LeaderbordShow lbs;
    public Score sc;

    public int topWhat = 10;
    public bool newHighScore = false;

    public void SaveData()
    {
        string scoreString = "";
        for (int i = 0; i < topWhat; i++)
        {
            if (scoreString.Length == 0)
            {
                scoreString += topScores[i];
                continue;
            }

            scoreString += ", " + topScores[i];

        }
        File.WriteAllText("./Assets/Script/scoresFolder/topscore.txt",scoreString);

    }

        

    public void ClearData()
    {
        for (int i = 0; i < topWhat; i++)
        {
            topScores[i] = 0;
            SaveData();
        }
    }

    public void LoadData()
    {
        topScores = new int[topWhat];
        
        string scoreStringWithCommas = File.ReadAllText("./Assets/Script/scoresFolder/topscore.txt");
        string[] stringScores = scoreStringWithCommas.Split(",");

        for (int i = 0; i < topWhat; i++)
        {
            topScores[i] = int.Parse(stringScores[i]);
        }
    }
    void Start()
    {
        lbs.show = false;
        newHighScore = false;
        dieBool = true;
        LoadData();
        //ph = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
       
        /*
        if(Input.GetKeyDown(KeyCode.P)) ClearData();
        if (Input.GetKeyDown(KeyCode.I)) testbool = true;
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < topWhat; i++)
            {
                Debug.Log($"#{i+1}:{topScores[i]}");
            }
        }
        */
        if (ph.playerDied && dieBool)
        {
            dieBool = false;
            int tempScore = (int)sc.score;
            
            for (int i = topWhat-1; i > 0; i--)
            {
                if (tempScore > topScores[i] && tempScore < topScores[i-1])
                {
                    MoveScoreDown(i);
                    topScores[i] = tempScore;
                    newHighScore = true;
                    latestPosition = i;
                }

                if (tempScore > topScores[0])
                {
                    MoveScoreDown(0);
                    topScores[0] = tempScore;
                    newHighScore = true;
                    latestPosition = 0;
                }
            }
            SaveData();
            lbs.show = true;
            
        }
        if(!ph.playerDied) dieBool = true;
        
        if (ph.playerDied && Input.GetKeyDown(KeyCode.Return))
        {
            print("reload");
            SceneManager.LoadScene("!Main Scene");
        }
    }

    void MoveScoreDown(int index)
    {
        for (int i = topWhat-1; i > index; i--)
        {
            topScores[i] = topScores[i - 1];
        }
    }
}
