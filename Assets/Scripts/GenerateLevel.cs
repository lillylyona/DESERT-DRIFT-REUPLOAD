using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section; // Array of section prefabs to instantiate as part of the level.

    public int zPos = 50; // Initial position for the first section to be instantiated.

    public bool creatingSection = false; // Flag to control the flow of section creation to avoid overlap.
    public int secNum; 

    void Update()
    {
        // Check if a section is not currently being created.
        if (creatingSection == false)
        {
            creatingSection = true; //indicate a section is now being created.
            StartCoroutine(GenerateSection()); // Start the coroutine to generate a section.
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 3); // Select a random index for the section array.
        Instantiate(section[secNum], new Vector3(0, 0, zPos), Quaternion.identity); // Instantiate the section at the specified z position.
        zPos += 50; // Increment the z position for the next section to be placed further down the track.
        yield return new WaitForSeconds(2); // Wait for 2 seconds before allowing another section to be created.
        creatingSection = false; // Reset so another section can be created.
    }
}