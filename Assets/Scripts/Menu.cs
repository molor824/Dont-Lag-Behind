using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Canvas))]
public class Menu : MonoBehaviour
{
    Controls _controls;
    Canvas _canvas;

    public void OnResumeClick()
    {
        _canvas.enabled = false;
    }
    public void OnBackClick()
    {
        SceneManager.LoadScene(0);
    }

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        _canvas.enabled = false;
        _controls = new();

        _controls.Player.Enable();

        _controls.Player.Menu.performed += _ =>
        {
            _canvas.enabled = !_canvas.enabled;
        };
    }
}
