using UnityEngine;
using TMPro;

public class CollectObject : MonoBehaviour
{
    public TMP_Text EnergyCountDisplay;  // UI Text element to display the current energy count.
    public GameOverManager gameOverManager;  // Reference to the GameOverManager to handle game over scenario.

    // Initialize settings when the game starts.
    private void Start()
    {
        CollectibleControl.EnergyCount = 100;  // Set the initial energy count.
        EnergyCountDisplay.text = CollectibleControl.EnergyCount.ToString();  // Display the initial energy count on UI.
    }

    // Triggered when this object collides with another object marked as a trigger.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bottle"))  // Check if the object is a 'Bottle'.
        {
            CollectibleControl.EnergyCount += 10;  // Increase the energy count by 10.
            other.GetComponent<Collectable>().Collect();  // Call the Collect method on the collected object.
        }
        
        UpdateEnergyDisplay();  // Update the UI display of the energy count.
    }

    // Triggered when a collision occurs with another object.
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Obstacle"))  // Check if the collided object is an 'Obstacle'.
        {
            CollectibleControl.EnergyCount -= 20;  // Decrease the energy count by 20.
            UpdateEnergyDisplay();  // Update the UI display of the energy count.
            CheckGameOver();  // Check if the game should end based on the new energy count.
        }
    }

    // Update the displayed energy count on the UI.
    void UpdateEnergyDisplay()
    {
        EnergyCountDisplay.text = CollectibleControl.EnergyCount.ToString();
    }

    // Check if the energy count has reached zero and trigger game over if so.
    void CheckGameOver()
    {
        if (CollectibleControl.EnergyCount <= 0)
        {
            CollectibleControl.EnergyCount = 0;  // Ensure energy doesn't display negative numbers.
            EnergyCountDisplay.text = "0";  // Update UI to show zero energy.
            gameOverManager.ShowGameOver();  // Trigger the game over sequence.
        }
    }
}
