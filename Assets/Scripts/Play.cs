using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    [SerializeField] GameObject _loadingUI;

    public void OnClick()
    {
        _loadingUI.SetActive(true);
        SceneManager.LoadScene(1);
    }
}
