using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; // Reference to the game over UI panel that should be shown when the game ends.

    
// Method to show the game over screen and stop all gameplay.
    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true); // Activate the game over panel, making it visible.
        Time.timeScale = 0; // Freeze game time, stopping all gameplay.
    }
// Method to restart the game by reloading the current scene.
    public void RestartGame()
    {
        Time.timeScale = 1; // Resume normal time scale
        // Reload the current active scene, starting the game over.
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}