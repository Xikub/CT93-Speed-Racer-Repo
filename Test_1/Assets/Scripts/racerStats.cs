using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racerStats : MonoBehaviour
{
    private Rigidbody playerCar;    // Select the object to be used as the player model
    public int CheckpointIndex;     // Value for holding number of checkpoints player has passed through
    float _movementForce = 20f;     //Force multiplier for moving forward
    float turnSpeed = 100f;         //Speed of rotation
    AudioSource hovers;     // Variable for controlling sounds
    bool playing = false;
    
    private void Awake() => playerCar = GetComponent<Rigidbody>();  // Assigns rigidbody attribute to the player model

    void Start()
    {
        CheckpointIndex = 0; //Start the index at 0 initially
        hovers = GetComponent<AudioSource>();
        // hovers.Stop();
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))    // Move forward
            playerCar.AddForce(_movementForce * -transform.forward);
        
        if (Input.GetKey(KeyCode.S))    // Move backward
            playerCar.AddForce(_movementForce * transform.forward);

        if (Input.GetKey(KeyCode.LeftShift)) // Boost button
            playerCar.AddForce((_movementForce * 3) * -transform.forward);
            
        if (Input.GetKey(KeyCode.Space))    // Brake button
            GetComponent<Rigidbody>().velocity = Vector3.zero;

        float rotation = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime;   // This allows the player model to rotate and continue pushing forward
        transform.Rotate(0,rotation,0);
    }
    void Update()
    {
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.LeftShift)) && !playing)
        {
            hovers.Play();
            playing = true; // If the W, S, or Left Shift keys are pressed play the audio and mark it as so
        }
        
        while ((!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift)) && playing)
        {
            hovers.Stop();
            playing = false;    // When the keys are not being pressed stop playing audio and mark it as so
        }
    }
    
}
