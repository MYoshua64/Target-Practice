using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    int score;

    // Start is called before the first frame update
    void Start()
    {
        EventManager.instance.OnTargetHit += UpdateScore;
    }

    // Update is called once per frame
    void UpdateScore(int addedScore)
    {
        score += addedScore;
        EventManager.instance.OnScoreUpdated?.Invoke(score);
    }
}
