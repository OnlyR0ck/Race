using System.Collections;
using System.Linq;
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
        GameObject activeVehicle = GetActiveVehicle();
        GameObject vehicle = _vehicles[index];
        vehicle.transform.localPosition = activeVehicle.transform.localPosition;
        activeVehicle.SetActive(false);


        if (Physics.Raycast(vehicle.transform.position, Vector3.down, out var hit, 10f))
        {
            vehicle.transform.localPosition = new Vector3(vehicle.transform.localPosition.x, vehicle.transform.localPosition.y - hit.distance + 1, 0f);
            //vehicle.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(hit.normal));
        }
        vehicle.SetActive(true);
        
    }
    
    private GameObject GetActiveVehicle()
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.activeSelf);
    }
}
