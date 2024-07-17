using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text textComponent;
    public Killstreak killstreak;
    public Transform playerTransform;

    public float score = 0;
    public float scoreMultiplier = 1;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreMultiplier = playerTransform.position.y * 0.25f * (1 + killstreak.killStreak * 0.1f)+.75f;
        textComponent.text = $"Score: {score:0000000}\n" +
                             $"Multiplier: {scoreMultiplier:F1}x";
    }
}
