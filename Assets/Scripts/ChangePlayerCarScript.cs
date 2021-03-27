using System;
using System.Linq;
using UnityEngine;

public class ChangePlayerCarScript : MonoBehaviour
{
    public enum Vehicles
    {
        Jeep = 0,
        Moto = 1,
        SportCar = 2
    }
    public static event Action<int> PlayerCarIsChanged;
    [SerializeField] private GameObject[] _vehicles;

    public void JeepButtonClicked()
    {
        if (!GetActiveVehicle().name.Equals("Jeep2"))
        {
            PlayerCarIsChanged?.Invoke((int) Vehicles.Jeep);
        }
    }

    public void MotoButtonClicked()
    {
        
        if (!GetActiveVehicle().name.Equals("Moto"))
        {
            PlayerCarIsChanged?.Invoke((int) Vehicles.Moto);
        }
    }

    public void SportCarButtonClicked()
    {
        if (!GetActiveVehicle().name.Equals("SportCar2"))
        {
            PlayerCarIsChanged?.Invoke((int) Vehicles.SportCar);
        }
    }

    private GameObject GetActiveVehicle()
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.activeSelf);
    }
    
}
