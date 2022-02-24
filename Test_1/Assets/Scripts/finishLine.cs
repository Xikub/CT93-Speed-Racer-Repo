using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishLine : MonoBehaviour
{
    bool gameOver = false;              // Set the intial game state to active
    public int CheckpointTotal;         // Set for the number of checkpoints, 6 in this case. Should not be changed unless more objects are added
    float currentTime = 0f;             // Variable to hold time for timer
    public float gameTime;              // Time limit for the game
    public GameObject completeLevelUI;  // UI used for winning gamestate
    public GameObject releaseMissile;   // UI used for losing gamestate
    public GameObject racer;            // Object used for player character


    // Start and Update are used for the loss condition
    // Win/Loss conditions handled in one code for simplicity
    void Start()
    {
        currentTime = gameTime;     // Set the time limit for the game
    }

    void Update()
    {
        currentTime -= Time.deltaTime;  // Decrease timer for game time limit done in seconds, not by frame

        if (currentTime <= 0f && gameOver == false)
            {
                //////// LOSS CONDITION
                gameOver = true;        // Set the game condition to over to prevent the player from being able to access win state
                releaseMissile.SetActive(true); // Release the missile which chases the player and ends the instance
            }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<racerStats>())    
        {
            racerStats Racer = other.GetComponent<racerStats>();

            if(Racer.CheckpointIndex == CheckpointTotal && gameOver == false) //Check for player's checkpoint index, if it matches and game not over, win!
            {
                ///////// WIN CONDITION
                completeLevelUI.SetActive(true);    // Activate the win game screen
                Racer.CheckpointIndex = 0;  // Sets the checkpoint index to 0, can be used to add laps
                gameOver = true;            // Set the game condition to over
                racer.SetActive(false);     // Removes the model and attached components from the scene
            }
        }
    }
}