using UnityEngine;
using Random = UnityEngine.Random;

public class SetStartVehicle : MonoBehaviour
{
    [SerializeField] private GameObject[] _vehicles;

    private void Awake()
    {
        _vehicles[Random.Range(0, _vehicles.Length)].SetActive(true);
    }
}
