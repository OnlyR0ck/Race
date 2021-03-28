using System;
using System.Collections;
using UnityEngine;

public class ChangeEnemyCarScript : MonoBehaviour
{
    [SerializeField] private float _delay;
    public static event Action<int> EnemyCarIsChanged;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals(gameObject.tag)) return;
        switch (other.tag)
        {
            case "Jeep":
                StartCoroutine(ChangeCar(0));
                break;
            case "Moto":
                StartCoroutine(ChangeCar(1));
                break;
            case "Sport":
                StartCoroutine(ChangeCar(2));
                break;
        }
    }

    IEnumerator ChangeCar(int carIndex)
    {
        yield return new WaitForSeconds(_delay);
        
        EnemyCarIsChanged?.Invoke(carIndex);
    }
}
