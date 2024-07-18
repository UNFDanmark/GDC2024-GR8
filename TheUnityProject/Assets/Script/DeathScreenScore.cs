using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathScreenScore : MonoBehaviour
{
    private TMP_Text textComponent;
    public Score score;
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.text = "Your final score: " + score.score;
    }
}
