using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUp : MonoBehaviour
{
    public GameObject start;
    public GameObject racer;

    void Start()
    {
        start.SetActive(true);  // Activates the finish line script, chosen in Inspector
        racer.SetActive(true);  // Activate the player model on load
    }

    public void Return()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);   // When the return button is pressed the scene with index 1 less than current loads, used for end game scenes
    }
}
