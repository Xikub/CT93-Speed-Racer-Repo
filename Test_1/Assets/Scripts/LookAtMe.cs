using UnityEngine;
using UnityEngine.AI;

public class LookAtMe : MonoBehaviour
{
    public Transform lookAtTarget;  // Target to be looked at
    public GameObject loseScreen;   // Canvas to be used when colliders impact 

    float smoothSpeed = 0.5f;   // Modifier between 0 and 1 for distance moved per frame
    
    void FixedUpdate()
    {
        Vector3 desiredPosition = lookAtTarget.position; // This is the desired position of the object
        Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);  // Uses Slerp over Lerp is it will smoothly move between points A and B
        transform.position = smoothedPosition;
        transform.LookAt(lookAtTarget); // Ensures the camera always follows the same object, the player in this case
    }

    private void OnTriggerEnter(Collider racer)
    {
        if(racer.GetComponent<racerStats>())
        {
            racerStats Racer = racer.GetComponent<racerStats>();    // When the object makes impact with the player model it activates the lose screen
            loseScreen.SetActive(true);
        }
    }
}
