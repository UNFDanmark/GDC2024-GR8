using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    public int[] topScores;
    public string[] topNames;
    
    public string inputName;
    private int latestPosition;

    private PlayerHealth ph;
    private bool dieBool, testbool;
    public Score sc;

    public int topWhat = 10;

    public void RecieveRecordName(string name)
    {
        topNames[latestPosition] = name;
        SaveData();
    }
    public void NewHighScore()
    {
    }
    public void SaveData()
    {
       Debug.Log("Called save data");
       string scoreString = "";
        for (int i = 0; i < topWhat; i++)
        {
            if (scoreString.Length == 0)
            {
                scoreString += topScores[i];
                continue;
            }

            scoreString += ", " + topScores[i];

            // PlayerPrefs.SetInt("#"+(i+1),topScores[i]);
            // PlayerPrefs.SetString("#"+(i+1),topNames[i]);

        }
        
        File.WriteAllText("/Assets/Script/scoresfolder/topscore.txt",scoreString);
      
    }

    public void ClearData()
    {
        for (int i = 0; i < topWhat; i++)
        {
            topScores[i] = 0;
            topNames[i] = "";
            SaveData();
        }
    }

    public void LoadData()
    {
        string scoreStringWithCommas = File.ReadAllText("/Assets/Script/scoresfolder/topscore.txt");
        string[] stringScores = scoreStringWithCommas.Split(",");

        for (int i = 0; i < stringScores.Length; i++)
        {
            topScores[i] = int.Parse(stringScores[i]);
        }
        
        topScores = new int[topWhat];
        topNames = new string[topWhat];
        for (int i = 0; i<topWhat; i++)
        {
            int score = PlayerPrefs.GetInt("#" + (i + 1), 0);
            string name = PlayerPrefs.GetString("#" + (i + 1), "N/A");
            topScores[i] = score;
            topNames[i] = name;
        }
    }
    void Start()
    {
        dieBool = true;
        LoadData();
        ph = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)) ClearData();
        if (Input.GetKeyDown(KeyCode.I)) testbool = true;
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < topWhat; i++)
            {
                Debug.Log($"#{i+1}:{topScores[i]}");
            }
        }
        if ((ph.playerDied && dieBool)|| testbool)
        {
            dieBool = false; testbool = false;
            int tempScore = (int)sc.score;
            
            for (int i = topWhat-1; i > 0; i--)
            {
                if (tempScore > topScores[i] && tempScore < topScores[i-1])
                {
                    MoveScoreDown(i);
                    topScores[i] = tempScore;
                    NewHighScore();
                    latestPosition = i;
                }

                if (tempScore > topScores[0])
                {
                    MoveScoreDown(0);
                    topScores[0] = tempScore;
                    NewHighScore();
                    latestPosition = 0;
                }
            }
            SaveData();
        }
    }

    void MoveScoreDown(int index)
    {
        for (int i = topWhat-1; i > index; i--)
        {
            topScores[i] = topScores[i - 1];
            topNames[i] = topNames[i - 1];
        }
    }
}
