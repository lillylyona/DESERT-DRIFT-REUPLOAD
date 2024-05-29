using UnityEngine;

// This script handles the audio effects for car interactions within the game.
public class CarAudio : MonoBehaviour
{
    // Reference to the AudioSource component on the same GameObject.
    public AudioSource audioSource;
    // Audio clip to play when collecting items.
    public AudioClip collect;

    // Called when the script instance is loaded.
    private void Start()
    {
        // Get the AudioSource component attached to this GameObject.
        audioSource = GetComponent<AudioSource>();
    }
    
    // This method is called when another collider enters the trigger collider attached to this object.
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider that entered the trigger is tagged as "Bottle".
        if (other.CompareTag("Bottle"))
        {
            // Play the collect sound effect once.
            audioSource.PlayOneShot(collect);
        }
    }
}