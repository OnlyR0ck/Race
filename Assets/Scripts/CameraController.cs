using System;
using System.Linq;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private GameObject[] _targets;
    private Transform _target;
    private readonly Vector3 _offset = new Vector3(8,5,10);
    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;
    [SerializeField] [Range(0, 1f)] private float _smoothSpeed;
    
    private void OnEnable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged += TargetChangeHandler;
    }

    private void Start()
    {
        _target = GetActiveVehicle().transform;
    }

    private void OnDisable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged -= TargetChangeHandler;
    }
    private void TargetChangeHandler(int index)
    {
        _target = _targets[index].transform;
    }

    private void LateUpdate()
    {
        _desiredPosition = _target.position + _offset;
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
        transform.position = _smoothedPosition;
        
        transform.LookAt(_target);
    }
    
    private GameObject GetActiveVehicle()
    {
        return _targets.FirstOrDefault(vehicle => vehicle.activeSelf);
    }
}
