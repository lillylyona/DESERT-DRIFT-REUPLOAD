using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDistance : MonoBehaviour
{
    public GameObject disDisplay;  // The UI that displays the distance.
    public static int disRun;  // Static variable to keep track of the distance covered.
    public bool addingDis = false;  
    public float disDelay = 0.35f;  

    // Initialize the distance at the start of the game.
    private void Start()
    {
        disRun = 0;  // Set initial distance to zero.
    }

    // Update is called once per frame.
    void Update()
    {
        // Check if the coroutine is not already running.
        if (addingDis == false)
        {
            addingDis = true;  
            StartCoroutine(AddingDis());  // Start the coroutine that handles distance adding.
        }
    }

    // Coroutine to increment the distance and update the UI.
    IEnumerator AddingDis()
    {
        disRun += 1;  // Increment the distance.
        disDisplay.GetComponent<TMP_Text>().text = disRun.ToString();  // Update the UI text to display the new distance.
        yield return new WaitForSeconds(disDelay);  // Wait for the specified delay.
        addingDis = false;  
    }
}
