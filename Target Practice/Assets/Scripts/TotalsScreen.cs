using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalsScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnScoreUpdated += UpdateFinalScore;
    }

    // Update is called once per frame
    void UpdateFinalScore(int score)
    {
        finalScoreText.text = "Total score: " + score.ToString() + "/100";
    }
}
