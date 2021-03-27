using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _vehicles;
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private Sprite[] _jeepSprites;
    [SerializeField] private Sprite[] _motoSprites;
    [SerializeField] private Sprite[] _sportCarSprites;
    [SerializeField] private Button _nextButton;
    private void OnEnable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged += HandlePlayerCarChange;
        CarController.LevelWasEnded += LevelEndedHandler;
    }

    private void Start()
    {
        switch (GetActiveVehicle().name)
        {
            case "Jeep2":
                HandlePlayerCarChange(0);
                break;
            case "Moto":
                HandlePlayerCarChange(1);
                break;
            case "SportCar2":
                HandlePlayerCarChange(2);
                break;
        }
    }

    private void OnDisable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged -= HandlePlayerCarChange;
        CarController.LevelWasEnded -= LevelEndedHandler;

    }

    private void HandlePlayerCarChange(int index)
    {
        switch (index)
        {
            case 0:
                _buttons[0].GetComponent<Button>().image.sprite = _jeepSprites[1];
                _buttons[1].GetComponent<Button>().image.sprite = _motoSprites[0];
                _buttons[2].GetComponent<Button>().image.sprite = _sportCarSprites[0];
                break;
            case 1:
                _buttons[0].GetComponent<Button>().image.sprite = _jeepSprites[0];
                _buttons[1].GetComponent<Button>().image.sprite = _motoSprites[1];
                _buttons[2].GetComponent<Button>().image.sprite = _sportCarSprites[0];
                break;
            case 2:
                _buttons[0].GetComponent<Button>().image.sprite = _jeepSprites[0];
                _buttons[1].GetComponent<Button>().image.sprite = _motoSprites[0];
                _buttons[2].GetComponent<Button>().image.sprite = _sportCarSprites[1];
                break;
        }
    }

    private GameObject GetActiveVehicle()
    {
        return _vehicles.FirstOrDefault(vehicle => vehicle.activeSelf);
    }

    private void LevelEndedHandler(int winner)
    {
        foreach (var button in _buttons)
        {
            button.SetActive(false);
        }
        
        _nextButton.gameObject.SetActive(true);
    }
}
