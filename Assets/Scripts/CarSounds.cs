using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSounds : MonoBehaviour
{
    public float minSpeed;     
    public float maxSpeed;     
    private float currentSpeed;

    private Rigidbody carRb;   
    private AudioSource carAudio; 

    public float minPitch;     // Minimum pitch of the audio source.
    public float maxPitch;     // Maximum pitch of the audio source.
    private float pitchFromCar; // Calculated pitch based on the car's speed.

    void Start()
    {
        // Initialize the AudioSource and Rigidbody components from the car.
        carAudio = GetComponent<AudioSource>();
        carRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Update the engine sound parameters each frame.
        EngineSound();
    }

    void EngineSound()
    {
        // Calculate the current speed based on the magnitude of the Rigidbody's velocity.
        currentSpeed = carRb.velocity.magnitude;
        // Calculate pitch adjustment from the car's speed.
        pitchFromCar = carRb.velocity.magnitude / 60f;
        
        if (currentSpeed < minSpeed)
        {
            // If the car's speed is below the minimum speed, set the pitch to its lowest.
            carAudio.pitch = minPitch;
        }
        else if (currentSpeed > minSpeed && currentSpeed < maxSpeed)
        {
            carAudio.pitch = minPitch + pitchFromCar;
        }
        else if (currentSpeed > maxSpeed)
        {
            // If the car's speed exceeds the maximum speed, cap the pitch at its highest.
            carAudio.pitch = maxPitch;
        }
    }
}
