using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform[] _targets;
    public Transform target;
    private Vector3 _offset;
    private Vector3 _tempOffset;
    private Vector3 _desiredPosition;
    private Vector3 _smoothedPosition;
    [SerializeField] [Range(0, 1f)] private float _smoothSpeed;


    private void OnEnable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged += TargetChangeHandler;
    }

    private void OnDisable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged -= TargetChangeHandler;
    }
    private void TargetChangeHandler(int index)
    {
        target = _targets[index];

    }

    private void Start()
    {
        _offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        _desiredPosition = target.position + _offset;
        _smoothedPosition = Vector3.Lerp(transform.position, _desiredPosition, _smoothSpeed);
        transform.position = _smoothedPosition;
        
        transform.LookAt(target);
    }
}
