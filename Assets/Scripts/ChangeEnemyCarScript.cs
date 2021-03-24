using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEnemyCarScript : MonoBehaviour
{
    public static event Action<int> EnemyCarIsChanged;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MotoEnemy"))
        {
            EnemyCarIsChanged?.Invoke(1);
        }
        else if (other.CompareTag("SportEnemy"))
        {
            EnemyCarIsChanged?.Invoke(2);
        }
    }
}
