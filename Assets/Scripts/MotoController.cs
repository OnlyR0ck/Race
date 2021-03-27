using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MotoController : MonoBehaviour
{
    private Rigidbody _motoRigidbody;
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private string _leadRoad;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private ParticleSystem _confetti;
    
    
     public enum Drivetrain {FWD,
        RWD,
        FourWD};
    
    private float verticalInput = 1;
    
    
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
    public Transform frontWheel;
    public Transform backWheel;

    private void Start()
    {
        _motoRigidbody = GetComponent<Rigidbody>();
        _motoRigidbody.centerOfMass = _centerOfMass.localPosition;
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
        UpdateWheelPose(frontRightWheel, frontWheel);
        UpdateWheelPose(rearRightWheel, backWheel);
    }

    private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos = wheelTransform.position;
        Quaternion rot = wheelTransform.rotation;
        wheelCollider.GetWorldPose(out pos,out rot);
        //wheelTransform.position = pos; 'cause of four wheels 
        wheelTransform.rotation = rot;
    }
    
    private void GroundCheck()
    {
        if (!Physics.Raycast(_groundChecker.position, Vector3.down, out var hit, 10f)) return;
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

    private void FixedUpdate()
    {
        Accelerate();
        UpdateWheelPoses();
        GroundCheck();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _confetti.Play();
        }
    }
}
