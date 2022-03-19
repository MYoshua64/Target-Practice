using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    public System.Action<int> OnTargetHit;
    public System.Action<int> OnScoreUpdated;

    private void Awake()
    {
        instance = this;
    }
}
