using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithForce : MonoBehaviour
{
    private Rigidbody playerCar;

    public float _movementForce = 20f;  //Force multiplier for moving forward
    public float turnSpeed = 100f;      //Speed of rotation

    private void Awake() => playerCar = GetComponent<Rigidbody>();

    private void FixedUpdate(){
        if (Input.GetKey(KeyCode.W))    // Move forward
            playerCar.AddForce(_movementForce*-transform.forward);
        
        if (Input.GetKey(KeyCode.S))    // Move backward
            playerCar.AddForce(_movementForce*transform.forward);

        if (Input.GetKey(KeyCode.LeftShift)) // Boost button
            playerCar.AddForce((_movementForce*3)*-transform.forward);
            
        if (Input.GetKey(KeyCode.Space))    // Brake button
            GetComponent<Rigidbody>().velocity = Vector3.zero;

        float rotation = Input.GetAxisRaw("Horizontal") * turnSpeed * Time.deltaTime;
        transform.Rotate(0,rotation,0);
   }

   
}
