using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _buttons;
    [SerializeField] private Sprite[] _jeepSprites;
    [SerializeField] private Sprite[] _motoSprites;
    [SerializeField] private Sprite[] _sportCarSprites;
    private void OnEnable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged += HandlePlayerCarChange;
    }

    private void OnDisable()
    {
        ChangePlayerCarScript.PlayerCarIsChanged -= HandlePlayerCarChange;
    }

    private void HandlePlayerCarChange(int index)
    {
        if (index == 1)
        {
            _buttons[index].GetComponent<Image>().sprite = _motoSprites[1];
            _buttons[index - 1].GetComponent<Image>().sprite = _jeepSprites[0];
        }
        else
        {
            _buttons[index].GetComponent<Image>().sprite = _sportCarSprites[1];
            _buttons[index - 1].GetComponent<Image>().sprite = _motoSprites[0];
        }
    }
}
