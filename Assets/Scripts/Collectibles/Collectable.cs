using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // The speed at which the collectable rotates around its vertical axis.
    public int rotateSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        // Rotate the object around its y-axis in the world space at a constant speed.
        transform.Rotate(0, rotateSpeed, 0, Space.World);
    }
  
    // Method to handle the behavior when the collectable is collected.
    public void Collect()
    {
        // Destroy the GameObject to which this script is attached, effectively "collecting" it.
        Destroy(gameObject);
    }
}

