using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killstreak : MonoBehaviour
{
    private float timer;
    private float killTimer = 0;
    public float streakRunOutTime = 2f;
    public int killStreak = 0;
    KillStreakUI killStreakUI;
    
    void Start()
    {
        killStreakUI = GameObject.FindWithTag("StreakText").GetComponent<KillStreakUI>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (killTimer + streakRunOutTime < timer)
        {
            killStreak = 0;
        }

        killStreakUI.streak = killStreak;
    }
    public void Kill()
    {
        killStreak++;
        killTimer = timer;
    }
}
