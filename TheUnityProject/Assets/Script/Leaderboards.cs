using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaderboards : MonoBehaviour
{
    public int[] topScores;

    public string[] topNames;

    private PlayerHealth ph;
    public Score sc;

    public int topWhat = 10;
    // Start is called before the first frame update
    public void SaveData()
    {
        Debug.Log("Called save data");
        for (int i = 0; i < topWhat; i++)
        {
            Debug.Log("Made it to the for loop");
            PlayerPrefs.SetInt("#"+(i+1),topScores[i]);
            Debug.Log("Saved value #"+(i+1)+" to " + topScores[i]);
            PlayerPrefs.SetString("#"+(i+1),topNames[i]);
        }
    }

    public void LoadData()
    {
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
        LoadData();
        ph = gameObject.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            for (int i = 0; i < topWhat; i++)
            {
                Debug.Log($"#{i+1}:{topScores[i]}");
            }
        }
        if (ph.playerDied)
        {
            Debug.Log("PLAYER HAS DIED!!!!");
            int tempScore = (int)sc.score;
            for (int i = topWhat-1; i > 0; i--)
            {
                Debug.Log(i);
                if (tempScore > topScores[i] && tempScore < topScores[i-1])
                {
                    topScores[i] = tempScore;
                }

                if (tempScore > topScores[0]) topScores[0] = tempScore;
            }

            topScores[1] = 298428794;
            SaveData();
        }
    }
}
