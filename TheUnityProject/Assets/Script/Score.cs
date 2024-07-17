using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMP_Text textComponent;

    public float score = 0;

    string displayScore;
    // Start is called before the first frame update
    void Start()
    {
        textComponent = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        displayScore = Mathf.RoundToInt(score).ToString();
        textComponent.text = "Score: " + displayScore;
    }
}
