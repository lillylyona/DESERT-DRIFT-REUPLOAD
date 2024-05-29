using UnityEngine;
using System;
using System.Collections.Generic;

public class CarController : MonoBehaviour
{
    // Enum to define the control mode (Keyboard or Buttons).
    public enum ControlMode
    {
        Keyboard,
        Buttons
    };

    // Enum to define the axle type of the wheels (Front or Rear).
    public enum Axel
    {
        Front,
        Rear
    }

    // Structure to hold information about each wheel.
    [Serializable]
    public struct Wheel
    {
        public GameObject wheelModel;         
        public WheelCollider wheelCollider;   
        public GameObject wheelEffectObj;    
        public ParticleSystem smokeParticle;  
        public Axel axel;                     
    }

    public ControlMode control;             // Current control mode used.
    public float maxAcceleration = 10.0f;   // Maximum force applied to accelerate.
    public float brakeAcceleration = 50.0f; // Maximum force applied to decelerate.
    public float turnSensitivity = 1f;      // Sensitivity of steering.
    public float maxSteerAngle = 30.0f;     // Maximum steering angle.

    public Vector3 _centerOfMass;           // Custom center of mass for the car's physics.

    public List<Wheel> wheels;              // List containing all wheels attached to the vehicle.

    private float moveInput;                // Input value for moving (acceleration/deceleration).
    private float steerInput;               // Input value for steering.

    private Rigidbody carRb;                // Rigidbody component for vehicle physics.
    

    void Start()
    {
        carRb = GetComponent<Rigidbody>();
        carRb.centerOfMass = _centerOfMass; // Set the center of mass to the car's Rigidbody.
    }

    void Update()
    {
        GetInputs();            // Process input for controlling the car.
        AnimateWheels();        // Update wheel animations to match their physics.
        WheelEffects();         // Handle visual effects related to wheels.
        
        // Add downward force to ensure wheels stay on the ground.
        carRb.AddForce(Vector3.down * 10f);
    }

    void LateUpdate()
    {
        Move();   // Handle moving the car based on input.
        Steer();  // Handle steering the car based on input.
        Brake();  // Handle braking.
    }

    // Processes player input for movement.
    public void MoveInput(float input)
    {
        moveInput = input;
    }

    // Processes player input for steering.
    public void SteerInput(float input)
    {
        steerInput = input;
    }
    
    void GetInputs()
    {
        if(control == ControlMode.Keyboard)
        {
            moveInput = Input.GetAxis("Vertical");
            steerInput = Input.GetAxis("Horizontal");
        }
    }

    // Apply torque to wheel colliders to move the car.
    void Move()
    {
        foreach (var wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = moveInput * 600 * maxAcceleration * Time.deltaTime;
        }
    }

    // Adjust wheel collider steering angles based on input.
    void Steer()
    {
        foreach(var wheel in wheels)
        {
            if (wheel.axel == Axel.Front)
            {
                float targetSteerAngle = steerInput * turnSensitivity * maxSteerAngle;
                wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, targetSteerAngle, 0.2f);
            }
        }
    }

    // Apply braking force to slow down the car.
    void Brake()
    {
        if (Input.GetKey(KeyCode.Space) || moveInput == 0)
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 300 * brakeAcceleration;
            }
        }
        else
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.brakeTorque = 0;
            }
        }
    }

    // Update wheel model positions and rotations to match the physics colliders.
    void AnimateWheels()
    {
        foreach(var wheel in wheels)
        {
            Quaternion rot;
            Vector3 pos;
            wheel.wheelCollider.GetWorldPose(out pos, out rot);
            wheel.wheelModel.transform.position = pos;
            wheel.wheelModel.transform.rotation = rot;
        }
    }

    // Handle the emission of the effects like smoke.
    void WheelEffects()
    {
        foreach (var wheel in wheels)
        {
            if (Input.GetKey(KeyCode.Space) && wheel.axel == Axel.Rear && wheel.wheelCollider.isGrounded == true && carRb.velocity.magnitude >= 10.0f)
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = true;
                wheel.smokeParticle.Emit(1);
            }
            else
            {
                wheel.wheelEffectObj.GetComponentInChildren<TrailRenderer>().emitting = false;
            }
        }
    }
}
