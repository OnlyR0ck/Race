using System.Linq;
using UnityEngine;

public class PlayerListener : MonoBehaviour
{
    [SerializeField] private GameObject[] _vehicles;

    private void OnEnable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged += CarChangedHandler;
    }
    
    private void OnDisable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged -= CarChangedHandler;
    }
    private void CarChangedHandler(int index)
    {
        GameObject activeVehicle = GetActiveVehicle();
        GameObject vehicle = _vehicles[index];
        vehicle.transform.localPosition = activeVehicle.transform.localPosition;
        vehicle.transform.eulerAngles = new Vector3(0f, -90f, activeVehicle.transform.eulerAngles.z); 
        activeVehicle.SetActive(false);


        if (Physics.Raycast(vehicle.transform.position, Vector3.down, out var hit, 10f))
        {
            vehicle.transform.localPosition = new Vector3(vehicle.transform.localPosition.x, vehicle.transform.localPosition.y - hit.distance + 1, 0f);
            //vehicle.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(hit.normal));
        }
        vehicle.SetActive(true);
        vehicle.GetComponent<Rigidbody>().velocity  = Vector3.forward * 5f;
    }
    private GameObject GetActiveVehicle()
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.activeSelf);
    }
}
