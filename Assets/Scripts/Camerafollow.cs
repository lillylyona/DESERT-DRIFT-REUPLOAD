using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Smoothing factor for camera movement.
    public float moveSmoothness;
    // Smoothing factor for camera rotation.
    public float rotSmoothness;

    // Position offset from the target object (car).
    public Vector3 moveOffset;
    // Rotation offset from the target object's direction.
    public Vector3 rotOffset;

    // Reference to the car's transform.
    public Transform carTarget;

    // Update camera position and rotation in FixedUpdate to better sync with physics calculations.
    void FixedUpdate()
    {
        FollowTarget();
    }

    // Central method to handle position and rotation updates.
    void FollowTarget()
    {
        HandleMovement();
        HandleRotation();
    }

    // Moves the camera smoothly towards the target object based on moveOffset.
    void HandleMovement()
    {
        // Calculate the target position based on offset from the car.
        Vector3 targetPos = carTarget.TransformPoint(moveOffset);

        // Smoothly calculates the camera's position towards the target position.
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSmoothness * Time.deltaTime);
    }

    // Rotates the camera smoothly to always look at the car.
    void HandleRotation()
    {
        // Determine the direction from the camera to the car.
        var direction = carTarget.position - transform.position;
        // Calculate the desired rotation to look at the car including the offset.
        var rotation = Quaternion.LookRotation(direction + rotOffset, Vector3.up);

        // Smoothly calculates the camera's rotation towards the desired rotation.
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, rotSmoothness * Time.deltaTime);
    }
}