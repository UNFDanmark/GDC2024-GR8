using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killstreak : MonoBehaviour
{
    private float timer;
    private float killTimer = 0;
    public float streakRunOutTime = 2f;
    public int killStreak = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (killTimer + streakRunOutTime < timer)
        {
            killStreak = 0;
        }
    }
    public void Kill()
    {
        killStreak++;
        killTimer = timer;
    }
}