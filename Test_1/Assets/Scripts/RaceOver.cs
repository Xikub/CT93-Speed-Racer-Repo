using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceOver : MonoBehaviour
{

    float currentTime = 0f;
    public float startingTime = 90f;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;
        // Debug.Log(currentTime);
        if (currentTime <= 0f && currentTime >= -0.008f)
            {
                Debug.Log("You lose because good is dumb");
            }
    }

}
