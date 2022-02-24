using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [Header("Flock Data")]
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;
    public float minSpeed = 2f; // Minimum speed for flock units
    public float maxSpeed = 7f; // Maximum speed for flock units
    public float cohesionDistance = 3f; // Distances set for use with behaviours
    public float avoidanceDistance = 1f;    
    public float alignmentDistance = 2f;    
    public float cohesionWeight = 4f;   // Weight for decision on which behaviour to use
    public float avoidanceWeight = 2f;
    public float alignmentWeight = 0.7f;
    public FlockUnit[] allUnits {get; set; } 

    private void Start()
    {
        GenerateUnits();    // Create the instances as soon as the scene loads
    }

    private void Update()
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();     // Tells every unit in the flock to move in the decided way
        }
    }

    private void GenerateUnits()
    {
        allUnits = new FlockUnit[flockSize];    // Creates object to act as the "flock"
        for (int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere; // Create the boundary for the spawn vector
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z); // Creating the spawnable area
            var spawnPosition = transform.position + randomVector;  // Assign a distance from the centre point for each object
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);    // Assign an angle from 0 for each object
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);    // Create the instance using the predecided values
            allUnits[i].AssignFlock(this);  // Add the instance to the flock
            allUnits[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));  // Give each instance a starting speed
            allUnits[i].gameObject.name = "Obstacle " + (i + 1);    // Name each instance for ease of debug
        }
    }
}