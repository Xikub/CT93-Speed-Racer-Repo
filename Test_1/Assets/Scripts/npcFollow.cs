// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class npcFollow : MonoBehaviour
{
    public Transform transformToFollow; //Select model in Inspector, player model is selected
    NavMeshAgent agent; // NavMesh is used to prevent the models climbing onto the race track
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        agent.destination = transformToFollow.position; // Agent will attempt to move to the position the player is on
    }
}
