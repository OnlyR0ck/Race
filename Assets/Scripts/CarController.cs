using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static event Action<int> LevelWasEnded; 
    [SerializeField] private string _leadRoad;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private ParticleSystem _confetti;
    public enum Drivetrain {FWD,
        RWD,
        FourWD};
    
    private float horizontalInput, verticalInput;
    private float steeringAngle;
    
    
    [Header("Car Settings")]
    public Drivetrain drivetrain = Drivetrain.FWD;
    public float maxSteeringAngle = 30;
    public float torque = 60;

    [Header("Wheel Colliders")] 
    public WheelCollider frontRightWheel;
    public WheelCollider frontLeftWheel;
    public WheelCollider rearRightWheel;
    public WheelCollider rearLeftWheel;
    private WheelCollider[] _wheelColliders;

    [Header("Wheels")] 
    public Transform frontRightTransform;
    public Transform frontLeftTransform;
    public Transform rearRightTransfrom;
    public Transform rearLeftTransform;


    private void Start()
    {
        verticalInput = 1;
        _wheelColliders = new[] {frontRightWheel, frontLeftWheel, rearRightWheel, rearLeftWheel};

    }

    private void Accelerate()
    {
        switch (drivetrain)
        {
            case Drivetrain.FWD:
                frontLeftWheel.motorTorque = torque * verticalInput;
                frontRightWheel.motorTorque = torque * verticalInput;
                break;
            case Drivetrain.RWD:
                rearLeftWheel.motorTorque = torque * verticalInput;
                rearRightWheel.motorTorque = torque * verticalInput;
                break;
            case Drivetrain.FourWD:
                frontLeftWheel.motorTorque = torque * verticalInput;
                frontRightWheel.motorTorque = torque * verticalInput;
                rearLeftWheel.motorTorque = torque * verticalInput;
                rearRightWheel.motorTorque = torque * verticalInput;
                break;
        }
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontLeftWheel, frontLeftTransform);
        UpdateWheelPose(frontRightWheel, frontRightTransform);
        UpdateWheelPose(rearLeftWheel,rearLeftTransform);
        UpdateWheelPose(rearRightWheel, rearRightTransfrom);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out pos,out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    private void FixedUpdate()
    { 
        Accelerate();
        UpdateWheelPoses();
        GroundCheck();
    }

    private void GroundCheck()
    {
        if (Physics.Raycast(_groundChecker.position, Vector3.down, out var hit, 10f))
        {
            if (!(hit.collider.CompareTag(_leadRoad) || hit.collider.CompareTag("Start")))
            {
                foreach (var wheel in _wheelColliders)
                {
                    wheel.brakeTorque = 1000;
                }
            }
            else
            {
                foreach (var wheel in _wheelColliders)
                {
                    wheel.brakeTorque = 0;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _confetti.Play();
            LevelWasEnded?.Invoke(0);
        }
    }
}