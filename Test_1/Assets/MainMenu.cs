using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // Load the scene 1 index value above the current
    }

    public void QuitGame()
    {
        Application.Quit(); // Close the application
    }

}
