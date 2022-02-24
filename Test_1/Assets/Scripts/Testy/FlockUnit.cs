using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockUnit : MonoBehaviour
{
    private float FOVAngle = 270f;
    private float SmoothDamp = 1f;
    // Creating lists to hold the targets in to decide which instances are flockmates
    private List<FlockUnit> cohesionNeighbours = new List<FlockUnit>();
    private List<FlockUnit> avoidanceNeighbours = new List<FlockUnit>();
    private List<FlockUnit> alignmentNeighbours = new List<FlockUnit>();
    
    private Flock assignedFlock;
    private Vector3 currentVelocity;
    private float speed;
    public Transform myTransform { get; set; }

    public void Awake()
    {
        myTransform = transform;
    }

    public void AssignFlock(Flock flock)
    {
        assignedFlock = flock;      // Rename flock variable to allow tampering
    }

    public void InitializeSpeed(float speed)
    {
        this.speed = speed;     // Set the initial speed of the object
    }

    public void MoveUnit()
    {
        FindNeighbours();
        CalculateSpeed();
        // Using calculated vectors to move the flock units
        var cohesionVector = CalculateCohesionVector() * assignedFlock.cohesionWeight;
        var avoidanceVector = CalculateAvoidanceVector() * assignedFlock.avoidanceWeight;
        var alignmentVector = CalculateAlignmentVector() * assignedFlock.alignmentWeight;

        var moveVector = cohesionVector + avoidanceVector + alignmentVector;    // Vector is sum of all 3 vectors above
        moveVector = Vector3.SmoothDamp(myTransform.forward, moveVector, ref currentVelocity, SmoothDamp);
        moveVector = moveVector.normalized * speed; // Normalize the vector to ensure one direction
        if (moveVector == Vector3.zero)     // If no neighbours continue moving forward
            moveVector = transform.forward;

        myTransform.forward = moveVector;   // Assign vector to the forward direction
        myTransform.position += moveVector * Time.deltaTime;    // Multiply by Time.deltaTime to ensure it moves by second not frame
    }

    private void FindNeighbours()
    {       // Ensure the lists are empty before assigning any objects
        cohesionNeighbours.Clear();
        avoidanceNeighbours.Clear();
        alignmentNeighbours.Clear();
        var allUnits = assignedFlock.allUnits;
        for (int i = 0; i < allUnits.Length; i++)
        {
            var currentUnit = allUnits[i];
            if (currentUnit != this)
            {   // To avoid strain on system square numbers are compared. The following calculations decide which behaviour list entities should be placed in
                float currentNeighbourDistanceSqr = Vector3.SqrMagnitude(currentUnit.transform.position - transform.position);
                if (currentNeighbourDistanceSqr <= assignedFlock.cohesionDistance * assignedFlock.cohesionDistance)     // If distance is shorter than specified distance, add to this group
                {
                    cohesionNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.avoidanceDistance * assignedFlock.avoidanceDistance)   // If distance is shorter than specified distance, add to this group
                {
                    avoidanceNeighbours.Add(currentUnit);
                }
                if (currentNeighbourDistanceSqr <= assignedFlock.alignmentDistance * assignedFlock.alignmentDistance)   // If distance is shorter than specified distance, add to this group
                {   
                    alignmentNeighbours.Add(currentUnit);
                }
            }
        }
    }

    private void CalculateSpeed()   // Use speed of neighbours to decide individual speed
    {
        if (cohesionNeighbours.Count == 0)  // Do not run if no neighbours prevents errors
            return;
        speed = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)
        {
            speed += cohesionNeighbours[i].speed;   // Add the neighbours speed to each object
        }
        speed /= cohesionNeighbours.Count;  // Divide the speed by the number of neighbours
        speed = Mathf.Clamp(speed, assignedFlock.minSpeed, assignedFlock.maxSpeed); // Clamp the value between stated minimum and maximum
    }

    private Vector3 CalculateCohesionVector()
    {
        var cohesionVector = Vector3.zero;
        if (cohesionNeighbours.Count == 0)  
            return Vector3.zero;        // If no neighbours return vector zero
        int neighboursInFOV = 0;
        for (int i = 0; i < cohesionNeighbours.Count; i++)  // Check if neighbours are in field of view
        {
            if(IsInFOV(cohesionNeighbours[i].myTransform.position)) // Prevents detection from behind
            {
                neighboursInFOV++;  // If neighbour is in field of view increase number 
                cohesionVector += cohesionNeighbours[i].myTransform.position;   // Add neighbour position to vector
            }
        }
        cohesionVector /= neighboursInFOV;
        cohesionVector -= myTransform.position;     // To convert from world frame to local frame of object
        cohesionVector = cohesionVector.normalized; // Normalized to convert from 3 to 1 directions
        return cohesionVector;
    }

    private Vector3 CalculateAlignmentVector()
    {
        var alignmentVector = myTransform.forward;
        if (alignmentNeighbours.Count == 0) 
            return myTransform.forward;     // If no neighbours return vector forward
        int neighboursInFOV = 0;        
        for (int i = 0; i < alignmentNeighbours.Count; i++) // Check if neighbours are in field of view
        {
            if (IsInFOV(alignmentNeighbours[i].myTransform.position))   // Prevents detection from behind 
            {
                neighboursInFOV++;  // If neighbour is in field of view increase number
                alignmentVector += alignmentNeighbours[i].myTransform.forward;  // Add neighbour position to vector
            }
        }
        alignmentVector /= neighboursInFOV;
        alignmentVector = alignmentVector.normalized;   // Normalized to convert from 3 to 1 directions
        return alignmentVector;
    }

    private Vector3 CalculateAvoidanceVector()
    {
        var avoidanceVector = Vector3.zero;
        if (avoidanceNeighbours.Count == 0) 
            return Vector3.zero;        // If no neighbours return vector zero
        int neighboursInFOV = 0;
        for (int i = 0; i < avoidanceNeighbours.Count; i++) // Check if neighbours are in field of view
        {
            if (IsInFOV(avoidanceNeighbours[i].myTransform.position))   // Prevents detection from behind
            {
                neighboursInFOV++;  // If neighbour is in field of view increase number 
                avoidanceVector += (myTransform.position - avoidanceNeighbours[i].myTransform.position);
            }
        }
        avoidanceVector /= neighboursInFOV;
        avoidanceVector = avoidanceVector.normalized;   // Normalized to convert from 3 to 1 directions
        return avoidanceVector;
    }

    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle; // Returns true if the position of the target is within the given field of view of the object
    }                                                                                           // This prevents detection of targets behind the object
}