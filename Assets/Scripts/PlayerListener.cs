using System;
using System.Collections;
using System.Collections.Generic;
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
        Transform _position = _vehicles[index - 1].transform;
        _vehicles[index - 1].SetActive(false);
        _vehicles[index].SetActive(true);
        if (index == 1)
        {
            _vehicles[index].transform.localPosition = new Vector3(_position.localPosition.x, -2.3f, 0.0f);
        }
        else
        {
            _vehicles[index].transform.localRotation = Quaternion.Euler(new Vector3(0, -89, 0));
            _vehicles[index].transform.localPosition = new Vector3(_position.localPosition.x, -2f, 0.0f);
        }
    }
}
