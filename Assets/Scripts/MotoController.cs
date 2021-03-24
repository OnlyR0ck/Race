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
    private bool _flag = true;

    
     public enum Drivetrain {FWD,
        RWD,
        FourWD};
    
    private float horizontalInput, verticalInput = 1;
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

    [Header("Wheels")] 
    public Transform frontWheel;
    public Transform backWheel;

    private void Start()
    {
        _motoRigidbody = GetComponent<Rigidbody>();
        _motoRigidbody.centerOfMass = _centerOfMass.localPosition;
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

    private void Steer()
    {
        steeringAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheel.steerAngle = steeringAngle;
        frontRightWheel.steerAngle = steeringAngle;
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
        //wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
    
    private void GroundCheck()
    {
        RaycastHit hit;
        
        if (Physics.Raycast(_groundChecker.position, Vector3.down, out hit, 10f))
        {
            if (!hit.collider.CompareTag(_leadRoad) && _flag)
            {
                var curve = new WheelFrictionCurve {extremumSlip = 500};
                
                _flag = false;
                frontLeftWheel.forwardFriction = curve;
                frontRightWheel.forwardFriction = curve;
                rearLeftWheel.forwardFriction = curve;
                rearRightWheel.forwardFriction = curve;
            }
        }
    }

    private void FixedUpdate()
    {
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
}
