using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1); //starts the game, loads scene with index 1 - room
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); //loads the scene with the index 1 less than the active scene - goes back
    }

    public void Restart()
    {
        SceneManager.LoadScene(1); //restarts the game, loads scene with index 1 - room
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(0); //loads scene with index 0 - main menu
    }

    public void QuitGame()
    {
        Application.Quit(); //quits the game
    }
}
