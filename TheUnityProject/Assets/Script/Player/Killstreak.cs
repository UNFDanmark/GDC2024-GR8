using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

public class Killstreak : MonoBehaviour
{
    public Slider killStreakSlider;
    public float timer;
    public float killTimer = 0;
    public float streakRunOutTime = 2f;
    public int killStreak = 0;
    float killStreakBarScale = 0;
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

        if(killTimer != 0) killStreakSlider.value = math.unlerp(streakRunOutTime, 0, timer-killTimer);

        killStreakUI.streak = killStreak;
    }
    public void Kill()
    {
        killStreak++;
        killTimer = timer;
    }
}
