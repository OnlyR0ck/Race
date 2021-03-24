using UnityEngine;

public class EnemyListener : MonoBehaviour
{
    [SerializeField] private GameObject[] _vehicles;

    private void OnEnable()
    {
        ChangeEnemyCarScript.EnemyCarIsChanged += CarChangedHandler;
    }
    
    private void OnDisable()
    {
        ChangeEnemyCarScript.EnemyCarIsChanged -= CarChangedHandler;
    }
    private void CarChangedHandler(int index)
    {
        Transform _position = _vehicles[index - 1].transform;
        _vehicles[index - 1].SetActive(false);
        _vehicles[index].SetActive(true);
        if (index == 1)
        {
            _vehicles[index].transform.localPosition = new Vector3(_position.localPosition.x, -2.3f, 0.0f);
        }
        else
        {
            _vehicles[index].transform.localRotation = Quaternion.Euler(new Vector3(0, -90, 0));
            _vehicles[index].transform.localPosition = new Vector3(_position.localPosition.x, -2f, 0.0f);
        }
        
    }
}
