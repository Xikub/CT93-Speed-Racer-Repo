using UnityEngine;
using System.Collections;

public class DestroyEffect : MonoBehaviour {

	public Transform missile;
	UnityEngine.AI.NavMeshAgent explosion;

    void Start()
    {
        explosion = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    void Update()
    {
        explosion.destination = missile.position; // Agent will attempt to move to the position the player is on
    }
}