using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundary : MonoBehaviour
{
    // Static variables to define the left and right boundaries of the level.
    // These boundaries are accessible from any other script.
    public static float leftSide = 184f;
    public static float rightSide = 193f;

    // Non-static variables to mirror the static boundary values.
    public float internalLeft;
    public float internalRight;

    // Update is called once per frame
    void Update()
    {
        // Update the instance-specific boundary variables to match the static boundary values.
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}