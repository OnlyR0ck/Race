using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private int _sceneCount;
    
    public void ChangeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(Random.Range(0, _sceneCount));
    }
}
