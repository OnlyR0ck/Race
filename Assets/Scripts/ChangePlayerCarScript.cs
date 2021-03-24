using System;
using UnityEngine;

public class ChangePlayerCarScript : MonoBehaviour
{
    public static event Action<int> PlayerCarIsChanged;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Moto"))
        {
            PlayerCarIsChanged?.Invoke(1);
        }
        else if (other.CompareTag("Sport"))
        {
            PlayerCarIsChanged?.Invoke(2);
        }
    }
}
