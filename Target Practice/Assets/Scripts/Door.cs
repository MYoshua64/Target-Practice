using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    int targetsHit;
    Animator doorAnim;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = GetComponent<Animator>();
        EventManager.instance.OnTargetHit += RegisterTargetHit;
    }

    void RegisterTargetHit(int points)
    {
        targetsHit++;
        if (targetsHit == 10)
        {
            doorAnim.SetTrigger("Open");
        }
    }
}
