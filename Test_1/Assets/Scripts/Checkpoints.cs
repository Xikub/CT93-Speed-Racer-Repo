using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    public int Index; //Unique ID for each checkpoint, inputted manually into Inspector

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<racerStats>())// Check the player has entered the checkpoint
        {
            racerStats Racer = other.GetComponent<racerStats>();    

            if(Racer.CheckpointIndex == Index -1)  
            {
                Racer.CheckpointIndex = Index;
                // If the index of the racer 1 fewer than current checkpoint then increase the index
                Debug.Log(Index);
            }
        }
    }
}