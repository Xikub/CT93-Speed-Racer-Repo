using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour     
{
    public GameObject missile;
    public GameObject racer;

    private void OnTriggerEnter()
    {
        missile.SetActive(false);   // Deactivate missile at the end of the game
        racer.SetActive(false);     // Deactivate player model at the end of the game
    }
}
